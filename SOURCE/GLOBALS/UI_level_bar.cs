using Godot;
using System;

public partial class UI_level_bar : TextureProgressBar
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print(this.Value);
		this.Value = 
		(float)GLOBAL_STATS._player_stats[GLOBAL_STATS.I_EXPERIENCE]/
		(float)GLOBAL_STATS._player_stats[GLOBAL_STATS.I_NEXT_LEVEL_IN] * 100.0f;
	}
}
