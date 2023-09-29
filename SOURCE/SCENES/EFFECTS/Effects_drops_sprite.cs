using Godot;
using System;

public partial class Effects_drops_sprite : Sprite3D
{
	public AnimationPlayer _animator = null;
	public String _curr_effect = "exp";

	private Vector3 _scaler = new Vector3(4.0f, 4.0f, 4.0f);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this._animator.Play(_curr_effect);
		if (_curr_effect == "hp" && this.Scale.X == 3.0f)
			this.Scale = _scaler;
	}
}
