using Godot;
using System;

///<summary>
/// A shiney effect for the drops in the game
/// just runs through the animation repeatedly and shifts the scale
/// depending on if it is an HP drop or an EXP drop
///</summary>
public partial class Effects_drops_sprite : Sprite3D
{
	public AnimationPlayer _animator = null;
	public String _curr_effect = "exp";

	private Vector3 _scaler = new Vector3(4.0f, 4.0f, 4.0f);

	public override void _Process(double delta)
	{
		this._animator.Play(_curr_effect);
		if (_curr_effect == "hp" && this.Scale.X == 3.0f)
			this.Scale = _scaler;
	}
}
