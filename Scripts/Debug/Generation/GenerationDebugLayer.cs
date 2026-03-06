using Godot;
using BiomeArchitectV3.Scripts.WorldGeneration;

namespace BiomeArchitectV3.Scripts.Debug.Generation
{
    public abstract partial class GenerationDebugLayer : Node2D
    {
        [Export] public bool IsLayerEnabled { get; set; } = true;



        public void RebuildLayer(PhaseContext context)
        {
            if (!IsLayerEnabled)
            {
                Visible = false;
                ClearLayer();
                return;
            }

            Visible = true;
            Rebuild(context);
        }



        public void ClearLayer() => Clear();



        protected abstract void Rebuild(PhaseContext context);



        protected abstract void Clear();
    }
}