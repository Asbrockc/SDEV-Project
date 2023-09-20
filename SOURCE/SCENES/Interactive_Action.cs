using Godot;
using System;

public partial class Interactive_Action : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public virtual void Test_interact_function()
	{
		GD.Print("here ya go");
		GLOBAL_FUNCTIONS.Call_Chatbox(
			"I guess I'll just talk to a tree to test this out", 
			"At this point I can't see any reason it is not going to work.", 
			"But I have been surpised before...");
	}
}
