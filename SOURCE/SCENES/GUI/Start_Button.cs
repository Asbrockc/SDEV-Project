using Godot;
using System;

public partial class Start_Button : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Pressed()
    {


		Control _active_chat = (Control)ResourceLoader.Load<PackedScene>("res://SCENES/GUI/Player_name_menu.tscn").Instantiate();
		this.AddChild(_active_chat);

		//GLOBAL_FUNCTIONS.Change_Scene(_desination);

		
    }
}
