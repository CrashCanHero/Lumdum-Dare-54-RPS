using Godot;
using System;

public partial class FrogObject : MicroGameObject
{
    public override void Start()
    {
        GD.Print("Ribbit");

        AnimatedSprite2D anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        //anim.SpeedScale = MicroGame.currentGameManager.timescale;

        anim.Play("ribbit");

        //MicroGame.currentGameManager.Fail();
    }
}
