using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using BiomeArchitectV3.Scripts.Core.World;
using BiomeArchitectV3.Scripts.Core.Math;

namespace BiomeArchitectV3.Scripts.WorldGeneration
{
    public sealed partial class WorldManager : Node
    {
        private readonly List<GenerationPhase> _phases = [];

        public PhaseContext LastContext { get; private set; } = null!;




        public event Action<PhaseContext> WorldRegenerated = delegate { };



        public override void _Ready()
        {
            _phases.Clear();
            _phases.Add(new Phases.RegionMapPhase());
            _phases.Add(new Phases.SelectBiomesPhase());
            _phases.Add(new Phases.SeedBiomesPhase());
            _phases.Add(new Phases.BiomeMapPhase());
        }



        public PhaseContext Regenerate(WorldConfig config, int seedValue)
        {
            var seed = new WorldSeed(seedValue);
            var context = new PhaseContext(config, seed);

            GD.Print($"[BAV3] ========================= REGENERATE =========================");
            GD.Print($"[BAV3] World Seed = {seedValue}");
            GD.Print($"[BAV3] World = {config.TerrainWidthTiles} x {config.TerrainHeightTiles} tiles | WrapX={config.WrapX}");

            foreach (var phase in _phases)
            {
                var timer = Stopwatch.StartNew();
                GD.Print($"[BAV3] ------------ {phase.Name} ------------");
                int streamSeed = context.Seed.Derive(phase.StreamLabel);
                var rng = new DeterministicRng(streamSeed);
                GD.Print($"[BAV3] Stream = {phase.StreamLabel} | StreamSeed = {streamSeed}");
                phase.Execute(context, rng);
                timer.Stop();

                context.Timings.Set(phase.Name, timer.ElapsedMilliseconds);
                GD.Print($"[BAV3] Phase: {phase.Name} => {timer.ElapsedMilliseconds} ms");
            }

            LastContext = context;
            WorldRegenerated.Invoke(context);

            GD.Print("[BAV3] ==============================================================");

            return context;
        }
    }
}