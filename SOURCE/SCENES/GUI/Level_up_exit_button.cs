using Godot;
using System;

public partial class Level_up_exit_button : Button
{
    public override void _Pressed()
    {
		Level_up_menu _parent = this.GetParent<Level_up_menu>();
		GLOBAL_STATS._player._state = 0;
		GLOBAL_STATS._player._current_menu = null;
		_parent._destroy = true;
    }
}
