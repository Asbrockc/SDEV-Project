using Godot;
using System;

public partial class Dragon_boss_base : Obj_enemy_base
{
	private int counter = 0;
	private int next_jump_in = 400;
	private bool _move_left = true;

    public override void _Ready()
    {
        base._Ready();
   		_slam_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_heavy_slam.wav");
    }

    public override Vector3 enemy_core_AI(double delta, Vector3 velocity)
	{
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (_jump_spd != 0)
		{
			velocity.Y = _jump_spd;
			_jump_spd = 0.0f;
		}

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
			case 4: 
				knight_slam_attack(true);
			break;
			case 5: 
				velocity = jump_state(delta, velocity);
			break;

			default:
				_hspd = 0;
				_vspd = 0;
				break;
		}

		_base = GLOBAL_FUNCTIONS.Entity_Dir(_base, _hspd, _vspd);

		return velocity;
	}

	public Vector3 jump_state(double delta, Vector3 velocity)
	{
		counter++;
		if (IsOnFloor() && counter > 30)
		{
			counter = 0;
			next_jump_in = GLOBAL_FUNCTIONS.Choose(200,400,500);
			_state = 4;
		}

		return velocity;
	}

    public override Vector3 move_to_player_state(double delta, Vector3 velocity)
    {
		_Animator.Play("side_walk");

		if (counter < next_jump_in)
		{
			counter++;
			if (_move_left)
			{
				if (this.GlobalPosition.X > -2)
					_hspd = -1;
				else
					_move_left = false;
			}
			else
			{
				if (this.GlobalPosition.X < 2)
					_hspd = 1;
				else
					_move_left = true;
			}
		}
		else
		{
			shift_to_jump_state(10);
		}

		return velocity;
    }

	private void shift_to_jump_state(float _height)
	{
			counter = 0;
			_hspd = 0;
			_vspd = 0;
			_jump_spd = _height;
			_Animator.Play("jump");
			_state = 5;
	}

}
