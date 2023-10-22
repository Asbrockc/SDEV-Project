using Godot;

public partial class Save_Game_Button : Button
{
    public override void _Pressed()
    {
        GLOBAL_STATS._Save_Game(GetParent<Save_Load_Menu_Section>()._save_slot);

        Save_Load_menu_base _base = GetParent().GetParent<Save_Load_menu_base>();
		_base.load_update();
        GLOBAL_FUNCTIONS.Play_Sound(_base._save_game_sound, .8f, false);
    }
}
