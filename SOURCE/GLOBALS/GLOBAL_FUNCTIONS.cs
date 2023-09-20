using Godot;
using System;

public partial class GLOBAL_FUNCTIONS : Node
{

	static public GLOBAL_SCENE _main_scene;
	static public Chat_Box _active_chat = null;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print("Just a test to see if git finds this");
		//GD.Print("well it's here");
	}



	static public void Call_Chatbox(params string[] _messages)
	{
		
		
		//GD.Print("make chat");
		if (_active_chat == null)
		{
			_active_chat = (Chat_Box)ResourceLoader.Load<PackedScene>("res://SCENES/GUI/Chat_Box.tscn").Instantiate();
			_main_scene.AddChild(_active_chat);
			_active_chat.Set_Messages(_messages);
		}
	}
}
