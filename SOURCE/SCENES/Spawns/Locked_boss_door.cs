using Godot;
using System;

public partial class Locked_boss_door : Locked_door_base
{
   public override void _door_trigger()
    {
    	this._open = GLOBAL_FUNCTIONS.GetFlag(GLOBAL_STATS.FLAG_INDEX.Boss_door_1);
    }
}
