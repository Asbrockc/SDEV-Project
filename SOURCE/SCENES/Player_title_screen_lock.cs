using Godot;

public partial class Player_title_screen_lock : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GLOBAL_STATS._player.Position = this.GlobalPosition;
	}
}
