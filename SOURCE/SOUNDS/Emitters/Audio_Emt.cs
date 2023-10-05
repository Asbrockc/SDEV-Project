using Godot;
using System;
using System.Collections.Generic;

public partial class Audio_Emt : Node3D
{
	public Music_player _music_player;
	public List<Audio_player_base> _audio_player_list = new List<Audio_player_base>();
	public int _index_count = 0;

	public float _game_volume = 0.1f;


	private float _fade_count = 100;

	public float _music_run_state = -1;

	public String _next_song;
	//public AudioStreamWav _next_song;

	public override void _Ready()
	{
		GLOBAL_FUNCTIONS._audio_emitter = this;
	}

    public override void _Process(double delta)
    {
        switch (_music_run_state)
		{
			case 0:
				fade_out();
			break;
			case 1:
				fade_in();
			break;
		}
    }

    /// <summary>
    /// audio sound player function
    /// Generic since Audio streams can handle wav, mp3, or ogg type files
    /// </summary>
    

	/// <summary>
	/// audio sound player function
	/// Generic since Audio streams can handle wav, mp3, or ogg type files
	/// </summary>
	public void play_sound<T>(T _sound, float _volume_in_perc ,bool _shift_tone)
	{ 
		Type _t_type = typeof(T);
		switch(_t_type.ToString())
		{
			case "Godot.AudioStreamMP3":
			_audio_player_list[_index_count].Stream = (AudioStreamMP3)Convert.ChangeType(_sound, typeof(AudioStreamMP3));
			break;
			case "Godot.AudioStreamWav":
			_audio_player_list[_index_count].Stream = (AudioStreamWav)Convert.ChangeType(_sound, typeof(AudioStreamWav));
			break;
		}

		if (_shift_tone)
			_audio_player_list[_index_count].PitchScale = GLOBAL_FUNCTIONS.Choose<float>(0.9f, 1.0f, 1.1f);
		else 
			_audio_player_list[_index_count].PitchScale = 1.0f;
		
		
		_audio_player_list[_index_count].VolumeDb = -(80.0f - (_volume_in_perc * 80.0f ));

		_audio_player_list[_index_count++].Play();
		//GD.Print(_audio_player_list.Count);
		if (_index_count >= _audio_player_list.Count)
			_index_count = 0;
	}

	public void play_music<T>(T _sound)
	{ 

	}

	private void fade_out()
	{
		if (_fade_count - 10 > 0)
		{
			_fade_count -= 1;
		}
		else
		{
			_fade_count = 0;
			AudioStreamWav _next = ResourceLoader.Load<AudioStreamWav>(_next_song);
			_music_player.Stream = _next;
			_music_player.Play();
			_music_run_state = 1;
		}


		_music_player.VolumeDb = -(80.0f - (_fade_count/100 * 80.0f ));
	}


	private void fade_in()
	{
		if (_fade_count + 10 < 100)
			_fade_count += 1;
		else
		{
			_fade_count = 100;
			_music_run_state = -1;
		}

		_music_player.VolumeDb = -(80.0f - (_fade_count/100 * 80.0f ));
	}
}
