using Godot;
using System;

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
