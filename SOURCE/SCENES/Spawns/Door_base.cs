using Godot;
using System;

public partial class Door_base : Area3D
{

	[Export] public String _desination = "";
	[Export] public int _x_off = 0;
	[Export] public int _y_off = 0;

	public String _target_zone = "null";

	public void _on_player_collision(Node3D _node)
	{
		if (_node.IsInGroup("Player") && GLOBAL_FUNCTIONS._transition == null)
		{
			((Obj_player_base_script)_node)._state = 2;

			if (_desination != null)
			{
				GLOBAL_FUNCTIONS.Room_Transition(_desination, this.GetGroups()[0], this._x_off, this._y_off);

				/*
				Room_transition_obj _active_chat = (Room_transition_obj)ResourceLoader.Load<PackedScene>("res://ROOMS/Room_transition_obj.tscn").Instantiate();
				GLOBAL_STATS._main_scene.AddChild(_active_chat);
				GLOBAL_FUNCTIONS._transition = _active_chat;
				_active_chat._old_room = GLOBAL_STATS._current_room_reference;
				_active_chat._room = _desination;
				_active_chat._target_zone = this.GetGroups()[0];
				_active_chat._x_off = this._x_off;
				_active_chat._y_off = this._y_off;*/
			}
		}
	}
}
