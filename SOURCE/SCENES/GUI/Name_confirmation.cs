using Godot;
using System;

public partial class Name_confirmation : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Pressed()
    {

		GLOBAL_STATS._player_name = this.GetParent<LineEdit>().Text;

        Room_transition_obj _active_chat = (Room_transition_obj)ResourceLoader.Load<PackedScene>("res://ROOMS/Room_transition_obj.tscn").Instantiate();
		GLOBAL_STATS._main_scene.AddChild(_active_chat);
		_active_chat._room = "res://SCENES/practice_scene.tscn";
		_active_chat._target_zone = "Save_Point";
		_active_chat._x_off = 0;
		_active_chat._y_off = 1;

    }
}
