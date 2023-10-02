using Godot;
using System;

public partial class Frogger : Node
{
	[Export]
	public RichTextLabel label;

    public override void _Ready()
    {
        base._Ready();

        WindowSystem.ResetWindow();
        //WindowSystem.Position = new Vector2(0.5f, 0.5f);
        //WindowSystem.PivotPoint = new Vector2(0.5f, 0.5f);

        //WindowSystem.Scale = Vector2.One * 0.5f;
        Input.MouseMode = Input.MouseModeEnum.Visible;

        label.Text = $"[center]You found:\n [color=red]{MicroGameManager.GetFrogs()} Frogs";

        if (SaveSystem.Instance.Load() < MicroGameManager.GetFrogs()) 
        {
            SaveSystem.Instance.Save(MicroGameManager.GetFrogs());
        }
    }
}
