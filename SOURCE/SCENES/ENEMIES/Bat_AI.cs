using Godot;
using System;

public partial class Bat_AI : Enemy_Egg_AI
{
	private int _attack_timer = 0; 
	private int _attack_timer_max = 600; 

	private bool _set_base = false;
	private bool _chirp = true;

	public AudioStreamWav _flap_sound = null;
	public AudioStreamWav _chirp_sound = null;
	private Obj_enemy_hurt_zone _hit_zone = null;

	public Vector3 _base_location;
	public Vector3 _off_set = new Vector3(0,.5f,0);
	//public Vector3 _target;
	public float _div = 25.0f;

	private bool _move_to_target = true;
	
    public override void _Ready()
    {
		base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
		if (!_set_base)
		{
			this._chirp_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_bat_chirp.wav");
			this._flap_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_flap_small.wav");
			_apply_grav = false;

			_hit_zone = GetNode<Obj_enemy_hurt_zone>("Obj_enemy_hurt_zone");
			_base_location = this.GlobalPosition;
			_set_base = true;
		}

        base._PhysicsProcess(delta);
    }

    public override Vector3 move_to_player_state(double delta, Vector3 velocity)
	{
		if (_target != null)
		{
			if (_chirp)
			{
				GLOBAL_FUNCTIONS.Play_Sound(_chirp_sound);
				_chirp = false;
			}

			if (_attack_timer < _attack_timer_max)
			{
				//_chirp = true;
				//GD.Print("wait");
				_attack_timer++;
				this.GlobalPosition -= (this.GlobalPosition - _base_location)/_div;
			}
			else
			{
				//GD.Print("attack");
				if (_hit_zone._hit_player)
				{
					_attack_timer = 0;
					_hit_zone._hit_player = false;
				}
				
				this.GlobalPosition -= (this.GlobalPosition - (_target.GlobalPosition - _off_set))/_div;

				if (_Animator != null)
				{
					if (!_Animator.IsPlaying())
					{
						//GD.Print("flap");
						_Animator.Play("down_walk");
						GLOBAL_FUNCTIONS.Play_Sound(_flap_sound);
					}
				}
			}
		}
		else
		{
			this.GlobalPosition -= (this.GlobalPosition - _base_location)/_div;
		}
		
		//if (IsOnFloor())
		//{
			//GLOBAL_FUNCTIONS.Play_Sound(_jump_sound);
			//_jump_spd = 5;
		//}

		//if (_Animator != null)
			//Animator.Play(_base + "walk");
		
		/*
		if (_Animator != null)
		{
			if (!_Animator.IsPlaying())
			{
				_Animator.Play("down_walk");
				GLOBAL_FUNCTIONS.Play_Sound(_flap_sound);
			}
		}*/

		return velocity;

		//return base.move_to_player_state(delta, velocity);
	}

    public override void hit_me(Node3D _hit_by, float _hit_force = 0, float _jump_force = 0, int _damage = 0)
    {
		_attack_timer = 0;
        base.hit_me(_hit_by, _hit_force, _jump_force, _damage);
    }
}
