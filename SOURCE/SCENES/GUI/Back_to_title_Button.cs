using Godot;
using System;

public partial class Back_to_title_Button : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_pressed()
	{
		GLOBAL_FUNCTIONS.Room_Transition("res://SCENES/Title_Screen.tscn", "LOCK_PLAYER", 0 ,0);
		//GLOBAL
	}
}
