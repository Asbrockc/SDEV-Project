using Godot;

public partial class Player_stats_menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int i = 0;
		this.GetChild<Label>(i++).Text = GLOBAL_STATS._player_name;
		this.GetChild<Label>(i++).Text = (GLOBAL_STATS._player_stats[GLOBAL_STATS.I_MAX_HEALTH]/2).ToString() + " / 10";		
		this.GetChild<Label>(i++).Text = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_STRENGTH].ToString() + " / 10";
		this.GetChild<Label>(i++).Text = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_DEFENCE].ToString() + " / 10";

	}
}
