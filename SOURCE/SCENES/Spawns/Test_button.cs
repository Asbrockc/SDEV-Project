using Godot;
using System;

public partial class Test_button : Button_base
{
    public override void _on_press()
    {
        GLOBAL_STATS._completion_flags[(int)GLOBAL_STATS.FLAG_INDEX.Locked_door_1] = true;
        //GetParent().GetNode<Locked_door_base>("Locked_door_base")._open = true;
    }
}
