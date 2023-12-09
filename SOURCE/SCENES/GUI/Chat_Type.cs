using Godot;

public partial class Chat_Type : Label
{
	public override void _Ready()
	{
		this.GetParent<Chat_Box>()._chat_typer = this;
	}
}
