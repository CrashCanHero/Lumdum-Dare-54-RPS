using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class MicroGameManager : Node2D
{
    public static MicroGameManager Instance;

    public int playerLives;
    public int lastPlayerLives;
    public float gameTimescale = 1f;

    private float Time = 30f;
    [Export]
    public Label timer;

    [Export]
    public Control ui;

    [Export]
    public AudioStreamPlayer aud;

    public int score;

    public static ulong Froggers;

    private bool isOver;

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

        isOver = true;

        if (Instance != null) {
            GD.Print("Goodbye");
            QueueFree();
            return;
        }

        Instance = this;

        LoadRandomMicroGame();
    }
    public override void _Process(double delta)
    {
        #if DEBUG
        if (Input.IsKeyPressed(Key.I)) {
            Time = 0f;
        }
        #endif

        if (Time <= 0f && !isOver) 
        {
            GameOver();
            return;
        }

        if (isOver) return;
        
        Time -= (float)delta;
        timer.Text = "Time: " + (int)Time;
    }

    public void AddTime(float time) => Time += time;
    public void FindFrog() => Froggers++;

    public static ulong GetFrogs()
    {
        return Froggers;
    }

    public void LoadRandomMicroGame() 
    {
        int random = GD.RandRange(0, levels.Length - 1);

        if (lastLevel == levels[random]) 
        {
            random = GD.RandRange(0, levels.Length - 1);
        }

        LoadMicroGame(levels[random]);

        lastLevel = levels[random];
    }
    public int GetHealthDifference() 
    {
        return playerLives - lastPlayerLives;
    }
    public void LoadMicroGame(string name) 
    {
        isOver = false;
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

    public void GameOver() 
    {
        //code here
        Instance = null;
        currentGame.QueueFree();
        isOver = true;
        aud.Stop();
        ui.Hide();

        MicroCam.Instance.EndGame();
    }

    public void FinishGame(bool isWon) 
    {
        if (!isWon) 
        {
            lastPlayerLives = playerLives;
            playerLives--;
        }

        isFinished = true;

        gameTimescale += gameTimescale * 0.1f;

        LoadRandomMicroGame();
    }

    public void StartGame() 
    {
        currentGame.SetTimescale(gameTimescale);
        currentGame.Start();
    }
}
