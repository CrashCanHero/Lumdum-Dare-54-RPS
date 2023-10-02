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
        QueueFree();
    }

    public void OnMenuButtonPressed()
    {
        GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("res://Scenes/UI/Menu.tscn"));
        QueueFree();
    }

    public void ScoreReady() {
        ScoreDisplay.Text = $"[center]{SaveSystem.Instance.Load()}";
    }
}
