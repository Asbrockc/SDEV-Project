using Godot;
using System;

public partial class Obj_player_camera : Camera3D
{
	public Node3D _target = null;

	public float _shake = 0;//0.01f;
	public float _y_dis = 3.0f; //4.0
	public float _z_dis = 3.0f;

	public override void _Ready()
	{
		GLOBAL_STATS._Camera = this;
		//_target = GLOBAL_STATS._player;
		//GLOBAL_SCENE _scene = GetParent<GLOBAL_SCENE>();
	}

	public override void _Process(double delta)
	{
		if (_target != null)
		{

			if (_shake - .02 > 0)
				_shake -= .01f;
			else
				_shake = 0;

			Position = new Vector3(
				_target.Position.X + GLOBAL_FUNCTIONS.Choose(-_shake,_shake), 
				_target.Position.Y + _y_dis + GLOBAL_FUNCTIONS.Choose(-_shake,_shake), 
				_target.Position.Z + _z_dis + GLOBAL_FUNCTIONS.Choose(-_shake,_shake));
		}
	}
}
