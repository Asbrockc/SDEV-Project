using Godot;
using System;
using System.Data;
using System.Runtime.Versioning;

public partial class Obj_player_base_script : Obj_physics_base
{

	public GLOBAL_SCENE _Global_accessor;
	public Obj_player_animator _Animator;
	public Interactive_Action _node_in_range = null;
	 
	private string _base = "down_";


	public Player_hitbox _hitbox = null;
	private float _hitbox_range = 0.5f;

	public override void _Ready()
	{
		GLOBAL_STATS._player = this;
		_Global_accessor = this.GetParent<GLOBAL_SCENE>();


		//GLOBAL_FUNCTIONS.Spawn_enemy(Position);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("ui_right"))
			GLOBAL_FUNCTIONS.Spawn_enemy(Position);
		//GD.Print(GLOBAL_STATS._player_stats[0]);
		//GD.Print(_Animator);
		Vector3 velocity = Velocity;
	
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

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
		}

		Velocity = velocity;
		MoveAndSlide();
	}


	public Vector3 Player_move_state(double delta, Vector3 velocity)
	{
		// Handle Jump.
		if (Input.IsKeyPressed(Key.Space) && IsOnFloor())
			velocity.Y = JumpVelocity;

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


		//GD.Print(_hspd.ToString() + " " + _vspd.ToString());
		velocity = apply_speed(velocity);

		if (IsOnFloor())
		{
			string _motion;

			if (_hspd > 0)
				_base = "right_";
			else if (_hspd < 0)
				_base = "left_";
			else if (_vspd > 0)
				_base = "down_";
			else if (_vspd < 0)
				_base = "up_";

			if (_vspd == 0 && _hspd == 0)
				_motion = "idle";
			else
				_motion = "walk";

			_Animator.Play(_base + _motion);
		}

		handle_action_key();
	
		if (Input.IsKeyPressed(Key.Left))
		{
			_state = 2;
		}
		
		return velocity;
	}


	private void handle_action_key()
	{
		if (Input.IsKeyPressed(Key.Up))
		{
			if (_node_in_range == null)
			{
				_state = 1;

				if (_hitbox == null)
				{
					_hitbox = (Player_hitbox)ResourceLoader.Load<PackedScene>("res://SCENES/Spawns/Player_hitbox.tscn").Instantiate();
				
					AddChild(_hitbox);
					_hitbox._player_parent = this;

					switch (_base)
					{
						case "right_":
							_hitbox.Position = new Vector3(_hitbox_range,0.0f,0.0f);
						break;
						case "left_":
							_hitbox.Position = new Vector3(-_hitbox_range,0.0f,0.0f);
						break;
						case "down_":
							_hitbox.Position = new Vector3(0.0f,0.0f,_hitbox_range);
						break;
						case "up_":
							_hitbox.Position = new Vector3(0.0f,0.0f,-_hitbox_range);
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

	public void back_to_move_state()
	{
		//for (int i = 0; i < 10; i++)
			//GLOBAL_FUNCTIONS.Spawn_item(Position);

		//GLOBAL_FUNCTIONS.Spawn_enemy(Position);
		//GLOBAL_FUNCTIONS.Spawn_enemy(Position);
		//GLOBAL_STATS._Load_Game();
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
		if (velocity.Y > 0)
			velocity.Y = 0;
		velocity.X = 0;
		velocity.Z = 0;

		_Animator.Play(_base + "attack");

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
