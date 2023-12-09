using Godot;

public partial class Save_Load_menu_base : Control
{

	public AudioStreamMP3 _save_game_sound = null;

	public bool _save_set_up = false;
	public bool _is_save_menu = true;

	public string _save_group = "";

	public override void _Ready()
	{
		load_update();
	}

	public void load_update()
	{
		for (int i = 0; i < 3; i++)
		{
			Save_Load_Menu_Section _curr = (Save_Load_Menu_Section)GetChildren()[i];
			_curr._save_slot = i;
		
			if (GLOBAL_STATS._File_Exists(i))
			{
				ConfigFile _config = GLOBAL_STATS._Load_Game_info(i);
				_curr._name_label.Text = _config.GetValue("Player Name", 0.ToString()).ToString();
				_curr._level_label.Text = _config.GetValue("Player Stat", GLOBAL_STATS.I_LEVEL.ToString()).ToString();
			}
			else
			{
				_curr._name_label.Text = "EMPTY";
				_curr._level_label.Text = "";
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_save_set_up)
		{
			if (_is_save_menu)
			{
				_save_game_sound = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_save_game.mp3");
				for (int i = 0; i < 3; i++)
				{
					((Save_Load_Menu_Section)GetChildren()[i])._save_button.Visible = true;
					((Save_Load_Menu_Section)GetChildren()[i])._save_button.Disabled = false;
				}
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					if (GLOBAL_STATS._File_Exists(i))
					{
						((Save_Load_Menu_Section)GetChildren()[i])._load_button.Visible = true;
						((Save_Load_Menu_Section)GetChildren()[i])._load_button.Disabled = false;
					}
				}
			}
		}
	}
}
