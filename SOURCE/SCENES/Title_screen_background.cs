using Godot;

public partial class Title_screen_background : MeshInstance3D
{
	private Vector3 _spin_speed = new Vector3(0,0.3f,0);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.RotationDegrees += _spin_speed;
	}
}
