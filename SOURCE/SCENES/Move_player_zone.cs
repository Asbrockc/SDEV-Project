using Godot;
using System;

public partial class Move_player_zone : Area3D
{
	public Obj_player_base_script _player = null;

	public Vector3 _prior_position;
	public Vector3 _position_difference;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_prior_position = this.Position;
	}

	public void _stick_on_touch(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
			_player = (Obj_player_base_script)_node;
	}

	public void _unstick_on_leave(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
			_player = null;
	}
}
