using Godot;

public partial class Standing_platform : MeshInstance3D
{
	public Move_player_zone _Player_Zone = null;

	public override void _Ready()
	{
		_Player_Zone = this.GetChild<Move_player_zone>(1);
	}
}
