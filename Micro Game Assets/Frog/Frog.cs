using Godot;
using System;
using System.Numerics;

public partial class Frog : AnimatedSprite2D
{
    public override void _Process(double delta)
    {
        if (GlobalPosition.DistanceTo(GetGlobalMousePosition() - new Godot.Vector2(0, 150f / 4f)) < 50f)
        {
            if (Input.IsActionJustPressed("Click")) 
            {
                MicroGame.currentGameManager.Win();
                MicroGameManager.Instance.AddTime(1f);
                MicroGameManager.Instance.FindFrog();
            }
        }
    }
}
