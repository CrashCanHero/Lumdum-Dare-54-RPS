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
    bool NoAnim;


    public int this[int ID, float time, Action onDoneAction = null] {
        get {
            AnimPoint point = new AnimPoint() {
                Point = Animations[ID],
                Time = time,
                OnDoneAction = onDoneAction
            };

            if (NoAnim) {
                point.Time = 0.01f;
                SetAnim(point);
            }

            anims.Enqueue(point);

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

    public void SetInstantMove(bool move) => NoAnim = move;

    void SetAnim(AnimPoint anim) {
        WindowSystem.SetAnimation(WindowSystem.Position, anim.Point.Position, WindowSystem.Scale, anim.Point.Size, WindowSystem.PivotPoint, anim.Point.PivotPoint, anim.Time);
    }


    class AnimPoint {
        public WindowAnimationPoint Point;
        public float Time;
        public Action OnDoneAction;
    }
}
