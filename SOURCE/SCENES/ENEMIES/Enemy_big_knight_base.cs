using Godot;
using System;

public partial class Enemy_big_knight_base : Enemy_Egg_AI
{
	public const int SLAM_STATE = 4;

    public override void _Ready()
    {
		_health = 30;
		_max_health = 30;

		_immune_to_bow = true;
		drop_amount = 10;
        base._Ready();
		_damage_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_hit_metal.wav");
		_slam_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_heavy_slam.wav");
		_distance_from_player = 2.0f;
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
				velocity = move_left_right_to_player_state(delta, velocity);
			break;
			case HIT_STATE: 
				velocity = hit_state(delta, velocity);
			break;
			case DEATH_STATE: 
				velocity = death_state(delta, velocity);
			break;
			case SLAM_STATE: 
				knight_slam_attack(false, 0, -1, 0);
			break;

			default:
				_hspd = 0;
				_vspd = 0;
				break;
		}

		_base = GLOBAL_FUNCTIONS.Entity_Dir(_base, _hspd, _vspd);

		return velocity;
	}

	public override void hit_me(Node3D _hit_by, float _hit_force = 0, float _jump_force = 0, int _damage = 0)
	{
		base.hit_me(_hit_by, 0, 0, _damage);
		if (_target == null)
			_target = GLOBAL_STATS._player;
	}

    public override void end_move()
    {
		_hspd = 0;
		_vspd = 0;
		_Animator.Play("slam_attack");
		_state = SLAM_STATE;
    }
}
