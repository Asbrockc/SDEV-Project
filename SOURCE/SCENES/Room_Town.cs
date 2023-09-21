using Godot;
using System;

public partial class Room_Town : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GLOBAL_STATS._current_room_reference = this;
	}
}
