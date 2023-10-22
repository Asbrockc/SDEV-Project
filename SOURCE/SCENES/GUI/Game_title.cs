using Godot;

public partial class Game_title : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GetChildCount() > 2)
		{
			GetChild<Button>(0).Disabled = true;
			GetChild<Button>(0).Visible = false;

			GetChild<Button>(1).Disabled = true;
			GetChild<Button>(1).Visible = false;
		}
		else
		{
			GetChild<Button>(0).Disabled = false;
			GetChild<Button>(0).Visible = true;

			GetChild<Button>(1).Disabled = false;
			GetChild<Button>(1).Visible = true;
		}
	}
}
