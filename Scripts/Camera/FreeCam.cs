using Godot;

namespace BiomeArchitectV3.Scripts.Camera
{
    public sealed partial class FreeCam : Camera2D
    {
        private const float BASE_MOVE_SPEED_PX_PER_SEC = 4000f;
        private const float FAST_MOVE_MULTIPLIER = 10f;
        private const float SLOW_MOVE_MULTIPLIER = 0.25f;
        private const float ZOOM_STEP = 0.12f;
        private const float ZOOM_STEP_FAST = 0.75f;
        private const float ZOOM_STEP_SLOW = 0.04f;
        private const float ZOOM_MIN = 0.01f;
        private const float ZOOM_MAX = 5f;

        [Export] public Vector2 StartPos { get; set; } = Vector2.Zero;
        [Export] public Vector2 StartZoom { get; set; } = new Vector2(0.25f, 0.25f);



        public override void _Ready()
        {
            MakeCurrent();
            GetTree().Root.GrabFocus();
            GlobalPosition = StartPos;
            Zoom = StartZoom;
        }



        public override void _Process(double delta)
        {
            Vector2 dir = Vector2.Zero;

            if (Input.IsActionPressed("freecam_left")) dir.X -= 1f;
            if (Input.IsActionPressed("freecam_right")) dir.X += 1f;
            if (Input.IsActionPressed("freecam_up")) dir.Y -= 1f;
            if (Input.IsActionPressed("freecam_down")) dir.Y += 1f;

            if (dir == Vector2.Zero)
                return;

            dir = dir.Normalized();
            float speed = BASE_MOVE_SPEED_PX_PER_SEC * GetMoveSpeedMultiplier();
            Position += dir * speed * (float)delta;
        }



        public override void _UnhandledInput(InputEvent e)
        {
            if (e.IsActionPressed("freecam_zoom_in"))
            {
                ApplyZoom(zoomIn: true);
                GetViewport().SetInputAsHandled();
                return;
            }
            else if (e.IsActionPressed("freecam_zoom_out"))
            {
                ApplyZoom(zoomIn: false);
                GetViewport().SetInputAsHandled();
                return;
            }
        }



        private float GetMoveSpeedMultiplier()
        {
            bool fast = Input.IsActionPressed("freecam_fast");
            bool slow = Input.IsActionPressed("freecam_slow");

            if (fast && slow) return 1f;
            if (fast) return FAST_MOVE_MULTIPLIER;
            if (slow) return SLOW_MOVE_MULTIPLIER;

            return 1f;
        }



        private float GetZoomStep()
        {
            bool fast = Input.IsActionPressed("freecam_fast");
            bool slow = Input.IsActionPressed("freecam_slow");

            if (fast && slow) return ZOOM_STEP;
            if (fast) return ZOOM_STEP_FAST;
            if (slow) return ZOOM_STEP_SLOW;

            return ZOOM_STEP;
        }



        private void ApplyZoom(bool zoomIn)
        {
            float step = GetZoomStep();
            float factor = zoomIn ? (1f - step) : (1f + step);
            float next = Mathf.Clamp(Zoom.X * factor, ZOOM_MIN, ZOOM_MAX);
            Zoom = new Vector2(next, next);
        }
    }
}


