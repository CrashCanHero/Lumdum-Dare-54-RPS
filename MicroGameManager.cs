using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class MicroGameManager : Node2D
{
    public static MicroGameManager Instance;

    public int playerLives;
    public float gameTimescale = 1f;

    [Export]
    public Node2D root;


    [Export]
    public string[] levels;

    public bool isFinished { get; private set; } = true;

    private MicroGame currentGame;

    public List<MicroGame> gamePool;

    private string lastLevel = string.Empty;

    public override void _Ready()
    {
        if (Instance != null) {
            QueueFree();
            return;
        }

        Instance = this;

        LoadRandomMicroGame();
    }
    public void LoadRandomMicroGame() 
    {
        int random = GD.RandRange(0, levels.Length - 1);

        if (lastLevel == levels[random]) 
        {
            random = GD.RandRange(0, levels.Length);
        }

        LoadMicroGame(levels[random]);

        lastLevel = levels[random];
    }
    public void LoadMicroGame(string name) 
    {
        if (!isFinished) return;
        GD.Print("Loading game");

        Node game = GD.Load<PackedScene>("res://Scenes/MicroGames/" + name + ".tscn").Instantiate();

        if (currentGame != null) {
            currentGame.QueueFree();
        }

        currentGame = game as MicroGame;
        root.AddChild(currentGame);

        isFinished = false;

        currentGame.Start();
    }

    public void FinishGame(bool isWon) 
    {
        if (!isWon) playerLives--;

        isFinished = true;

        gameTimescale += gameTimescale * 0.1f;
    }

    public void StartGame() 
    {
        currentGame.SetTimescale(gameTimescale);
        currentGame.Start();
    }
}