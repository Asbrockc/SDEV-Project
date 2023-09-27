using Godot;
using System;

public partial class UI_health_dots : Sprite2D
{
	private Texture2D _text = ResourceLoader.Load<Texture2D>("res://SPRITES/FILE/icon.svg");

	private const float _X_offset = 400.0f;
	private const float _Y_offset = 60.0f;
	private const float _space_offset = 150.0f;

	private int curr_health = 0;


    public override void _Process(double delta)
    {
		if (curr_health != GLOBAL_STATS._player_stats[GLOBAL_STATS.I_HEALTH])
		{
        	QueueRedraw();
			curr_health = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_HEALTH];
		}
    }

    public override void _Draw()
    {
		for (int i = 0; i < GLOBAL_STATS._player_stats[GLOBAL_STATS.I_MAX_HEALTH]; i++)
			DrawTexture(_text, new Vector2(_X_offset + i *_space_offset, _Y_offset), Color.Color8(1, 1, 1, 255));

		for (int i = 0; i < GLOBAL_STATS._player_stats[GLOBAL_STATS.I_HEALTH]; i++)
			DrawTexture(_text, new Vector2(_X_offset + i *_space_offset, _Y_offset));
    }
}
