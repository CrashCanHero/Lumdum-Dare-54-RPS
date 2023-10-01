using Godot;
using System;

public partial class Window_Test : Node2D {

    Vector2 pos;
    Vector2 size;

    bool set;

    int animID;
    Vector2[] animationPoints = new[] {
        new Vector2(0f, 0f),
        new Vector2(0.5f, 0f),
        new Vector2(0.5f, 0.5f),
        new Vector2(0f, 0.5f)
    };

    public override void _Ready() {
        ((Button)GetNode(new NodePath("Container/SetButton"))).Pressed += Set;
    }

    public override void _Process(double delta) {
        if (!set) {
            return;
        }

        if (WindowSystem.PumpAnimation(out float t)) {
            animID++;

            if (animID >= 4) {
                animID = 0;
            }

            WindowSystem.SetAnimation(WindowSystem.Position, animationPoints[animID], WindowSystem.Scale, WindowSystem.Scale, WindowSystem.PivotPoint, WindowSystem.PivotPoint, 2f);
        }
    }

    public override void _Input(InputEvent @event) {
        if (!set) {
            return;
        }
        
        if (@event is not InputEventMouseMotion) {
            return;
        }

        InputEventMouseMotion evnt = (InputEventMouseMotion)@event;
        Vector2 relative =  new Vector2(
            (evnt.Relative.X * WindowSystem.PixelScale.X),
            (evnt.Relative.Y * WindowSystem.PixelScale.Y)
        );

        pos += relative;
    }

    void Set() {
        WindowSystem.SetupWindow();
        set = true;
        Input.MouseMode = Input.MouseModeEnum.Captured;

        pos = Vector2.Zero;
        size = Vector2.One * 0.5f;

        Reset();

        WindowSystem.SetAnimation(WindowSystem.Position, animationPoints[animID], WindowSystem.Scale, WindowSystem.Scale, WindowSystem.PivotPoint, WindowSystem.PivotPoint, 0.1f);
    }

    void Reset() {
        WindowSystem.Position = pos;
        WindowSystem.Scale = size;
    }
}
