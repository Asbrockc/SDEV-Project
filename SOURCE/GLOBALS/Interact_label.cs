using Godot;

public partial class Interact_label : Label3D
{
	public override void _Ready()
	{
		this.GetParent<Spr_sprite_interact_target>()._label = this;
	}
}
