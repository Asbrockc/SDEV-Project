using Godot;
using System;

public partial class Chat_Timer : Timer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.GetParent<Chat_Type>().GetParent<Chat_Box>()._chat_timer = this;
	}

	public void _on_timeout()
	{
		this.GetParent<Chat_Type>().VisibleCharacters++;
	}
}
