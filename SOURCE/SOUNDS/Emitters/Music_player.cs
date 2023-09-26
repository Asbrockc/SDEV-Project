using Godot;
using System;

public partial class Music_player : AudioStreamPlayer3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.GetParent<Audio_Emt>()._music_player = this;
	}

	public void _on_finished()
	{
		this.Play();
	}
}
