using Godot;
using System;

public partial class Boss_Summoner : Event_Summoner
{
	public override void _summon_event()
	{
		GLOBAL_FUNCTIONS.Spawn_enemy(this.Position, "res://SCENES/ENEMIES/BOSS_ENEMIES/Enemy_Boss_Dragon.tscn");
	}
}
