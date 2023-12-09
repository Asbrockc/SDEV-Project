using Godot;

public partial class Player_title_screen_lock : Node3D
{
	private Vector3 _basic_camera_position = new Vector3(0,8,8);

	public override void _Ready()
	{
		GLOBAL_STATS._main_scene.GetNode<Control>("UI_Player_stats").Visible = false;
		rollback_camera();
	}


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
