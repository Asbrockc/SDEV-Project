using Godot;
using System;

public partial class Puzzle_room_rotating_path : Path3D
{
	private Vector3 _rotate;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rotate = new Vector3(0,0.1f,0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.RotationDegrees += _rotate;
	}
}
