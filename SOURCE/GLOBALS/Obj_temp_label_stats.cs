using Godot;
using System;

public partial class Obj_temp_label_stats : Label
{
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		String _temp = "";

		for (int i = 0; i < GLOBAL_STATS._player_stats.Count; i++)
			_temp += GLOBAL_STATS._player_stats[i].ToString() + "\n";

		this.Text = _temp;
	}
}
