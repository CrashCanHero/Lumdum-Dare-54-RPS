using Godot;
using System;

public partial class FrogObject : MicroGameObject
{
    private float maxDistance = 2202.19f;
    public override void Start()
    {
        GD.Print("Ribbit");

        AnimatedSprite2D anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        //anim.SpeedScale = MicroGame.currentGameManager.timescale;

        anim.Play("ribbit");

        //MicroGame.currentGameManager.Fail();
    }
    public override void _Process(double delta)
    {
        float distance = GlobalPosition.DistanceTo(GetGlobalMousePosition() - new Godot.Vector2(0, 150f / 4f));

        GetNode<AudioStreamPlayer>("AudioStreamPlayer").VolumeDb = 1f - ((distance / maxDistance) * 10f) * 5f + 1f;

        //GD.Print(MicroGameManager.Froggers);
    }
}
