using Godot;
using System;

public partial class Boss_hp_bar : Control
{
	public Dragon_boss_core_AI _core = null;

	private ProgressBar _boss_hp = null;	
	private Label _boss_name = null;


	private Vector2 _adjust = new Vector2(0, 2);
	private float _up_size = -60.0f;
	private float _down_size = 0.0f;

	public override void _Ready()
	{
		_core = GetParent<Dragon_boss_core_AI>();
		_boss_hp = GetChild<ProgressBar>(1);
		_boss_name = GetChild<Label>(3);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (!_core._defeated)
		{
			if (this.GlobalPosition.Y < _down_size)
				this.GlobalPosition += _adjust;
		}
		else
		{
			if (this.GlobalPosition.Y > _up_size)
				this.GlobalPosition -= _adjust;
		}

		_boss_name.Text = _core._name;
		_boss_hp.Value = (100.0f * _core._boss_hp/ _core._boss_max_hp);
	}
}
