using Godot;

public partial class Chat_Timer : Timer
{
	public AudioStreamWav _item_pickup = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_chat_sound.wav");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.GetParent<Chat_Type>().GetParent<Chat_Box>()._chat_timer = this;
	}

	public void _on_timeout()
	{
		GLOBAL_FUNCTIONS.Play_Sound(_item_pickup, 0.8f);
		this.GetParent<Chat_Type>().VisibleCharacters += 2;
	}
}
