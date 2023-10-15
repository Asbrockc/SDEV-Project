using Godot;
using System;

public partial class Player_hitbox : Area3D
{
	public bool _active = true;
	public Obj_player_base_script _player_parent;

	public void _hit_somthing(Node3D _node)
	{
		if (_node.IsInGroup("Enemy_hit_zone"))
		{
			if (!((Obj_enemy_base)_node)._immune_to_sword)
			{
				((Obj_enemy_base)_node).hit_me(this._player_parent, 2.0f, 3.5f, GLOBAL_STATS._player_stats[GLOBAL_STATS.I_STRENGTH]);
			}
			
			//GD.Print(GLOBAL_STATS._player_stats[GLOBAL_STATS.I_STRENGTH]);
		}
	}
}
