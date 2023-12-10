using Godot;
using System;

public partial class Obj_enemy_hurt_zone : Area3D
{
	public Obj_enemy_base _enemy_parent = null;
	public bool _hit_player = false;

	public void _player_hurt_zone(Node3D _node)
	{
		if (_node.IsInGroup("Player") && _enemy_parent != null)
		{
			_hit_player = true;
			((Obj_player_base_script)_node)._hurt_hspd = _enemy_parent._hit_force * -Math.Sign(_enemy_parent.GlobalPosition.X - _node.GlobalPosition.X);
			((Obj_player_base_script)_node)._hurt_vspd = _enemy_parent._hit_force * -Math.Sign(_enemy_parent.GlobalPosition.Z -_node.GlobalPosition.Z);
			((Obj_player_base_script)_node)._hurt_up_speed = _enemy_parent._hit_force/2;
			if (_enemy_parent._hit_damage > 0)
			{
				GLOBAL_FUNCTIONS.Create_Effect(_node, "Effect_hit.tscn", false);
				GLOBAL_FUNCTIONS.Play_Sound(((Obj_player_base_script)_node)._player_hit);
				if (GLOBAL_STATS._player._hit_timer == 0)
				{
					GLOBAL_STATS._player_stats[GLOBAL_STATS.I_HEALTH] -= _enemy_parent._hit_damage;
					GLOBAL_STATS._player._hit_timer = 20;
				}
			}
		}
	}
}
