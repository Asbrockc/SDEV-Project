using Godot;

public partial class Start_Button : Button
{
    public override void _Pressed()
    {
		Control _active_chat = (Control)ResourceLoader.Load<PackedScene>("res://SCENES/GUI/Player_name_menu.tscn").Instantiate();
		this.GetParent().AddChild(_active_chat);
		_active_chat.Position += new Vector2(0, 150);
    }
}
