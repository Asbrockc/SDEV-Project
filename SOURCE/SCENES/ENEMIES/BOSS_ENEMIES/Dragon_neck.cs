using Godot;
using System;

public partial class Dragon_neck : Path3D
{
	public bool _dead = false;
	public int _destroy_count = 0;
	public int _head_id = -1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Curve = new Curve3D();
		this.Curve.AddPoint(new Vector3(0,0,0));
		this.Curve.AddPoint(new Vector3(0,1,0));
		_destroy_count = GetChildren().Count - 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float _div = 0.1f;
		for (int i = 0; i < GetChildren().Count; i++)
		{
			((PathFollow3D)GetChildren()[i]).ProgressRatio = _div;
			_div += 0.1f;
		}

		if (_dead)
		{
			if (_destroy_count > 0)
			{
				GetChild<PathFollow3D>(_destroy_count).GetChild<Sprite3D>(0).Visible = false;
				_destroy_count--;
			}
			else
			{
				Dragon_boss_core_AI _temp = this.GetParent<Dragon_boss_core_AI>();
				_temp._neck[_head_id] = null;
				_temp.RemoveChild(this);
				QueueFree();
			}
		}
	}
}
