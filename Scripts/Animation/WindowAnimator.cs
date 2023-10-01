using Godot;
using System;
using System.Collections.Generic;

public partial class WindowAnimator : Node2D {
    public static WindowAnimator Instance;

    [Export]
    public WindowAnimationPoint[] Animations { get; private set; }

    Queue<AnimPoint> anims = new Queue<AnimPoint>();

    public delegate void OnAnimationDoneEvent();
    public static event OnAnimationDoneEvent AnimationCompleted;

    AnimPoint currentAnim;


    public int this[int ID, float time] {
        get {
            anims.Enqueue(new AnimPoint() {
                Point = Animations[ID],
                Time = time,
            });

            if (currentAnim == null) {
                currentAnim = anims.Peek();
                SetAnim(currentAnim);
            }

            return anims.Count - 1;
        }
    }

    public override void _Ready() {
        if (Instance != null) {
            QueueFree();
            return;
        }

        Instance = this;
    }

    public override void _Process(double delta) {
        if (WindowSystem.PumpAnimation(out float time) && time != -1f) {
            if (anims.Count <= 0) {
                return;
            }
            currentAnim = anims.Dequeue();
            SetAnim(currentAnim);
            AnimationCompleted?.Invoke();
        }

        if (anims.Count <= 0) {
            currentAnim = null;
        }
    }

    void SetAnim(AnimPoint anim) {
        WindowSystem.SetAnimation(WindowSystem.Position, anim.Point.Position, WindowSystem.Scale, anim.Point.Size, WindowSystem.PivotPoint, anim.Point.PivotPoint, anim.Time);
    }


    class AnimPoint {
        public WindowAnimationPoint Point;
        public float Time;
    }
}
