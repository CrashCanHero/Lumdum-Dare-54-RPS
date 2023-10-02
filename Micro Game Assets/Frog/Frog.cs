using Godot;

using System;
using System.Numerics;

public partial class Frog : AnimatedSprite2D
{
    bool picked;
    float timer = 0.5f;
    public override void _Process(double delta)
    {
        if (picked) timer -= (float)delta;
        if (GlobalPosition.DistanceTo(GetGlobalMousePosition() - new Godot.Vector2(0, 150f / 4f)) < 50f)
        {
            if (Input.IsActionJustPressed("Click"))
            {
                picked = true;

                //anim
                GetParent().GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("found");
                MicroCam.Shrink(0.02f);
            }
        }

        if (picked && timer <= 0)
        {
            MicroGame.currentGameManager.Win();
            MicroGameManager.Instance.AddTime(1.5f);
            MicroGameManager.Instance.FindFrog();
        }
    }
}
