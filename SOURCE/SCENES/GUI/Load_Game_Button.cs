using Godot;
using System;

public partial class Load_Game_Button : Button
{
    public override void _Pressed()
    {
		//GD.Print(GetParent<Save_Load_Menu_Section>()._save_slot);
        GLOBAL_STATS._Load_Game(GetParent<Save_Load_Menu_Section>()._save_slot);
    }
}
