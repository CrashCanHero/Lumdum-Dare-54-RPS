using Godot;
using System;

public partial class IntroUI : Control {

    [Export]
    public NodePath ButtonPath, IntroScreenPath, WaitScreenPath, TimerPath, WaitScreenLabelPath, WaitText, TessSpin, AudioStreamPlayerPath;

    [Export]
    public AudioStream LowBoop, HighBoop;

    [Export]
    public Vector2[] Corners;

    Button button;
    Control introScreen, waitScreen;
    Timer timer;
    Control waitText, tess;
    AudioStreamPlayer audioPlayer;
    int waitIndex = 0;

    public override void _Ready() {
        button = GetNode<Button>(ButtonPath);
        introScreen = GetNode<Control>(IntroScreenPath);
        waitScreen = GetNode<Control>(WaitScreenPath);
        timer = GetNode<Timer>(TimerPath);
        waitText = GetNode<Control>(WaitText);
        tess = GetNode<Control>(TessSpin);
        audioPlayer = GetNode<AudioStreamPlayer>(AudioStreamPlayerPath);
        button.Pressed += SetupClicked;
    }

    public void SetupClicked() {
        introScreen.Visible = false;
        waitScreen.Visible = true;

        WindowSystem.SetupWindow(false);

        timer.OneShot = false;
        timer.Timeout += TimerDone;
        timer.Start();
        audioPlayer.Stream = LowBoop;
    }

    void TimerDone() {
        waitText.Visible = false;
        tess.Visible = true;
        WindowSystem.Scale = Vector2.One * 0.5f;
        NextWindowPosition(waitIndex);
        waitIndex++;
    }

    void NextWindowPosition(int animID) {
        if (animID >= 4) {
            audioPlayer.Stream = HighBoop;
            audioPlayer.Play();
            timer.WaitTime = 1f;
            WindowSystem.PivotPoint = Vector2.One * 0.5f;
            WindowSystem.Position = WindowSystem.PivotPoint;
            timer.Timeout -= TimerDone;
            timer.Timeout += IntroDone;
            return;
        }
        WindowSystem.Position = Corners[animID];
        audioPlayer.Play();
    }

    void IntroDone() {   
        WindowSystem.PivotPoint = Vector2.Zero;
        WindowSystem.Position = Vector2.One * 0.8f;
        WindowSystem.Scale = Vector2.One * 0.6f;
        Control scene = (Control)ResourceLoader.Load<PackedScene>("res://Scenes/UI/Menu.tscn").Instantiate();
        GetParent().AddChild(scene);
        WindowSystem.Scale = Vector2.One;
        WindowSystem.Position = Vector2.Zero;

        QueueFree();
    }
}
