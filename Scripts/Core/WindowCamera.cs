using Godot;
using System;
using System.Runtime.Intrinsics;

public partial class WindowCamera : Camera2D {
    Vector2 position, scale, pivot;

    public override void _Ready() {
        WindowSystem.OnWindowMove += PositionChange;
        WindowSystem.OnWindowScale += ScaleChange;
        WindowSystem.OnWindowPivotChange += PivotChange;
        scale = Vector2.One;
    }

    void PositionChange(Vector2 pos) {
        position = pos;
        UpdateTransform();
    }

    void ScaleChange(Vector2 scale) {
        this.scale = scale;
        UpdateTransform();
    }

    void PivotChange(Vector2 pivot) {
        this.pivot = pivot;
        UpdateTransform();
    }

    void UpdateTransform() {
        GlobalPosition = (position / WindowSystem.PixelScale);
    }
}
