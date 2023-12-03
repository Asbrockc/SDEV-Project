using Godot;
using System;

public partial class Game_button_base : Button_base
{
	public int my_x = 0;
	public int my_y = 0;

	public bool game_on = false;

	public Button_game_control _control;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_control = GetParent<Button_game_control>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		

		if (_set && _button._Player_Zone._player == null)
		{
			_set = false;
		}

		if (game_on)
		{

		}

		base._Process(delta);
	}

    public override void _on_press()
    {
		_control.pressed_x = my_x;
		_control.pressed_y = my_y;
		_control.pressed_active = true;
        //base._on_press();
    }
}
