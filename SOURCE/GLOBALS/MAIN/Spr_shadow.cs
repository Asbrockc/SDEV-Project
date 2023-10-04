using Godot;
using System;

public partial class Spr_shadow : Sprite3D
{
	CharacterBody3D _shadow_caster = null;

	private float _floor_y = 0.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this._shadow_caster = this.GetParent<CharacterBody3D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (this._shadow_caster.IsOnFloor())
		{
			_floor_y = _shadow_caster.GlobalPosition.Y;
		}

		this.GlobalPosition = new Vector3(_shadow_caster.GlobalPosition.X, _floor_y, _shadow_caster.GlobalPosition.Z);
	}
}
