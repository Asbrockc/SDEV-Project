using Godot;
using System;

public partial class Obj_player_camera : Camera3D
{
	public Node3D _target = null;

	public float _y_dis = 3.0f; //4.0
	public float _z_dis = 3.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GLOBAL_STATS._Camera = this;
		_target = GLOBAL_STATS._player;
		//GLOBAL_SCENE _scene = GetParent<GLOBAL_SCENE>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_target != null)
		{
			float _test = 0;//0.01f;
			Position = new Vector3(
				_target.Position.X + GLOBAL_FUNCTIONS.Choose(-_test,_test), 
				_target.Position.Y + _y_dis + GLOBAL_FUNCTIONS.Choose(-_test,_test), 
				_target.Position.Z + _z_dis + GLOBAL_FUNCTIONS.Choose(-_test,_test));
		}
	}
}
