using Godot;
using System;

public partial class Music_player : AudioStreamPlayer3D
{
	private Audio_Emt _parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_parent = this.GetParent<Audio_Emt>();
		_parent._music_player = this;
	}

	public void _on_finished()
	{
		if (_parent._next_song == "res://SOUNDS/ALL_SOUNDS/MUSIC/snd_boss_one_part_1.wav")
		{
			_parent._next_song = "res://SOUNDS/ALL_SOUNDS/MUSIC/snd_boss_one_part_2.wav";
			this.Stream = ResourceLoader.Load<AudioStreamWav>(_parent._next_song);
		}
		
		
		
		this.Play();
	}
}
