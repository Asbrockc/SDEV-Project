using Godot;

public partial class Path_follower_back_an_forth : PathFollow3D
{
	private int _forward = 0;

	private Vector3 _curr_zone;
	private Move_player_zone _platform;

	public bool _active = true;

	private float _rate = 1.0f;
	[Export] private float _div = 1000.0f;

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

		if (Input.IsKeyPressed(Key.J))
		{
			_active = !_active;
		}
		if (_active)
		{
			switch (_forward)
			{
				case 0:
					moveforward();
				break;
				case 1:
					moveback();
				break;
			}
		}

		if (_platform._player != null)
			_platform._player.GlobalPosition -= (_curr_zone - GlobalPosition);
	}

	private void moveforward()
	{
		float _new_rate = _rate/_div;
			if (this.ProgressRatio + _new_rate < 1)
				this.ProgressRatio += _new_rate;
			else
			{
				this.ProgressRatio = 1;
				_forward = 1;
			}
	}

	private void moveback()
	{
		float _new_rate = _rate/_div;
			if (this.ProgressRatio - _new_rate > 0)
				this.ProgressRatio -= _new_rate;
			else
			{
				this.ProgressRatio = 0;
				_forward = 0;
			}
	}
}
