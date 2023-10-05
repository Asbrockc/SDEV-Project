using Godot;
using System;

public partial class Name_box_cancel : Button
{
    public override void _Pressed()
    {
        Node _base = this.GetParent();
		_base.GetParent().RemoveChild(_base);

		GLOBAL_STATS._player._state = 0;
		_base.QueueFree();
    }
}
