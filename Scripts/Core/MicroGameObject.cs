using Godot;
using System;

[GlobalClass]
public partial class MicroGameObject : Node2D
{
    public virtual void Start() 
    {
        //Called when the micro game has started
        GD.Print("Base class");
    }
}
