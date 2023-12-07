using Godot;
using System;
using System.Collections.Generic;

public partial class Mage_AI : Enemy_Egg_AI
{
	private int shift_timer = 0;
	private int appear_timer = 200;

	Enemy_spawner[] _spawn_points;

	private bool _attacking = true;

	private int micro_state = 0;

	private Vector3 _away = new Vector3(-1000,-1000,-1000);

    public override void _Ready()
    {
		int i = 0;
		foreach (Node3D _node in this.GetParent().GetParent().GetChildren())
		{
			if (_node.IsInGroup("enemy_spawn"))
				i++;
		}

		_spawn_points = new Enemy_spawner[i];
		
		i = 0;
		foreach (Node3D _node in this.GetParent().GetParent().GetChildren())
		{
			if (_node.IsInGroup("enemy_spawn"))
				_spawn_points[i++] = (Enemy_spawner)_node;
		}

        base._Ready();
    }

    public override Vector3 move_to_player_state(double delta, Vector3 velocity)
	{
		switch (micro_state)
		{
			case 0:
				holdoff();
			break;
			case 1:
				attack();
			break;	
			case 2:
				warper();
			break;
		}
		

		return velocity;
	}

	private void holdoff()
	{
		if (shift_timer == 0)
		{
			_attacking = !_attacking;

			if (_attacking)
			{
				Random random = new Random();
				int randomNumber = random.Next(0, _spawn_points.Length);
				this.GlobalPosition = _spawn_points[randomNumber].GlobalPosition;
				Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);
			}
			else
			{
				this.GlobalPosition = _away;
			}
		}

		if (shift_timer < appear_timer)
		{
			shift_timer++;
			_Animator.Play("idle");
		}
		else
			micro_state = 1;
	}

	private void attack()
	{
		if (shift_timer > 0)
		{
			shift_timer = 0;
			_Animator.Play("summon");
		}
		
		if (!_Animator.IsPlaying())
		{
			if (_attacking)
			{
				Obj_projectile_fireball _arrow = (Obj_projectile_fireball)GLOBAL_FUNCTIONS.Create_projectile(this, "res://SCENES/EFFECTS/EFFECT_PROJECTILES/Obj_projectile_fireball.tscn");
				
				if (_arrow != null)
				{
					_arrow._base_location = this.GlobalPosition;
					_arrow._target_location = GLOBAL_STATS._player.GlobalPosition;
					_arrow._parent = this;
				}
				//GD.Print("SHOT FROM --" + this.ToString());
				GLOBAL_FUNCTIONS.Play_Sound(GLOBAL_STATS._player._player_bow_shot, 0.9f);
			}
			micro_state = 2;
		}
	}

	private void warper()
	{
		if (shift_timer < appear_timer/4)
		{
			shift_timer++;
			_Animator.Play("idle");
		}
		else
		{
			Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);
			//micro_state = 1;
			shift_timer = 0;
			micro_state = 0;
		}
	}
}


