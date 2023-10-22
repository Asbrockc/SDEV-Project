using Godot;

public partial class Spr_sprite_interact_target : Sprite3D
{
	public Interact_label _label;

	public override void _Process(double delta)
	{
		if (GLOBAL_STATS._player._node_in_range == null)
		{
			this.Position = new Vector3(0.0f, 10000.0f, 0.0f);
		}
		else
		{
			this._label.Text = GLOBAL_STATS._player._node_in_range._label;
			this.Position = (GLOBAL_STATS._player._node_in_range.GlobalPosition + new Vector3(0.0f, 1.0f, -0.75f));
		}
	}
}
