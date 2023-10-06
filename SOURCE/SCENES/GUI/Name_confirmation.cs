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
		GLOBAL_FUNCTIONS.Room_Transition("res://SCENES/practice_scene.tscn", "Save_Point", 0, 1);
		GLOBAL_FUNCTIONS.UI_Visibiity(true);
		//GLOBAL_STATS._Camera._target = GLOBAL_STATS._player;

    }
}
