using Godot;
using System;

public partial class Spider_AI : Enemy_Egg_AI
{
	public AudioStreamWav _jump_sound = null;
	
    public override void _Ready()
    {
		base._Ready();
		this._jump_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_spider_jump.wav");

    }

	public override Vector3 move_to_player_state(double delta, Vector3 velocity)
	{
		if (IsOnFloor())
		{
			GLOBAL_FUNCTIONS.Play_Sound(_jump_sound);
			_jump_spd = GLOBAL_FUNCTIONS.Choose(4,5,6);
			_hspd = 0;
			_vspd = 0;
			return velocity;
		}
		else
		{

			if (_Animator != null)
				_Animator.Play(_base + "walk");
			return base.move_to_player_state(delta, velocity);
		}
	}
}
