using Godot;
using System;

public partial class Enemy_timer : Timer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OneShot = true;
		Autostart = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_timeout()
	{
		GetParent<Enemy_spawner>()._trigger = true;
		WaitTime = GLOBAL_FUNCTIONS.Choose(5,10,15);
		Start();
	}
}
