using Godot;
using System;

public partial class FrogGame : MicroGame
{
    [Export]
    public MicroGameObject[] objs;

    public override void Start()
    {
        GD.Print("Starting");

        foreach (MicroGameObject obj in objs)
        {
            obj.Start();
            GD.Print("Starting obj");
        }
    }
}
