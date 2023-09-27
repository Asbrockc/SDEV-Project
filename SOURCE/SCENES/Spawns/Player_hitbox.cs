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
			GLOBAL_FUNCTIONS.Play_Sound(this._player_parent._sword_hit);
			((Obj_enemy_base)_node)._state = 2;
			((Obj_enemy_base)_node)._hspd = 2.0f * -Math.Sign(this._player_parent.GlobalPosition.X - _node.GlobalPosition.X);
			((Obj_enemy_base)_node)._vspd = 2.0f * -Math.Sign(this._player_parent.GlobalPosition.Z -_node.GlobalPosition.Z);
			((Obj_enemy_base)_node)._jump_spd = 3.5f;
			((Obj_enemy_base)_node)._health -= GLOBAL_STATS._player_stats[GLOBAL_STATS.I_STRENGTH];
			GLOBAL_FUNCTIONS.Create_Effect(_node, "Effect_hit.tscn", false);
		}
	}
}
