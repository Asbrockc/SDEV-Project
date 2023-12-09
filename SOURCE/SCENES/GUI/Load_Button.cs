using Godot;

public partial class Load_Button : Button
{
    public override void _Pressed()
    {
		Save_Load_menu_base _active_chat = (Save_Load_menu_base)ResourceLoader.Load<PackedScene>("res://SCENES/GUI/Save_Load_menu_base.tscn").Instantiate();
		this.GetParent().AddChild(_active_chat);
		_active_chat.Position += new Vector2(0, 150);
		_active_chat._is_save_menu = false;
		_active_chat._save_set_up = true;
    }
}
