using Godot;
using System;

public partial class Puzzle_room_rotating_path : Path3D
{
	//private Vector3 _rotate;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (Node3D _node in GetParent().GetChildren())
		{
			if (_node.IsInGroup("enem"))
				_node.GetChild<Obj_enemy_base>(0)._target = GLOBAL_STATS._player;
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//this.RotationDegrees += _rotate;
	}
}
