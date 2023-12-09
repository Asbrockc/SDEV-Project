using Godot;
using System;

public partial class Core_spotLight : SpotLight3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GLOBAL_FUNCTIONS.GetFlag(GLOBAL_STATS.FLAG_INDEX.show_shadows))
		{
			this.ShadowEnabled = true;
			this.ShadowBias = 0.1f;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
