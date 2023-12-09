using Godot;

public partial class Audio_player_base : AudioStreamPlayer3D
{
	public override void _Ready()
	{
		this.GetParent<Audio_Emt>()._audio_player_list.Add(this);
	}
}
