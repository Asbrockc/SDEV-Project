using Godot;
using System;
using System.ComponentModel;
using System.Data;

public partial class Obj_enemy_base : Obj_physics_base
{
	public const int IDLE_STATE = 0;
	public const int MOVE_STATE = 1;
	public const int HIT_STATE = 2;
	public const int DEATH_STATE = 3;

	public bool _death_flag = false;
	public int _health = 4;

	public Node3D _target = null;
	private int hit_timer = 0;
	private int delay_timer = 30;

	public float _hit_force = 8.0f;
	public int _hit_damage = 1;

    public override void _Ready()
    {
		base._state = 1;
    }

	public override void _PhysicsProcess(double delta)
	{
		if (_health <= 0)
			_state = DEATH_STATE;

		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (_jump_spd != 0)
		{
			velocity.Y = _jump_spd;
			_jump_spd = 0.0f;
		}

		velocity = this.enemy_core_AI(delta, velocity);

		if (!_death_flag)
		{
			velocity = apply_speed(velocity);
			Velocity = velocity;
			MoveAndSlide();
		}
	}

	public Vector3 enemy_core_AI(double delta, Vector3 velocity)
	{
		switch (_state)
		{
			case IDLE_STATE: 
				velocity = idle_state(delta, velocity);
			break;
			case MOVE_STATE: 
				velocity = move_to_player_state(delta, velocity);
			break;
			case HIT_STATE: 
				velocity = hit_state(delta, velocity);
			break;
			case DEATH_STATE: 
				velocity = death_state(delta, velocity);
			break;

			default:
				_hspd = 0;
				_vspd = 0;
				break;
		}

		return velocity;
	}

	public Vector3 idle_state(double delta, Vector3 velocity)
	{
		_hspd = 0;
		_vspd = 0;

		if (_target != null)
		{
			_state = MOVE_STATE;
		}
		//GD.Print("Will walk to player soon enough");
		return velocity;
	}

	public Vector3 move_to_player_state(double delta, Vector3 velocity)
	{
		if (_target != null)
		{
			if (GLOBAL_FUNCTIONS.distance_between_nodes(_target, this) > 0.1)
			{
				_hspd = Math.Sign(_target.GlobalPosition.X - this.GlobalPosition.X) * Speed/2;
				_vspd = Math.Sign(_target.GlobalPosition.Z - this.GlobalPosition.Z) * Speed/2;
			}
			else
			{
				//_state = 0;
			}
		}

		return velocity;
	}

	public Vector3 hit_state(double delta, Vector3 velocity)
	{
		//GD.Print("hit");
		if (hit_timer <  delay_timer)
		{
			hit_timer++;
		}
		else
		{
			hit_timer = 0;
			_state = IDLE_STATE;
		}
		//GD.Print("Will walk to player soon enough");
		return velocity;
	}

	public void hit_me(Node3D _hit_by, float _hit_force, float _jump_force, int _damage)
	{
		this._state = HIT_STATE;
		this._hspd = _hit_force * -Math.Sign(_hit_by.GlobalPosition.X - GlobalPosition.X);
		this._vspd = _hit_force * -Math.Sign(_hit_by.GlobalPosition.Z - GlobalPosition.Z);
		this._jump_spd = _jump_force;
		this._health -= _damage;
		GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_hit.tscn", false);

		if (_target == null)
			_target = GLOBAL_STATS._player;
	}
	

	public Vector3 death_state(double delta, Vector3 velocity)
	{
		//GD.Print("hit");
		if (hit_timer <  delay_timer)
		{
			hit_timer++;
		}
		else
		{
			hit_timer = 0;
			for (int i = 0; i < 10; i++)
			
			GLOBAL_FUNCTIONS.Spawn_item(this.Position, 0.2f, 2);
			GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_blood.tscn", true);
			QueueFree();
			_death_flag = true;
		}
		//GD.Print("Will walk to player soon enough");
		return velocity;
	}
}
