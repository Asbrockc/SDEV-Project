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
		GD.Print("here ya go");
		GLOBAL_FUNCTIONS.Call_Chatbox(
			"This one I am just talking to a box instead", 
			"If this works I am in very good shape.", 
			"Since now any thing I want to be interactive, all I need to do is inherit and override this function");
	}
}
