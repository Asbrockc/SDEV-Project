using Godot;
using System;
using System.Collections.Generic;

public partial class Audio_Emt : Node3D
{
	public Music_player _music_player;
	public List<Audio_player_base> _audio_player_list = new List<Audio_player_base>();
	public int _index_count = 0;

	public override void _Ready()
	{
		GLOBAL_FUNCTIONS._audio_emitter = this;
	}

	public override void _Process(double delta)
	{
	}

	/// <summary>
	/// audio sound player function
	/// Generic since Audio streams can handle wav, mp3, or ogg type files
	/// </summary>
	public void play_sound<T>(T _sound, bool _shift_tone)
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

		_audio_player_list[_index_count++].Play();
		//GD.Print(_audio_player_list.Count);
		if (_index_count >= _audio_player_list.Count)
			_index_count = 0;
	}
}
