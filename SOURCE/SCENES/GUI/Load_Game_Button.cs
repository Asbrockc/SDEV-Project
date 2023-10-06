using Godot;
using System;

public partial class Load_Game_Button : Button
{
    public override void _Pressed()
    {
        GLOBAL_STATS._Load_Game(GetParent<Save_Load_Menu_Section>()._save_slot);

        Save_Load_menu_base _base = GetParent().GetParent<Save_Load_menu_base>();
        GLOBAL_FUNCTIONS.Play_Sound(_base._save_game_sound, .8f, false);
        GLOBAL_FUNCTIONS.UI_Visibiity(true);
    }
}
