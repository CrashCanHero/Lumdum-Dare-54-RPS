using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class MicroGameManager : Node2D
{
    public static MicroGameManager Instance;

    public int playerLives;
    public float gameTimescale;

    public bool isFinished { get; private set; }

    private MicroGame currentGame;

    public List<MicroGame> gamePool;

    public override void _Ready()
    {
        if (Instance != null) {
            QueueFree();
            return;
        }

        Instance = this;
    }
    public void LoadMicroGame(string name) 
    {
        if (!File.Exists("res://Scenes/MicroGames/" + name + ".tscn") || !isFinished) return;

        MicroGame game = GD.Load<MicroGame>("res://Scenes/MicroGames/" + name + ".tscn");

        currentGame = game;

        isFinished = false;
    }

    public void FinishGame(bool isWon) 
    {
        if (!isWon) playerLives--;

        isFinished = true;
    }

    public void StartGame() 
    {
        currentGame.SetTimescale(gameTimescale);
        currentGame.Start();
    }
}
