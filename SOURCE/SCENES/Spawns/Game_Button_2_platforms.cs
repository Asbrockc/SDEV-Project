using Godot;
using System;

public partial class Game_Button_2_platforms : Game_button_base
{
    public override void _on_press()
    {
		Path_follower_back_an_forth _temp = GetParent().GetNode("Path3D").GetChild<Path_follower_back_an_forth>(0);
        _temp._active = !_temp._active;
		_temp = GetParent().GetNode("Path3D2").GetChild<Path_follower_back_an_forth>(0);
        _temp._active = !_temp._active;
		_temp = GetParent().GetNode("Path3D3").GetChild<Path_follower_back_an_forth>(0);
        _temp._active = !_temp._active;
    }
}
