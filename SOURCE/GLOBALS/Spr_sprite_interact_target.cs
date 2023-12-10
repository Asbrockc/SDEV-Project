using Godot;

///<summary>
/// A global target that is usually out of the players view
///</summary>
public partial class Spr_sprite_interact_target : Sprite3D
{
	public Interact_label _label;

	private Vector3 _gone = new Vector3(0.0f, 10000.0f, 0.0f);
	private Vector3 _off = new Vector3(0.0f, 1.0f, -0.75f);

	///<summary>
	/// typically stays out of range so the player can't see it 
	/// however, if there is an interactiable object close enough to use 
	/// this arrow moves over it to showcase the player can use it.
	///</summary>
	public override void _Process(double delta)
	{
		if (GLOBAL_STATS._player._node_in_range == null)
		{
			this.Position = _gone;
		}
		else
		{
			this._label.Text = GLOBAL_STATS._player._node_in_range._label;
			this.Position = (GLOBAL_STATS._player._node_in_range.GlobalPosition + _off);
		}
	}
}
