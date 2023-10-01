using Godot;
using System;

public partial class Button_base : Node3D
{
	public Standing_platform _button;	
	public Vector3 _pressure;

	public bool _set = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_button = this.GetChild<Standing_platform>(0);
		_pressure = new Vector3(0.0f, 0.01f, 0.0f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
		}

		if (_button.Position.Y <= -0.2 && !_set)
		{
			_set = true;
			GLOBAL_FUNCTIONS.Play_Sound(GLOBAL_STATS._player._player_hit);
			_on_press();
		}
	}

	public virtual void _on_press()
	{
		GD.Print("CLICK");
	}
}
