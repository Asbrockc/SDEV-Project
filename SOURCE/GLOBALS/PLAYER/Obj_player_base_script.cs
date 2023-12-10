using Godot;
using System;

///<summary>
/// PLayers functionality
///</summary>
public partial class Obj_player_base_script : Obj_physics_base
{
	public GLOBAL_SCENE _Global_accessor;
	public Interactive_Action _node_in_range = null;
	public Level_up_menu _current_menu = null;

	private int _dead_timer = 0;
	private Vector3 _base_angle = new Vector3(-45, 0, 0);
	private Vector3 _mid_angle = new Vector3(-60, 0, 0);
	private Vector3 _attack_angle = new Vector3(-70, 0, 0);

	public int _hit_timer = 0;

	public AudioStreamWav _sword_hit = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_sword_hit.wav");
	public AudioStreamWav _sword_swing = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_sword_swing.wav");
	public AudioStreamWav _item_pickup = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_item_pickup.wav");
	public AudioStreamWav _exp_pickup = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_exp_pickup.wav");
	
	public AudioStreamWav _level_up = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_level_up.wav");
	public AudioStreamWav _player_hit = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_player_hit.wav");
	public AudioStreamMP3 _player_bow_shot = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_bow_shot_2.mp3");
	

	public AudioStreamMP3 _player_game_over = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_game_over.mp3");
	public AudioStreamMP3 _player_won_game = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_game_won.mp3");
	public string _base = "down_";
	
	public Player_hitbox _hitbox = null;
	private float _hitbox_range = 0.5f;

	public float _hurt_hspd = 0;
	public float _hurt_vspd = 0;	
	public float _hurt_up_speed = 0;

	private bool _shoot_arrow = false;
	private bool _jumped = false;
	private float _arrow_speed = 0.3f;

	private int finish_counter = 0;

	///<summary>
	/// gives a refernece of the player to the GLobal stats
	///</summary>
	public override void _Ready()
	{
		GLOBAL_STATS._player = this;
		_Global_accessor = this.GetParent<GLOBAL_SCENE>();
	}

	///<summary>
	/// uses the _state variable in a switch to validate the players input
	///</summary>
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;
		else
		{

		}

		if (!GLOBAL_STATS._game_finished)
		{
			if (_hit_timer > 0)
				_hit_timer--;
			else
				_hit_timer = 0;

			//if the health is 0 or less cut the _state instantly becomes the death state
			if (GLOBAL_STATS._player_stats[GLOBAL_STATS.I_HEALTH] <= 0 && _state != 6)
				_state = 5;

			//core input validation, only one state will play at a time depending on what the 
			//user presses
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
				case 5:
					GLOBAL_FUNCTIONS.Play_Sound(_player_game_over);
					_Animator.Play("dead");
					_state = 6;
					break;
				case 6:
					_hspd = 0;
					_vspd = 0;
					_hurt_hspd = 0;
					_hurt_vspd = 0;
					_hurt_up_speed = 0;

					if (!_Animator.IsPlaying() && _dead_timer > 200)
					{
						_dead_timer = 0;
						GLOBAL_FUNCTIONS.Change_Music(null);
						GLOBAL_FUNCTIONS.Room_Transition("res://SCENES/Game_over_Screen.tscn", "LOCK_PLAYER", 0 ,0);
						GLOBAL_STATS._Reset_Stats();
						_state = 0;
					}
					else
						_dead_timer++;
					break;
			}

			velocity = manage_hurt_velocity(velocity);
		}
		else
		{
			velocity.X = 0;
			velocity.Z = 0;
			if (finish_counter == 0)
			{
				GLOBAL_FUNCTIONS.Play_Sound(_player_won_game);
				_Animator.Play("WIN");
			}
			
			finish_counter++;

			if (finish_counter >= 300)
			{
				GLOBAL_FUNCTIONS.Room_Transition("res://SCENES/Win_Screen.tscn", "LOCK_PLAYER", 0, 0);
				GLOBAL_STATS._game_finished = false;
			}

		}

		Velocity = velocity;
		
		//hidden cheat that levels the player very quickly. A joke at "God mode" from older games.
		if (Input.IsKeyPressed(Key.G) && Input.IsKeyPressed(Key.O) && Input.IsKeyPressed(Key.D))
		{
			GLOBAL_STATS._player_stats[GLOBAL_STATS.I_EXPERIENCE] += 10;
		}
		else if (_state != 6)
			MoveAndSlide();
	}

	///<summary>
	/// An extra function that will manage the velocity applied when hit by enemies
	///</summary>
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

	///<summary>
	/// the jump state that gives full mobility, but stops the players ability to attack
	///</summary>
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


	///<summary>
	/// base player script that allows the player to move around, jump, and attack
	///</summary>
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


	///<summary>
	/// A branching path for what the action key does
	/// if there is a interactive node in range they interact with it
	/// if not they swing the sword
	///</summary>
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

	///<summary>
	/// Takes a hitbox and turns it on, if it exists
	///</summary>
	public void activate_hitbox()
	{
		if (_hitbox != null)
			_hitbox._active = true;
	}

	///<summary>
	/// ends attacks, fixes his attack angle, then shifts the player back into the move state
	///</summary>
	public void back_to_move_state()
	{
		GetNode<Sprite3D>("Obj_player_sprite").RotationDegrees = _base_angle;
		if (_hitbox != null)
		{
			RemoveChild(_hitbox);
			_hitbox.QueueFree();
			_hitbox = null;
		}
		_state = 0;
	}
	
	///<summary>
	/// removes some of the players controls and starts the attack animation
	/// also shifts the players sprite, so they do not clip, and direction, so they attack
	/// in the direction they are facing
	///</summary>
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

		switch(_base)
		{
			case "left_":
			case "right_":
				GetNode<Sprite3D>("Obj_player_sprite").RotationDegrees = _mid_angle;
			break;
			case "down_":
				GetNode<Sprite3D>("Obj_player_sprite").RotationDegrees = _attack_angle;
			break;


		}

		_Animator.Play(_base + "attack");

		return velocity;
	}

	///<summary>
	/// removed function that let the player hit a direction to aim the bow.
	///</summary>
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

	///<summary>
	/// removes the players ability to move and makes them draw and arrow and fire a projectle
	///</summary>
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
			_arrow.Position += new Vector3(0, 0.25f, 0); 
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

	///<summary>
	/// a specific state that just stops the player and removes all input 
	/// the object they are interacting with will shift them back into the move state
	/// when it is done.
	///</summary>
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
