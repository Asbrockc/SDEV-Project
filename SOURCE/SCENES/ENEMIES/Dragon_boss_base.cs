using Godot;

///<summary>
/// Dragon boss main AI controls
///</summary>
public partial class Dragon_boss_base : Boss_enemy_base
{

	public bool _move_left = true;

	private AudioStreamWav _flap_sound;
	private AudioStreamWav _attack_sound;

    public override void _Ready()
    {
        base._Ready();
		_state = 6;
   		_slam_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_heavy_slam.wav");
		_flap_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_flap_small.wav");
		_attack_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_dragon_jump.wav");
		_destroy_sound = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_boom_sound.mp3");

		delay_timer = 300;
		drop_amount = 50;
    }

	///<summary>
	/// He doesnt move to the player
	/// instead he jsut shifts back and forth occationlly jumping 
	/// to cause a shockwave 
	///</summary>
    public override Vector3 move_to_player_state(double delta, Vector3 velocity)
    {
		if (!_Animator.IsPlaying())
		{
				_Animator.Play("side_walk");
				GLOBAL_FUNCTIONS.Play_Sound(_flap_sound);
		}

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

	///<summary>
	/// death animation speeds him up
	///</summary>
    public override Vector3 death_state(double delta, Vector3 velocity)
    {
		_Animator.Play("side_walk");
		this._Animator.SpeedScale = 1.2f;

        return base.death_state(delta, velocity);
    }

    private void shift_to_jump_state(float _height)
	{
			GLOBAL_FUNCTIONS.Play_Sound(_attack_sound, 1, false);
			counter = 0;
			_hspd = 0;
			_vspd = 0;
			_jump_spd = _height;
			_Animator.Play("jump");
			_state = 5;
	}

    public override Vector3 intro_state(double delta, Vector3 velocity)
    {
		if (!_Animator.IsPlaying())
		{
				_Animator.Play("side_walk");
				GLOBAL_FUNCTIONS.Play_Sound(_flap_sound);
		}

        return base.intro_state(delta, velocity);
    }

}
