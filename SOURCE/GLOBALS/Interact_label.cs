using Godot;


///<summary>
/// Label for interacable objects, it just gives the parent class a reference to this specific instance
///</summary>
public partial class Interact_label : Label3D
{
	public override void _Ready()
	{
		this.GetParent<Spr_sprite_interact_target>()._label = this;
	}
}
