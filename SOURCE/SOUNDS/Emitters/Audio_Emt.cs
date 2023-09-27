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

	public void play_sound(AudioStreamWav _sound, bool _shift_tone)
	{
		_audio_player_list[_index_count].Stream = _sound;
		if (_shift_tone)
			_audio_player_list[_index_count].PitchScale = GLOBAL_FUNCTIONS.Choose_Float(0.9f, 1.0f, 1.1f);
		else 
			_audio_player_list[_index_count].PitchScale = 1.0f;

		_audio_player_list[_index_count++].Play();
		//GD.Print(_audio_player_list.Count);
		if (_index_count >= _audio_player_list.Count)
			_index_count = 0;
	}
}
