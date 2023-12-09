using Godot;

public partial class Title_screen_background : MeshInstance3D
{
	private Vector3 _spin_speed = new Vector3(0,0.3f,0);

	public override void _Process(double delta)
	{
		this.RotationDegrees += _spin_speed;
	}
}
