using Godot;

public partial class Player_title_screen_lock : Node3D
{
	private Vector3 _basic_camera_position = new Vector3(0,8,8);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GLOBAL_STATS._main_scene.GetNode<Control>("UI_Player_stats").Visible = false;
		rollback_camera();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		rollback_camera();
		GLOBAL_STATS._player.Position = this.GlobalPosition;
	}

	private void rollback_camera()
	{
		GLOBAL_STATS._Camera._target = null;
		GLOBAL_STATS._Camera.Position = _basic_camera_position;
	}
}
