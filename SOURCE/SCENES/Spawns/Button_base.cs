using Godot;

public partial class Button_base : Node3D
{
	public Standing_platform _button;	
	public Vector3 _pressure;

	public bool _set = false;

	public bool _pressed = false;

	[Export] public GLOBAL_STATS.FLAG_INDEX _flag = GLOBAL_STATS.FLAG_INDEX.Beat_boss_1;


	public override void _Ready()
	{
		_button = this.GetChild<Standing_platform>(0);
		_pressure = new Vector3(0.0f, 0.01f, 0.0f);
	}

	public override void _Process(double delta)
	{
		if (_button._Player_Zone._player != null)
		{
			//GD.Print("Standing on button.");
			_button.Position -= _pressure;
		}
		else 
		{
			if (_button.Position.Y + _pressure.Y < 0.2 && !_set)
				_button.Position += _pressure;
			else
				_pressed = false;
		}

		if (_button.Position.Y <= -0.2 && !_set)
		{
			AudioStreamMP3 _button_sound = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_button_jump.mp3");
			
			
			if (!_pressed)
			{
				_set = true;
				GLOBAL_FUNCTIONS.Play_Sound(_button_sound);
				_on_press();
				_pressed = true;
			}
		}
	}

	public virtual void _on_press()
	{
		GLOBAL_FUNCTIONS.Shake_Camera(1);
		GLOBAL_STATS._completion_flags[(int)_flag] = true;
	}
}
