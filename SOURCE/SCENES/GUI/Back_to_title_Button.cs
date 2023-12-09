using Godot;
using System;

public partial class Back_to_title_Button : Button
{
	public void _on_pressed()
	{
		if (GLOBAL_STATS._player._current_menu != null)
			GLOBAL_STATS._player._current_menu = null;
		GLOBAL_STATS._Reset_Stats();
		GLOBAL_FUNCTIONS.Room_Transition("res://SCENES/Title_Screen.tscn", "LOCK_PLAYER", 0 ,0);
	}
}
