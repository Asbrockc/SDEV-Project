using Godot;
using System;

public partial class Name_confirmation : Button
{
	private Label _error_label;
	private string _error_message;


	public override void _Ready()
	{
		_error_label = this.GetChild<Label>(0);
	}

	public override void _Process(double delta)
	{
		_error_label.Text = _error_message;
	}

    public override void _Pressed()
    {
		string _test_string = this.GetParent<LineEdit>().Text;
		if (_test_string == "")
			_error_message = "No Name Entered.";
		else if (_test_string.Length > 15)
			_error_message = "Name Is Too Long.";
		else if (hasDigit(_test_string))
			_error_message = "Name Cannot Have Numbers.";
		else
		{
			GLOBAL_STATS._player_name = this.GetParent<LineEdit>().Text;
			GLOBAL_FUNCTIONS.Room_Transition("res://ROOMS/Room_town.tscn", "Save_Point", 0, 1);
			GLOBAL_FUNCTIONS.UI_Visibiity(true);
		}
    }

	private bool hasDigit(string _name)
	{
		for (int i = 0; i < _name.Length; i++)
		{
			if (Char.IsDigit(_name, i))
				return true;
		}

		return false;
	}
}
