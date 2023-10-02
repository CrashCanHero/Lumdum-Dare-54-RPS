using Godot;
using System;

public partial class MainMenu : CenterContainer {
    [Export]
    public RichTextLabel ScoreDisplay;

    public void OnQuitButtonPressed() {
        GetTree().Quit();
    }

    public void OnGoButtonPressed() {
        GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://Scenes/microgame.tscn"));
        MicroGameManager.Froggers = 0u;
        QueueFree();
    }

    public void OnMenuButtonPressed()
    {
        GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://Scenes/UI/Menu.tscn"));
        QueueFree();
    }

    public void ScoreReady() {
        ulong score = SaveSystem.Instance.Load();
        if (score < MicroGameManager.GetFrogs()) 
        {
            score = MicroGameManager.GetFrogs();
        }
        ScoreDisplay.Text = $"[center]{score.ToString()}";
    }
}
