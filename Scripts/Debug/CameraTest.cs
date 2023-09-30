using Godot;
using System;

public partial class CameraTest : Node2D {
    Vector2 position;

    public override void _Ready() {
        WindowSystem.SetupWindow();
        WindowSystem.Position = Vector2.One * 0.5f;
        WindowSystem.Scale = Vector2.One * 0.25f;
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        WindowSystem.Position += (position * ((float)delta + 1f));

        position = Vector2.Zero;
    }

    public override void _Input(InputEvent @event) {
        if (@event is not InputEventMouseMotion) {
            return;
        }

        Vector2 relative = ((InputEventMouseMotion)@event).Relative * WindowSystem.PixelScale;

        if (position == Vector2.Zero) {
            position += relative;
        }
    }
}
