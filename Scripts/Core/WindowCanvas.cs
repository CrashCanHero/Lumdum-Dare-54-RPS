using Godot;
using System;

public partial class WindowCanvas : CanvasLayer {
    public override void _Ready() {
        WindowSystem.OnWindowScale += ScaleChange;
        ScaleChange(WindowSystem.Scale);
    }

    void ScaleChange(Vector2 newScale) {
        Scale = newScale * 1.33f;
    }

    public override void _ExitTree()
    {
        WindowSystem.OnWindowScale -= ScaleChange;
        base._ExitTree();
    }
}
