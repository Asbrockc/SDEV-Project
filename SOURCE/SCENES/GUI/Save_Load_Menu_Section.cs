using Godot;

public partial class Save_Load_Menu_Section : NinePatchRect
{
	public int _save_slot = 0;

	public Button _save_button;
	public Button _load_button;

	public Label _name_label;
	public Label _level_label;

	public override void _Ready()
	{
		_save_button = this.GetChild<Button>(0);
		_load_button = this.GetChild<Button>(1);

		_name_label = this.GetChild<Label>(2);
		_level_label = this.GetChild<Label>(3);
	}
}
