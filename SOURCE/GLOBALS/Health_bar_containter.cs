using Godot;
using System;

public partial class Health_bar_containter : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int i = 0;
		int k = 0;
		foreach (TextureProgressBar _node in GetChildren())
		{
			
			if (i < GLOBAL_STATS._player_stats[GLOBAL_STATS.I_MAX_HEALTH]/2)
			{
				_node.Visible = true;
				_node.Value = 0;
				
				for (int j = 0; j < 2; j++)
					if (k++ < GLOBAL_STATS._player_stats[GLOBAL_STATS.I_HEALTH])
						_node.Value++;
					
			}
			else
			{
				_node.Visible = false;
			}

			i++;
		}
	}
}
