using Godot;
using System;

///<summary>
/// Parent boss class
/// inherits the enemy_base and physics base
/// defines the more advanced boss switch methods 
/// also defines their death animation to be a bit more dramatic
///</summary>
public partial class Boss_enemy_base : Obj_enemy_base
{
	public int counter = 0;
	public int next_jump_in = 120;

	public string _boss_music = "res://SOUNDS/ALL_SOUNDS/MUSIC/snd_boss_one_part_1.wav";

	public bool _dying = true;

	protected AudioStreamMP3 _destroy_sound = null;

	///<summary>
	/// core boss AI add more states to the switch driver
	/// Notably the intro state that will showcase the boss with the camera 
	/// and start the boss song
	///</summary>
	public override Vector3 enemy_core_AI(double delta, Vector3 velocity)
	{
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (_jump_spd != 0)
		{
			//GD.Print("went to jump");
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
				if (_dying)
					velocity = death_state(delta, velocity);
				else
					velocity = death_finale(delta, velocity);
			break;
			case 4: 
				knight_slam_attack(true, 0, -1, 0);
			break;
			case 5: 
				velocity = jump_state(delta, velocity);
			break;
			case 6: 
				velocity = intro_state(delta, velocity);
			break;

			default:
				_hspd = 0;
				_vspd = 0;
				break;
		}

		_base = GLOBAL_FUNCTIONS.Entity_Dir(_base, _hspd, _vspd);

		//GD.Print(velocity.Y);

		return velocity;
	}

	///<summary>
	/// Death animation plays a bunch of explotions before the boss is destroyed
	///</summary>
	public virtual Vector3 death_state(double delta, Vector3 velocity)
	{
		//GD.Print("death_state");
		delay_timer = 300;
		if (!GetParent<Boss_core_AI>()._defeated)
			GetParent<Boss_core_AI>()._defeated = true;

		_hspd = 0;
		_vspd = 0;
		//velocity.Y = 0;
		_apply_grav = true;

		if (hit_timer <  delay_timer)
		{
			hit_timer++;

			if (hit_timer % 10 == 0)
			{
				GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_blood.tscn", true,
						GLOBAL_FUNCTIONS.Choose(-.5f, -.25f, 0, .25f, .5f),
						GLOBAL_FUNCTIONS.Choose(-.5f, -.25f, 0, .25f, .5f),
						GLOBAL_FUNCTIONS.Choose(-.5f, -.25f, 0, .25f, .5f));
				GLOBAL_FUNCTIONS.Play_Sound(_destroy_sound);
			}

			//this.Position += new Vector3(0,.02f,0);
		}
		else
		{
			hit_timer = 0;
			death_function();
			GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_blood.tscn", true);
			GLOBAL_FUNCTIONS.SetFlag(GLOBAL_STATS.FLAG_INDEX.Beat_boss_1);
			GLOBAL_STATS._Camera._target = GLOBAL_STATS._player;
			GLOBAL_STATS._Camera._y_dis = 3.0f; //4.0
			GLOBAL_STATS._Camera._z_dis = 3.0f; //4.0

			GLOBAL_FUNCTIONS.Change_Music(null, 5);

			_dying = false;
		}
		//GD.Print("Will walk to player soon enough");
		return velocity;
	}

	///<summary>
	/// 
	///</summary>
	public virtual Vector3 death_finale(double delta, Vector3 velocity)
	{
		//GD.Print("base final");
		_death_flag = true;
		GetParent().RemoveChild(this);
		QueueFree();
		/*
		counter++;
		if (IsOnFloor() && counter > 30)
		{
			counter = 0;
			next_jump_in = GLOBAL_FUNCTIONS.Choose(200,400,500);
			_state = 4;
		}

		return velocity;
		*/

		return velocity;
	}

	///<summary>
	/// cinematic for boss enemies 
	/// locks the player
	/// moves the camera to the boss to show them off
	/// starts the boss music
	/// moves everything back to normal 
	/// initilizes the boss and lets the player move again
	///</summary>
	public virtual Vector3 intro_state(double delta, Vector3 velocity)
    {
		if (counter < next_jump_in)
		{
			counter++;
			GLOBAL_STATS._player._base = "up_";
			GLOBAL_STATS._player._state = 2;
			GLOBAL_STATS._Camera._target = this;
			GLOBAL_STATS._Camera._y_dis = 4.0f;
			GLOBAL_STATS._Camera._z_dis = 2.0f;
		}
		else
		{
			//
			next_jump_in = GLOBAL_FUNCTIONS.Choose(200,400,500);

			if (_boss_music != "null")
				GLOBAL_FUNCTIONS.Change_Music(_boss_music, 100);

			counter = 0;
			GLOBAL_STATS._Camera._target = GLOBAL_STATS._player;
			GLOBAL_STATS._Camera._y_dis = 3.0f;
			GLOBAL_STATS._Camera._z_dis = 3.0f;
			this.GetParent<Boss_core_AI>()._intro = false;
			GLOBAL_STATS._player._state = 0;
			this._state = MOVE_STATE;
		}

		return velocity;
    }

	public virtual Vector3 jump_state(double delta, Vector3 velocity)
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
}
