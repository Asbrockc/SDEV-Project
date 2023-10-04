using Godot;
using System;

public partial class Locked_door_1 : Locked_door_base
{
    public override void _door_trigger()
    {
    	this._open = GLOBAL_FUNCTIONS.GetFlag(GLOBAL_STATS.FLAG_INDEX.Locked_door_1);
    }
}
