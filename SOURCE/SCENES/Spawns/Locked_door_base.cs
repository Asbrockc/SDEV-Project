using Godot;
using System;

public partial class Locked_door_base : Node3D
{
	public bool _open = false;
	private Standing_platform _left;
	private Standing_platform _right;

	private float _open_size = 0.3f;
	private Vector3 _open_rate = new Vector3(0.01f, 0.0f, 0.0f);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_left = this.GetChild<Standing_platform>(0);
		_right = this.GetChild<Standing_platform>(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this._open = GLOBAL_STATS._completion_flags[(int)GLOBAL_STATS.FLAG_INDEX.Locked_door_1];
		//if (Input.IsActionJustPressed("ui_down"))
			//_open = !_open;

		if (_open)
		{
			if (_right.Position.X < _open_size*2)
				_right.Position += _open_rate;
		}
		else
		{
			if (_right.Position.X > _open_size)
				_right.Position -= _open_rate;
		}

		_left.Position = -_right.Position;
	}
}
