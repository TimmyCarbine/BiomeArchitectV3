using System.Collections.Generic;

namespace BiomeArchitectV3.Scripts.WorldGeneration
{
    public sealed class PhaseTimings
    {
        private readonly Dictionary<string, long> _msByPhase = new();



        public void Set(string phaseName, long elapsedMs)
        {
            _msByPhase[phaseName] = elapsedMs;
        }



        public IReadOnlyDictionary<string, long> All => _msByPhase;
    }
}