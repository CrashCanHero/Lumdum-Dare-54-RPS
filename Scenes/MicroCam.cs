using Godot;
using System;

public partial class MicroCam : Node2D
{
    Vector2 position;
    public override void _Ready()
    {
        WindowSystem.ResetWindow();
        WindowSystem.Position = Vector2.Zero;
        WindowSystem.Scale = Vector2.One * 0.5f;
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public static void Shrink(float scale) 
    {
        WindowSystem.Scale = new Vector2(Math.Clamp(WindowSystem.Scale.X - scale, 0.25f, 1f), Math.Clamp(WindowSystem.Scale.Y - scale, 0.25f, 1f));
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        WindowSystem.Position += position;

        position = Vector2.Zero;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseMotion)
        {
            return;
        }

        Vector2 relative = ((InputEventMouseMotion)@event).Relative * WindowSystem.PixelScale;

        if (position == Vector2.Zero)
        {
            position += relative;
        }
    }
}
