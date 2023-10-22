using Godot;

public partial class Standing_platform : MeshInstance3D
{
	public Move_player_zone _Player_Zone = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Player_Zone = this.GetChild<Move_player_zone>(1);
	}
}
