using Godot;

public partial class Music_volume_bar : ProgressBar
{
	public override void _Process(double delta)
	{
		this.Value = GLOBAL_FUNCTIONS._audio_emitter._music_volume;
	}

    public override void _GuiInput(InputEvent @event)
    {
		if (@event.GetType().ToString() == "Godot.InputEventMouseMotion")
		{
			int _curr_loc = (int)((InputEventMouseMotion)@event).Position.X/36;
			
			if (Input.IsMouseButtonPressed(MouseButton.Left) && _curr_loc >= 0 && _curr_loc <= 10)
			{
				GLOBAL_FUNCTIONS._audio_emitter._music_volume = _curr_loc;
				GLOBAL_FUNCTIONS._audio_emitter.update_music_volumn();
			}
		}
    }
}
