using Godot;
using System;

public partial class WindowViewport : CanvasLayer {

    [Export]
    public NodePath ViewportPath, ContainerPath, NodeParentPath;

    SubViewport viewport;
    SubViewportContainer container;
    Node2D nodeParent;

    public override void _Ready() {
        viewport = GetNode<SubViewport>(ViewportPath);
        container = GetNode<SubViewportContainer>(ContainerPath);
        nodeParent = GetNode<Node2D>(NodeParentPath);
    }

    public override void _Process(double delta) {
        Vector2 screenSize = new Vector2(1f / WindowSystem.PixelScale.X, 1f / WindowSystem.PixelScale.Y);
        viewport.Size = (Vector2I)(screenSize * WindowSystem.Scale);
        //container.Position = -(screenSize * WindowSystem.Position);
        nodeParent.Position = -(screenSize * WindowSystem.Position);
    }
}
