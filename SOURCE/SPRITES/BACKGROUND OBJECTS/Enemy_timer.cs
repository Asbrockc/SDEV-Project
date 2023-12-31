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

	public void _on_timeout()
	{
		GetParent<Enemy_spawner>()._trigger = true;
		
		if (GetParent<Enemy_spawner>()._time == -4)
			WaitTime = GLOBAL_FUNCTIONS.Choose(5,10,15);
		else
			WaitTime = GetParent<Enemy_spawner>()._time;
			
		Start();
	}
}
