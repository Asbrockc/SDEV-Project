using Godot;

public partial class attribute_leveler_parent : Control
{
	public Level_up_menu _parent_node = null;
	public Button _up_button = null;
	public Button _down_button = null;
	public Label _bonus_label = null;
	public Label _attribute_label = null;

	public int _current_level = 0;

	public override void _Ready()
	{
		_parent_node = this.GetParent<Level_up_menu>();

		_up_button = this.GetChild<Button>(2);
		_down_button = this.GetChild<Button>(3);
		_bonus_label = this.GetChild<Label>(4);
		_attribute_label = this.GetChild<Label>(5);
	}

	public override void _Process(double delta)
	{
		_bonus_label.Text = _current_level.ToString();
	}
}
