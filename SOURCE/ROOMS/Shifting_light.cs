using Godot;
using System;

public partial class Shifting_light : OmniLight3D
{
	private float _count = 0;
	private Vector3 _new_location = new Vector3(0,0,0);

	public override void _Ready()
	{
		_new_location = this.GlobalPosition;
	}

	public override void _Process(double delta)
	{
		_count += .025f;

		if (_count >= 360)
			_count = 0;

		_new_location.X = 5 * (float)Math.Cos(_count);
		_new_location.Z = 5 * (float)Math.Sin(_count);

		this.GlobalPosition = _new_location;
	}
}
