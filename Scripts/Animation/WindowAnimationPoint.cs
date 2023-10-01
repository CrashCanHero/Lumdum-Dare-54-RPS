using Godot;
using System;

[GlobalClass]
public partial class WindowAnimationPoint : Resource {
    [Export]
    public Vector2 Position, Size, PivotPoint;
}
