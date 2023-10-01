using Godot;
using System;

public partial class MicroGame : Node2D
{
	protected float timescale;

	public void SetTimescale(float time) 
	{
		timescale = time;
	}

	public virtual void Start() 
	{
		//some kind of code here
	}

	protected virtual void Fail() 
	{
		if (MicroGameManager.Instance == null) return;

		MicroGameManager.Instance.FinishGame(false);
	}

    protected virtual void Win() 
	{
        if (MicroGameManager.Instance == null) return;

        MicroGameManager.Instance.FinishGame(true);
    }
}
