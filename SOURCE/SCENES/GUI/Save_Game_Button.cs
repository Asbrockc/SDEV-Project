using Godot;
using System;

public partial class Save_Game_Button : Button
{
    public override void _Pressed()
    {
        GLOBAL_STATS._Save_Game(GetParent<Save_Load_Menu_Section>()._save_slot);

		GetParent().GetParent<Save_Load_menu_base>().load_update();
    }
}
