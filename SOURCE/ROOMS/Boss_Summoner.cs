using Godot;
using System;

public partial class Boss_Summoner : Event_Summoner
{
	[Export] private String _boss_to_summon = "x";
	[Export] private bool _stop_music = true;

	public override void _summon_event()
	{
		if (_stop_music)
			GLOBAL_FUNCTIONS.Change_Music(null, 100);
			
		GLOBAL_FUNCTIONS.Spawn_enemy(this.Position, _boss_to_summon);
	}
}
