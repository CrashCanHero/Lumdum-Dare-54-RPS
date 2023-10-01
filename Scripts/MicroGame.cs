using Godot;
using System;

[GlobalClass]
public partial class MicroGame : Node2D
{	
	public static MicroGame currentGameManager;

	[Export]
	public int InTransition;

	[Export]
	public float InTransitionTime;

	public float timescale { get; private set; } = 1f;

    public override void _Ready()
    {
		currentGameManager = this;
    }

    public void SetTimescale(float time) 
	{
		timescale = time;
	}

	public virtual void Start() 
	{
		//some kind of code here
	}

	public virtual void Fail() 
	{
		if (MicroGameManager.Instance == null) return;

		MicroGameManager.Instance.FinishGame(false);
	}

    public virtual void Win() 
	{
        if (MicroGameManager.Instance == null) return;

        MicroGameManager.Instance.FinishGame(true);
    }
}
