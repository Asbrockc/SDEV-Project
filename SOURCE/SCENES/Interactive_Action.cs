using Godot;
using System;

public partial class Interactive_Action : Area3D
{
	public String _label = "???";
	
	public virtual void Test_interact_function()
	{
		GD.Print("here ya go");
		GLOBAL_FUNCTIONS.Call_Chatbox(
			"I guess I'll just talk to a tree to test this out", 
			"At this point I can't see any reason it is not going to work.", 
			"But I have been surpised before...");
	}
}
