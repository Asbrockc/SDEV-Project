using Godot;

public partial class Music_player : AudioStreamPlayer3D
{
	private Audio_Emt _parent;

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
		if (_parent._next_song == "res://SOUNDS/ALL_SOUNDS/MUSIC/snd_final_boss_part_1.wav")
		{
			_parent._next_song = "res://SOUNDS/ALL_SOUNDS/MUSIC/snd_final_boss_part_2.wav";
			this.Stream = ResourceLoader.Load<AudioStreamWav>(_parent._next_song);
		}
		
		this.Play();
	}
}
