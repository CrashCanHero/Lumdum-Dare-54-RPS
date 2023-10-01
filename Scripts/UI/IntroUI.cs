using Godot;
using System;

public partial class IntroUI : Control {

    [Export]
    public NodePath ButtonPath, IntroScreenPath, WaitScreenPath, TimerPath, WaitScreenLabelPath;

    Button button;
    Control introScreen, waitScreen;
    Timer timer;
    Label label;
    int waitIndex;

    public override void _Ready() {
        button = GetNode<Button>(ButtonPath);
        introScreen = GetNode<Control>(IntroScreenPath);
        waitScreen = GetNode<Control>(WaitScreenPath);
        timer = GetNode<Timer>(TimerPath);
        label = GetNode<Label>(WaitScreenLabelPath);

        button.Pressed += SetupClicked;
    }

    public void SetupClicked() {
        introScreen.Visible = false;
        waitScreen.Visible = true;

        timer.WaitTime = 0.2f;
        timer.Timeout += Timer1Done;
        button.Pressed -= SetupClicked;

        timer.Start();
    }

    void Timer1Done() {
        WindowSystem.SetupWindow();

        timer.Timeout -= Timer1Done;
        timer.WaitTime = 0.5f;
        timer.Timeout += Timer2Done;

        timer.Start();
    }

    void Timer2Done() {
        timer.Timeout -= Timer2Done;

        float time = 1f;
        float crossMoveTime = time * 1.15f;

        int id = WindowAnimator.Instance[0, 0.3f];
        id = WindowAnimator.Instance[1, time];
        id = WindowAnimator.Instance[2, crossMoveTime];
        id = WindowAnimator.Instance[3, time];
        id = WindowAnimator.Instance[4, crossMoveTime / 2f];

        WindowSystem.OnAnimationComplete += AnimComplete;
    }

    void AnimComplete() {
        waitIndex++;

        if (waitIndex == 5) {
            label.Text = "Look Good? Good.";
            label.LabelSettings.FontSize /= 2;
            WindowSystem.OnAnimationComplete -= AnimComplete;

            timer.WaitTime = 2f;
            timer.Timeout += IntroDone;
            timer.Start();
        } else {
            label.Text = waitIndex.ToString();
        }
    }

    void IntroDone() {
        timer.Timeout -= IntroDone;
        
        Control scene = (Control)ResourceLoader.Load<PackedScene>("res://Scenes/UI/Menu.tscn").Instantiate();
        GetParent().AddChild(scene);
        WindowSystem.Scale = Vector2.One;
        WindowSystem.Position = Vector2.Zero;

        QueueFree();
    }
}
