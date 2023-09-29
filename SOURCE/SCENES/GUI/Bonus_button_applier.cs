using Godot;
using System;

public partial class Bonus_button_applier : Button
{
    public override void _Pressed()
    {
        Level_up_menu _menu = this.GetParent<Level_up_menu>();

		for (int i = 0; i < 3; i++)
		{
			attribute_leveler_parent _curr = _menu.GetChild<attribute_leveler_parent>(i+1);

			if (_curr._current_level > 0)
			{
				switch (i)
				{
					case 0: 
					GLOBAL_STATS._player_stats[GLOBAL_STATS.I_MAX_HEALTH] += _curr._current_level;
					_curr._current_level = 0;
					break;
					case 1: 
					GLOBAL_STATS._player_stats[GLOBAL_STATS.I_STRENGTH] += _curr._current_level;
					_curr._current_level = 0;				
					break;
					case 2: 
					GLOBAL_STATS._player_stats[GLOBAL_STATS.I_DEFENCE] += _curr._current_level;
					_curr._current_level = 0;				
					break;
				}
			}
		}

		GLOBAL_STATS._player_stats[GLOBAL_STATS.I_BONUS_POINTS] = _menu._avalable_level;
    }
}
