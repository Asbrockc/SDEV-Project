using Godot;
using System;

public partial class Final_boss_first_phase : Boss_enemy_base
{
    private bool _death_set_up = false;

    public override void _Ready()
    {
        base._Ready();
        //_slam_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_heavy_slam.wav");
		//_flap_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_flap_small.wav");
		//_attack_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_dragon_jump.wav");
		_destroy_sound = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_boom_sound.mp3");
		_state = 6;
    }

    public override Vector3 death_finale(double delta, Vector3 velocity)
	{
		
		if (!_death_set_up)
        {
            //GD.Print("CLICKED");
            //velocity.Y = 10;
            _jump_spd = 10;
            _death_set_up = true;
        }

		counter++;
		if (IsOnFloor() && counter > 5)
		{
			counter = 0;
		
            //GD.Print("CLICKED _boom");
            _death_flag = true;
            QueueFree();

			GLOBAL_FUNCTIONS.Change_Music("res://SOUNDS/ALL_SOUNDS/MUSIC/snd_final_boss_part_1.wav", 100);

			GLOBAL_STATS._current_room_reference.GetNode("L_floor_one").QueueFree();
		}

		return velocity;

	}
	
}
