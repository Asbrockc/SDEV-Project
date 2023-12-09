using Godot;

public partial class Load_Game_Button : Button
{
    public override void _Pressed()
    {
        if (GLOBAL_STATS._Load_Game(GetParent<Save_Load_Menu_Section>()._save_slot))
        {
            Save_Load_menu_base _base = GetParent().GetParent<Save_Load_menu_base>();
            GLOBAL_FUNCTIONS.Play_Sound(_base._save_game_sound, .8f, false);
            GLOBAL_FUNCTIONS.UI_Visibiity(true);
        }
        else
        {		
            this.Visible = false;
            GetParent<Save_Load_Menu_Section>()._name_label.Text = "CORRUPTED SAVE";
            GetParent<Save_Load_Menu_Section>()._name_label.AddThemeColorOverride("font_color", new Color("ff0000"));
            GetParent<Save_Load_Menu_Section>()._level_label.Text = "";
        }
    }
}
