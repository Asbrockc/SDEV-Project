using Godot;
using System;

public partial class Obj_enemy_base : Obj_physics_base
{
	public const int IDLE_STATE = 0;
	public const int MOVE_STATE = 1;
	public const int HIT_STATE = 2;
	public const int DEATH_STATE = 3;

	public bool _attack_flag = false;

	public AudioStreamWav _damage_sound = null;
	public AudioStreamWav _slam_sound = null;

	public float _distance_from_player = 0.001f;
	public float _shockwave_trigger_frame = 0.3f;

	[Export] public bool _immune_to_sword = false;
	[Export] public bool _immune_to_bow = false;
	[Export] public int _health = 4;
	[Export] public int _max_health = 4;
	[Export] public int drop_amount = 2;
	[Export] public float _hit_force = 8.0f;
	[Export] public int _hit_damage = 1;
	[Export] public bool _apply_grav = true;

	public string[] _drops = {"exp","exp","exp","exp","exp","exp","exp","exp","hp"};
	public string _base = "down_";

	public bool _death_flag = false;

	public Node3D _target = null;
	public int hit_timer = 0;
	public int delay_timer = 30;
	

    public override void _Ready()
    {
		_Animator = this.GetNode("Spr_enemy").GetChild<AnimationPlayer>(0);
		this._damage_sound = GLOBAL_STATS._player._sword_hit;
		//GLOBAL_FUNCTIONS.Play_Sound(this._player_parent._sword_hit);
		base._state = 1;
		this.GetNode<Obj_enemy_hurt_zone>("Obj_enemy_hurt_zone")._enemy_parent = this;
    }

	public override void _PhysicsProcess(double delta)
	{
		if (_health <= 0)
			_state = DEATH_STATE;

		Vector3 velocity = Velocity;

		velocity = this.enemy_core_AI(delta, velocity);

		if (!_death_flag)
		{
			velocity = apply_speed(velocity);
			Velocity = velocity;
			MoveAndSlide();
		}
	}

	public virtual Vector3 enemy_core_AI(double delta, Vector3 velocity)
	{
		if (!IsOnFloor() && _apply_grav)
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

			default:
				_hspd = 0;
				_vspd = 0;
				break;
		}

		_base = GLOBAL_FUNCTIONS.Entity_Dir(_base, _hspd, _vspd);

		return velocity;
	}

	public virtual Vector3 idle_state(double delta, Vector3 velocity)
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

	public virtual Vector3 move_to_player_state(double delta, Vector3 velocity)
	{
		if (_target != null)
		{
			if (_Animator != null)
				_Animator.Play(_base + "walk");
				
			if (GLOBAL_FUNCTIONS.Distance_Between_Nodes(_target, this) > _distance_from_player)
			{

				if (Math.Abs(_target.GlobalPosition.X - this.GlobalPosition.X) > .5)
					_hspd = Math.Sign(_target.GlobalPosition.X - this.GlobalPosition.X) * Speed/2;
				else 
					_hspd = 0;

				if (Math.Abs(_target.GlobalPosition.Z - this.GlobalPosition.Z) > .5)
					_vspd = Math.Sign(_target.GlobalPosition.Z - this.GlobalPosition.Z) * Speed/2;
				else 
					_vspd = 0;
				
			}
			else
				end_move();
		}

		return velocity;
	}

	public virtual Vector3 move_left_right_to_player_state(double delta, Vector3 velocity)
	{
		_attack_flag = false;

		if (_target != null)
		{
			if (_Animator != null)
				_Animator.Play(_base + "walk");
	
			if (Math.Abs(_target.GlobalPosition.X - this.GlobalPosition.X) > _distance_from_player)
			{
				_vspd = 0;
				_hspd = Math.Sign(_target.GlobalPosition.X - this.GlobalPosition.X) * Speed/2;
			}
			else if (Math.Abs(_target.GlobalPosition.Z - this.GlobalPosition.Z) > _distance_from_player)
			{
				_hspd = 0;
				_vspd = Math.Sign(_target.GlobalPosition.Z - this.GlobalPosition.Z) * Speed/2;
			}
			else
				end_move();
		}

		return velocity;
	}

	public virtual void end_move()
	{

	}

	public virtual Vector3 hit_state(double delta, Vector3 velocity)
	{
		if (!_death_flag)
		{
			if (hit_timer <  delay_timer)
			{
				hit_timer++;
			}
			else
			{
				hit_timer = 0;
				_state = IDLE_STATE;
			}
		}

		return velocity;
	}

	public virtual void hit_me(Node3D _hit_by, float _hit_force = 0, float _jump_force = 0, int _damage = 0)
	{
		if (_damage_sound != null)
			GLOBAL_FUNCTIONS.Play_Sound(_damage_sound);

		if (_hit_by != null)
		{
			if (_hit_force > 0)
			{
				this._state = HIT_STATE;
				this._hspd = _hit_force * -Math.Sign(_hit_by.GlobalPosition.X - GlobalPosition.X);
				this._vspd = _hit_force * -Math.Sign(_hit_by.GlobalPosition.Z - GlobalPosition.Z);
			}

			if (_jump_force > 0)
				this._jump_spd = _jump_force;

			this._health -= _damage;
			GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_hit.tscn", false);
		}
		if (_target == null)
			_target = GLOBAL_STATS._player;
	}
	

	public virtual Vector3 death_state(double delta, Vector3 velocity)
	{
		if (hit_timer <  delay_timer)
			hit_timer++;
		else
		{
			hit_timer = 0;
			death_function();
			GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_blood.tscn", true);
			QueueFree();
			_death_flag = true;
		}
		//GD.Print("Will walk to player soon enough");
		return velocity;
	}

	public virtual void death_function()
	{
		//string[] drops = {"exp","exp","exp","exp","exp","exp","exp","exp","hp"};
		for (int i = 0; i < drop_amount; i++)
			GLOBAL_FUNCTIONS.Spawn_item(this.GlobalPosition, 0.2f, 1, _drops);
	}

		///<summary>Knights main attack</summary>
	public void knight_slam_attack(bool _skip = false, float _x = 0, float _y = 0, float _z = 0)
	{
		if ((_Animator.CurrentAnimationPosition > _shockwave_trigger_frame && !_attack_flag) || _skip)
		{
			Obj_projectile_parent _boom = GLOBAL_FUNCTIONS.Create_projectile(this, "res://SCENES/EFFECTS/EFFECT_AREA/Effect_area_effect_parent.tscn");
			Obj_projectile_parent _boom2 = GLOBAL_FUNCTIONS.Create_projectile(this, "res://SCENES/EFFECTS/EFFECT_AREA/Effect_area_effect_parent.tscn");
			_boom2.RotationDegrees = new Vector3(0, 45, 0);

			if (_x != 0 || _y != 0 || _z != 0)
			{
				Vector3 _offset = new (_x, _y, _z);
				_boom.Position += _offset;
				_boom2.Position += _offset;
			}
			
			GLOBAL_FUNCTIONS.Shake_Camera(.3f);

			if (_slam_sound != null)
				GLOBAL_FUNCTIONS.Play_Sound(_slam_sound);

			_attack_flag = true;
		}

		if (!_Animator.IsPlaying() || _skip)
			_state = MOVE_STATE;
	}
}
