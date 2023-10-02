using Godot;
using System;
using static Godot.DisplayServer;

public static class WindowSystem {
    public static Vector2 PivotPoint {
        get => pivot;
        set {
            pivot = value;

            //We need to reset the window position if the pivot changes
            //so just setting the positon to itself will call the proper event to do so
            Position = pos;
            OnWindowPivotChange?.Invoke(value);
        }
    }
    static Vector2 pivot;

    public static Vector2 Position {
        get => pos;
        set {
            pos = value;

            Vector2 pivotSize = WindowGetSize() * PivotPoint;

            WindowSetPosition(new Vector2I(
                (int)Mathf.Lerp(positionReference.X - pivotSize.X, positionReference.X + sizeReference.X - pivotSize.X, value.X),
                (int)Mathf.Lerp(positionReference.Y - pivotSize.Y - 50f, positionReference.Y + sizeReference.Y - pivotSize.Y - 50f, value.Y)
            ));
            OnWindowMove?.Invoke(value);
        }
    }
    static Vector2 pos;

    public static Vector2 Scale {
        get => scale;
        set {
            scale = value;
            WindowSetSize(new Vector2I(
                (int)(sizeReference.X * value.X),
                (int)(sizeReference.Y * value.Y)
            ));
            OnWindowScale?.Invoke(value);
        }
    }
    static Vector2 scale;

    public static Vector2 PixelScale { get; private set; }

    public delegate void OnWindowMoveEvent(Vector2 newPosition);
    public static event OnWindowMoveEvent OnWindowMove;

    public delegate void OnWindowScaleEvent(Vector2 newScale);
    public static event OnWindowScaleEvent OnWindowScale;

    public delegate void OnWindowPivotEvent(Vector2 newPivot);
    public static event OnWindowPivotEvent OnWindowPivotChange;

    public delegate void OnAnimationCompleteEvent();
    public static event OnAnimationCompleteEvent OnAnimationComplete;

    static Vector2I positionReference;
    static Vector2I sizeReference;

    static WindowAnimationData AnimData;

    public static void SetupWindow(bool Reset = true) {
        WindowSetMode(WindowMode.Maximized);

        positionReference = WindowGetPosition();
        sizeReference = WindowGetSize();

        Vector2I size = ScreenGetSize();

        PixelScale = new Vector2(
            1f / size.X,
            1f / size.Y
        );

        WindowSetMode(WindowMode.Windowed);
        WindowSetFlag(WindowFlags.Borderless, true);

        if (Reset) {
            ResetWindow();
        }
    }

    public static void ResetWindow() {
        WindowSetPosition(positionReference);
        WindowSetSize(sizeReference);
    }

    public static void SetAnimation(Vector2 startPos, Vector2 endPos, Vector2 startSize, Vector2 endSize, Vector2 startPivot, Vector2 endPivot, float time) {
        WindowAnimationData data = new WindowAnimationData(){
            StartPosition = startPos,
            EndPosition = endPos,
            StartSize = startSize,
            EndSize = endSize,
            StartPivot = startPivot,
            EndPivot = endPivot,
            Time = time,
            StartTime = Time.GetUnixTimeFromSystem(),
        };

        AnimData = data;
    }

    public static bool PumpAnimation(out float time) {
        if (AnimData == null) {
            time = -1f;
            return true;
        }

        WindowAnimationData dta = AnimData;

        float t = (float)(Time.GetUnixTimeFromSystem() - dta.StartTime) / dta.Time;

        if (t >= 1f) {
            OnAnimationComplete?.Invoke();
            AnimData = null;
            time = 1f;
            return true;
        }

        Position = new Vector2 (
            Mathf.Lerp(dta.StartPosition.X, dta.EndPosition.X, t),
            Mathf.Lerp(dta.StartPosition.Y, dta.EndPosition.Y, t)
        );

        Scale = new Vector2 (
            Mathf.Lerp(dta.StartSize.X, dta.EndSize.X, t),
            Mathf.Lerp(dta.StartSize.Y, dta.EndSize.Y, t)            
        );

        PivotPoint = new Vector2 (
            Mathf.Lerp(dta.StartPivot.X, dta.EndPivot.X, t),
            Mathf.Lerp(dta.StartPivot.Y, dta.EndPivot.Y, t)            
        );

        time = t;
        return false;
    }

    public static void ForceWindowUpdate() {
        PivotPoint = PivotPoint;
        Scale = Scale;
    }

    class WindowAnimationData {
        public double StartTime;

        public float Time;

        public Vector2 StartPosition, EndPosition;
        public Vector2 StartSize, EndSize;
        public Vector2 StartPivot, EndPivot;
    };
}
