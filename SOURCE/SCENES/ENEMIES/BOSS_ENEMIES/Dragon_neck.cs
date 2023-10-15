using Godot;
using System;

public partial class Dragon_neck : Path3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Curve = new Curve3D();
		this.Curve.AddPoint(new Vector3(0,0,0));
		this.Curve.AddPoint(new Vector3(0,0,0));
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
	}
}
