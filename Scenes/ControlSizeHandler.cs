using Godot;
using System;

public partial class ControlSizeHandler : Control {
    public override void _Process(double delta) {
        Vector2I size = DisplayServer.WindowGetSize();
        
        GetNode<Control>("Label").Scale = WindowSystem.Scale;
        GetNode<Control>("TextureRect").Position = (Vector2)size / 2f;
    }
}
