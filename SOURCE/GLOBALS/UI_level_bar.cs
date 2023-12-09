using Godot;


public partial class UI_level_bar : TextureProgressBar
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print(this.Value);
		this.Value = 
		(float)GLOBAL_STATS._player_stats[GLOBAL_STATS.I_EXPERIENCE]/
		(float)GLOBAL_STATS._player_stats[GLOBAL_STATS.I_NEXT_LEVEL_IN] * 100.0f;
	}
}
