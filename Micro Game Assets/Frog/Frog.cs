using Godot;
using System;

public partial class Frog : AnimatedSprite2D
{
    public override void _Process(double delta)
    {
        Position = DisplayServer.ScreenGetSize(DisplayServer.WindowGetCurrentScreen());
        GD.Print("Mouse - " + GetGlobalMousePosition());
        GD.Print("This - " + GlobalPosition);

    }
}
