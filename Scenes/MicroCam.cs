using Godot;
using System;

public partial class MicroCam : Node2D
{
    public static MicroCam Instance;
    Vector2 position;

    private float shrinkTime;
    private float timer;

    public override void _Ready()
    {
        if (Instance != null) 
        {
            QueueFree();
            return;
        }
        Instance = this;

        WindowSystem.ResetWindow();

        WindowSystem.Scale = Vector2.One * 0.5f;
        WindowSystem.Position = position;

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public static void Shrink(float scale) 
    {
        WindowSystem.SetAnimation(WindowSystem.Position, WindowSystem.Position, WindowSystem.Scale, new Vector2(Math.Clamp(WindowSystem.Scale.X - scale, 0.1f, 1f), Math.Clamp(WindowSystem.Scale.Y - scale, 0.1f, 1f)), WindowSystem.PivotPoint, WindowSystem.PivotPoint, 0.2f);
    }

    public void EndGame() 
    {
        GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://Scenes/UI/Credits.tscn"));
        QueueFree();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        WindowSystem.Position += position;

        position = Vector2.Zero;

        WindowSystem.PumpAnimation(out float t);
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
