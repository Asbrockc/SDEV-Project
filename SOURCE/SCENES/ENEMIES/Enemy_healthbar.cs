using Godot;
using System;

public partial class Enemy_healthbar : ProgressBar
{
	Obj_enemy_base _enemy_base;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_enemy_base = GetParent().GetParent<Obj_enemy_base>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_enemy_base._health != _enemy_base._max_health)
		{
			this.Visible = true;
			this.Value = 100.0f * ((float)_enemy_base._health / (float)_enemy_base._max_health);
		}
	}
}
