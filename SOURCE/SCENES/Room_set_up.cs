using Godot;
using System;

public partial class Room_set_up : Node3D
{
	[Flags]
	public enum MyEnum
	{
		Fire = 1 << 1,
		Water = 1 << 2,
		Earth = 1 << 3,
		Wind = 1 << 4,

		// A combination of flags is also possible.
		FireAndWater = Fire | Water,
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GLOBAL_STATS._current_room_reference = this;
	}
}
