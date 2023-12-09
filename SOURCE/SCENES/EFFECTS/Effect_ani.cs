using Godot;

public partial class Effect_ani : AnimationPlayer
{

	public override void _Ready()
	{
		this.GetParent<Effects_drops_sprite>()._animator = this;
	}


	public override void _Process(double delta)
	{
	}
}
