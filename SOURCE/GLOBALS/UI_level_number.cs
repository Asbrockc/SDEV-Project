using Godot;

public partial class UI_level_number : Label
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Text = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_LEVEL].ToString();
	}
}
