using Godot;

public partial class Locked_door_base : Node3D
{
	public bool _open = false;
	private Standing_platform _left;
	private Standing_platform _right;

	private float _open_size = 0.3f;
	private Vector3 _open_rate = new Vector3(0.01f, 0.0f, 0.0f);

	[Export] public GLOBAL_STATS.FLAG_INDEX _flag = GLOBAL_STATS.FLAG_INDEX.Beat_boss_1;

	public override void _Ready()
	{
		_left = this.GetChild<Standing_platform>(0);
		_right = this.GetChild<Standing_platform>(1);
	}

	public override void _Process(double delta)
	{
		_door_trigger();
		
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
	
	public virtual void _door_trigger()
	{
		this._open = GLOBAL_FUNCTIONS.GetFlag(_flag);
	}
}
