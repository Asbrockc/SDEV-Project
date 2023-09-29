using Godot;
using System;

public partial class Obj_enemy_search_area : Area3D
{
	Obj_enemy_base _enemy_base;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_enemy_base = GetParent<Obj_enemy_base>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _enemy_search(Node3D node)
	{
		if (node.IsInGroup("Player"))
		{
			if (_enemy_base._target == null)
			{
				_enemy_base._target = node;
			}
		}
	}

	public void _enemy_lost_track(Node3D node)
	{
		if (_enemy_base._target != null && node.IsInGroup("Player"))
		{
			//_enemy_base._target = null;
			//_enemy_base._hspd = 0;
			//_enemy_base._vspd = 0;
		}
		

	}
}
