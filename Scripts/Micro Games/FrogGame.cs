using Godot;
using System;
using System.Runtime.InteropServices;

public partial class FrogGame : MicroGame
{
    [Export]
    public MicroGameObject[] objs;

    [Export]
    public Node2D[] spawns;

    private static int lastPos = 0;

    public override void Start()
    {
        MicroCam.Shrink(0.02f);

        GD.Print("Starting");

        foreach (MicroGameObject obj in objs)
        {
            obj.Start();
            GD.Print("Starting obj");

            if (obj.GetType() == typeof(FrogObject)) 
            {
                int id = GD.RandRange(0, spawns.Length - 1);

                if (lastPos == id) 
                {
                    id = GD.RandRange(0, spawns.Length - 1);
                }

                obj.Position = spawns[id].Position;

                lastPos = id;
            }
        }
    }
}
