using Godot;
using System;

public partial class MainMenu : CenterContainer {
    public void OnQuitButtonPressed() {
        GetTree().Quit();
    }

    public void OnGoButtonPressed() {
        Node2D game = (Node2D)ResourceLoader.Load<PackedScene>("res://Scenes/microgame.tscn").Instantiate();

        GetParent().AddChild(game);
        QueueFree();
    }
}
