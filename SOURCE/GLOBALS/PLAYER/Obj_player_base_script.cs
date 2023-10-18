using Godot;
using System;
using System.Data;
using System.Runtime.Versioning;

public partial class Obj_player_base_script : Obj_physics_base
{

	public GLOBAL_SCENE _Global_accessor;
	public Interactive_Action _node_in_range = null;
	public Level_up_menu _current_menu = null;

	public AudioStreamWav _sword_hit = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_sword_hit.wav");
	public AudioStreamWav _sword_swing = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_sword_swing.wav");
	public AudioStreamWav _item_pickup = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_item_pickup.wav");
	public AudioStreamWav _exp_pickup = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_exp_pickup.wav");
	
	public AudioStreamWav _level_up = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_level_up.wav");
	public AudioStreamWav _player_hit = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_player_hit.wav");
	public AudioStreamMP3 _player_bow_shot = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_bow_shot_2.mp3");
	
	public string _base = "down_";
	
	public Player_hitbox _hitbox = null;
	private float _hitbox_range = 0.5f;

	public float _hurt_hspd = 0;
	public float _hurt_vspd = 0;	
	public float _hurt_up_speed = 0;

	private bool _shoot_arrow = false;
	private bool _jumped = false;
	private float _arrow_speed = 0.3f;

	public override void _Ready()
	{
		GLOBAL_STATS._player = this;
		_Global_accessor = this.GetParent<GLOBAL_SCENE>();

		//this._Animator.GetParent<Obj_player_sprite>().

	}

	public override void _PhysicsProcess(double delta)
	{
		//GD.Print(GetTree().CurrentScene.Name);
		if (Input.IsActionJustPressed("ui_right"))
		{
			//GLOBAL_STATS._Load_Game();
			//GLOBAL_STATS._Save_Game();
			//GLOBAL_FUNCTIONS.Spawn_enemy(Position);

			/*
			GLOBAL_FUNCTIONS.Change_Music(GLOBAL_FUNCTIONS.Choose(
				"res://SOUNDS/ALL_SOUNDS/MUSIC/snd_dunguon_theme.wav",
				"res://SOUNDS/ALL_SOUNDS/MUSIC/snd_forest_theme.wav",
				"res://SOUNDS/ALL_SOUNDS/MUSIC/snd_town_theme.wav"
				));*/

			//GD.Print(GLOBAL_STATS._current_room_reference.GetTree().CurrentScene.SceneFilePath);
		}

		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;
		else
		{

		}

		switch (_state)
		{
			case 0:
				velocity = Player_move_state(delta, velocity);
				break;
			case 1:
				velocity = Player_attack_state(delta, velocity);
				break;
			case 2:
				velocity = Player_interact_state(delta, velocity);
				break;
			case 3:
				velocity = Player_aim_state(delta, velocity);
				break;
			case 4:
				velocity = Player_shoot_state(delta, velocity);
				break;
		}

		velocity = manage_hurt_velocity(velocity);

		Velocity = velocity;
		MoveAndSlide();
	}

	private Vector3 manage_hurt_velocity(Vector3 velocity)
	{
		velocity.X += _hurt_hspd;
		velocity.Z += _hurt_vspd;

		if (_hurt_up_speed > 0)
		{
			velocity.Y = _hurt_up_speed;
			_hurt_up_speed = 0;
		}

		if (Math.Abs(_hurt_hspd) > .2)
			_hurt_hspd = GLOBAL_FUNCTIONS.Gradual_Stop(_hurt_hspd, 20);
		else
			_hurt_hspd = 0;

		if (Math.Abs(_hurt_vspd) > .2)
			_hurt_vspd = GLOBAL_FUNCTIONS.Gradual_Stop(_hurt_vspd, 20);
		else
			_hurt_vspd = 0;

		return velocity;
	}

	private Vector3 Player_jump_handle(double delta, Vector3 velocity)
	{
		if (Input.IsKeyPressed(Key.Space) && IsOnFloor())
		{
			_jumped = false;
			velocity.Y = JumpVelocity;
		}
		else if (!_jumped)
		{
			_jumped = true;
			_Animator.Play(_base + "jump");
		}
	
		return velocity;
	}


	public Vector3 Player_move_state(double delta, Vector3 velocity)
	{
		_shoot_arrow = false;
		if (Input.IsKeyPressed(Key.Enter) && _current_menu == null)
			_current_menu = GLOBAL_FUNCTIONS.Summon_stat_menu();

		velocity = Player_jump_handle(delta, velocity);
		
		if (Input.IsActionJustReleased("ui_accept") && velocity.Y > 0)
			velocity.Y = 0;

		if (Input.IsKeyPressed(Key.W))
			_vspd = -Speed;
		else if (Input.IsKeyPressed(Key.S))
			_vspd = Speed;
		else
			_vspd = 0;

		if (Input.IsKeyPressed(Key.A))
			_hspd = -Speed;
		else if (Input.IsKeyPressed(Key.D))
			_hspd = Speed;
		else
			_hspd = 0;


		velocity = apply_speed(velocity);

		if (IsOnFloor())
		{
			string _motion;

			_base = GLOBAL_FUNCTIONS.Entity_Dir(_base, _hspd, _vspd);

			if (_vspd == 0 && _hspd == 0)
				_motion = "idle";
			else
				_motion = "walk";

			_Animator.Play(_base + _motion);

			handle_action_key();
			
			if (Input.IsActionJustPressed("ui_left"))
			{
				_state = 4; //or 3
			}
		}

		return velocity;
	}



	private void handle_action_key()
	{
		if (Input.IsKeyPressed(Key.Up))
		{
			if (_node_in_range == null)
			{
				GLOBAL_FUNCTIONS.Play_Sound(_sword_swing);
				_state = 1;

				if (_hitbox == null)
				{
					_hitbox = (Player_hitbox)ResourceLoader.Load<PackedScene>("res://SCENES/Spawns/Player_hitbox.tscn").Instantiate();
					AddChild(_hitbox);
					_hitbox._player_parent = this;

					switch (_base)
					{
						case "right_":
							_hitbox.Position = new Vector3(_hitbox_range,0.0f,-_hitbox_range/2);
						break;
						case "left_":
							_hitbox.Position = new Vector3(-_hitbox_range,0.0f,-_hitbox_range/2);
						break;
						case "down_":
							_hitbox.Position = new Vector3(0.0f,0.0f,_hitbox_range);
						break;
						case "up_":
							_hitbox.Position = new Vector3(0.0f,0.0f,-_hitbox_range*2);
						break;
					}
				}
			}
			else
			{
				_state = 2;
				_node_in_range.Test_interact_function();
			}
		}
	}

	public void activate_hitbox()
	{
		if (_hitbox != null)
			_hitbox._active = true;
	}

	public void back_to_move_state()
	{
		if (_hitbox != null)
		{
			RemoveChild(_hitbox);
			_hitbox.Free();
			_hitbox = null;
		}
		_state = 0;
	}
	
	public Vector3 Player_attack_state(double delta, Vector3 velocity)
	{
		if (_Animator.CurrentAnimationPosition > .2 && _hitbox != null && _hitbox._active)
		{
			_hitbox._active = false;
			//GLOBAL_FUNCTIONS.play_sound(_sword_swing);
			_hitbox.GetChild<CollisionShape3D>(0).Disabled = false;
		}
		
		if (velocity.Y > 0)
			velocity.Y = 0;
		velocity.X = 0;
		velocity.Z = 0;

		_Animator.Play(_base + "attack");

		return velocity;
	}

	public Vector3 Player_aim_state(double delta, Vector3 velocity)
	{
		_hspd = 0;
		_vspd = 0;
		velocity.X = 0;
		velocity.Z = 0;

		if (Input.IsActionJustReleased("ui_left"))
			_state = 4;


		if (Input.IsKeyPressed(Key.W))
			_base = "up_";
		else if (Input.IsKeyPressed(Key.S))
			_base = "down_";
		else if (Input.IsKeyPressed(Key.A))
			_base = "left_";
		else if (Input.IsKeyPressed(Key.D))
			_base = "right_";
		else
			_hspd = 0;


		_Animator.Play(_base + "idle");

		return velocity;
	}

	public Vector3 Player_shoot_state(double delta, Vector3 velocity)
	{
		_hspd = 0;
		_vspd = 0;
		velocity.X = 0;
		velocity.Z = 0;

		_Animator.Play(_base + "bow");

		if (!_shoot_arrow && _Animator.CurrentAnimationPosition > .3)
		{
			_shoot_arrow = true;
			Obj_projectile_parent _arrow = GLOBAL_FUNCTIONS.Create_projectile(this);
			GLOBAL_FUNCTIONS.Play_Sound(_player_bow_shot, 0.9f);
			switch (_base)
			{
				case "left_": 
					_arrow._hspd = -_arrow_speed;
					_arrow._my_sprite.RotationDegrees = new Vector3(-45, 0, 90);
				break;
				case "right_": 
					_arrow._hspd = _arrow_speed;
					_arrow._my_sprite.RotationDegrees = new Vector3(-45, 0, -90);
				break;
				case "up_": 
					_arrow._vspd = -_arrow_speed;
					_arrow._my_sprite.RotationDegrees = new Vector3(-45, 0, 0);
				break;
				case "down_": 
					_arrow._vspd = _arrow_speed;
					_arrow._my_sprite.RotationDegrees = new Vector3(-45, 0, 180);
				break;
			}
		}
		
		return velocity;
	}

	public Vector3 Player_interact_state(double delta, Vector3 velocity)
	{
		_Animator.Play(_base + "idle");

		if (velocity.Y > 0)
			velocity.Y = 0;
		velocity.X = 0;
		velocity.Z = 0;

        //GLOBAL_FUNCTIONS.Call_Chatbox("test", "test2", "test3");

        return velocity;
	}




}
