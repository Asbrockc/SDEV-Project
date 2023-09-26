using Godot;
using System;
using System.ComponentModel;
using System.Data;

public partial class Obj_enemy_base : Obj_physics_base
{
	public bool _death_flag = false;
	public int _health = 4;

	public Node3D _target = null;
	private int hit_timer = 0;
	private int delay_timer = 30;

    public override void _Ready()
    {
		base._state = 1;
    }

	public override void _PhysicsProcess(double delta)
	{
			if (_health <= 0)
				_state = 3;

			Vector3 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
				velocity.Y -= gravity * (float)delta;

			// Handle Jump.
			if (_jump_spd != 0)
			{
				GD.Print("jump");
				velocity.Y = _jump_spd;
				_jump_spd = 0.0f;
			}

			//if (_target != null) GD.Print(_target); else GD.Print("N/A");
			switch (_state)
			{
				case 0: 
					velocity = idle_state(delta, velocity);
				break;
				case 1: 
					velocity = move_to_player_state(delta, velocity);
				break;
				case 2: 
					velocity = hit_state(delta, velocity);
				break;
				case 3: 
					velocity = death_state(delta, velocity);
				break;

				default:
					_hspd = 0;
					_vspd = 0;
					break;
			}

		//death flag to flag if the instance was destroyed before this step
		if (!_death_flag)
		{
			velocity = apply_speed(velocity);
			Velocity = velocity;
			MoveAndSlide();
		}
	}

	public Vector3 idle_state(double delta, Vector3 velocity)
	{
		_hspd = 0;
		_vspd = 0;

		if (_target != null)
		{
			_state = 1;
		}
		//GD.Print("Will walk to player soon enough");
		return velocity;
	}

	public Vector3 move_to_player_state(double delta, Vector3 velocity)
	{
		if (_target != null)
		{
			if (GLOBAL_FUNCTIONS.distance_between_nodes(_target, this) > 3)
			{
				_hspd = Math.Sign(_target.GlobalPosition.X - this.GlobalPosition.X) * Speed/2;
				_vspd = Math.Sign(_target.GlobalPosition.Z - this.GlobalPosition.Z) * Speed/2;
			}
			else
			{
				_state = 0;
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
			_state = 0;
		}
		//GD.Print("Will walk to player soon enough");
		return velocity;
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
				GLOBAL_FUNCTIONS.Spawn_item(this.Position);
			
			Free();
			_death_flag = true;
		}
		//GD.Print("Will walk to player soon enough");
		return velocity;
	}
}
