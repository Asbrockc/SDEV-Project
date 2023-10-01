using Godot;
using System;

public partial class Path_follower_back_an_forth : PathFollow3D
{
	private int _forward = 0;

	private Vector3 _curr_zone;
	private Move_player_zone _platform;

	private float _rate = 0.0002f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_platform = this.GetChild<Standing_platform>(0)._Player_Zone;
		_curr_zone = _platform.GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_curr_zone = GlobalPosition;

		switch (_forward)
		{
			case 0:
				moveforward();
			break;
			case 1:
				moveback();
			break;
		}

		if (_platform._player != null)
			_platform._player.GlobalPosition -= (_curr_zone - GlobalPosition);


	}

	private void moveforward()
	{
			if (this.ProgressRatio + _rate < 1)
				this.ProgressRatio += _rate;
			else
			{
				this.ProgressRatio = 1;
				_forward = 1;
			}
	}

	private void moveback()
	{
			if (this.ProgressRatio - _rate > 0)
				this.ProgressRatio -= _rate;
			else
			{
				this.ProgressRatio = 0;
				_forward = 0;
			}
	}
}
