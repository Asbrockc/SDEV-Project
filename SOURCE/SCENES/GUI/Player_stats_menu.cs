using Godot;

public partial class Player_stats_menu : Control
{
	public override void _Process(double delta)
	{
		int i = 0;
		this.GetChild<Label>(i++).Text = GLOBAL_STATS._player_name;
		this.GetChild<Label>(i++).Text = (GLOBAL_STATS._player_stats[GLOBAL_STATS.I_MAX_HEALTH]/2).ToString() + " / 10";		
		this.GetChild<Label>(i++).Text = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_STRENGTH].ToString() + " / 10";
		this.GetChild<Label>(i++).Text = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_DEFENCE].ToString() + " / 10";

	}
}
