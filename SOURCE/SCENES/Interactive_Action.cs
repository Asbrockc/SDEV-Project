using Godot;
using System;

public partial class Interactive_Action : Area3D
{
	[Export] public String _label = "???";
	[Export] public String[] _message = {"???","???","???"};
	
	public virtual void Test_interact_function()
	{
		GLOBAL_FUNCTIONS.Call_Chatbox(_message);
	}
}
