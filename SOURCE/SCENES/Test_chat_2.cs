using Godot;
using System;

public partial class Test_chat_2 : Interactive_Action
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_label = "TEST NPC";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void Test_interact_function()
	{
		Save_Load_menu_base _active_chat = (Save_Load_menu_base)ResourceLoader.Load<PackedScene>("res://SCENES/GUI/Save_Load_menu_base.tscn").Instantiate();
		this.GetParent().AddChild(_active_chat);
		_active_chat.Position += new Vector2(0, 150);
		_active_chat._is_save_menu = true;
		_active_chat._save_set_up = true;
		/*
		GD.Print("here ya go");
		GLOBAL_FUNCTIONS.Call_Chatbox(
			"This one I am just talking to a box instead", 
			"If this works I am in very good shape.", 
			"Since now any thing I want to be interactive, all I need to do is inherit and override this function");*/
	}
}
