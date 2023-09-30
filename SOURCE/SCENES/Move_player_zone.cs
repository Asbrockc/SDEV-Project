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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_player != null)
		{
			//GD.Print(_position_difference);
			//Constant
			_player.Position -= _position_difference;
		}
	}

	public void _stick_on_touch(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
		{
			//GLOBAL_STATS._main_scene.RemoveChild(_node);
			//this.AddChild(_node);
			_player = (Obj_player_base_script)_node;
		}
	}

	public void _unstick_on_leave(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
		{
			_player = null;
		}
	}
}
