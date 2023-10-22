using Godot;

public partial class Chat_Type : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.GetParent<Chat_Box>()._chat_typer = this;
	}
}
