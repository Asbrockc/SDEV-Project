using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Global class that will hold and handle player specific stats
/// This includes their health, exprience, and attributes
/// as well as flags that showcase what has been accomished or not
/// The stats can be saved and loaded using the corresponding functions
/// </summary>
public partial class GLOBAL_STATS : Node
{
	public const int I_HEALTH = 0;
	public const int I_EXPERIENCE = 1;
	public const int I_LEVEL = 2;
	public const int I_STRENGTH = 3;
	public const int I_DEFENCE = 4;
	public const int I_NEXT_LEVEL_IN = 5;
	public const int I_MUSIC_VOLUME = 6;
	public const int I_SOUND_VOLUME = 7;
	public const int I_ZZ = 8;
	public const int I_BONUS_POINTS = 9;
	public const int I_MAX_HEALTH = 10;

	/// <summary>Enums with int value that correspond to the completion flags lists</summary>
	public enum FLAG_INDEX : ushort
	{
		Beat_boss_1 = 0,
		Beat_boss_2 = 1,
		Locked_door_1 = 2,
		Boss_door_1 = 3,
	}

	static public bool _pause = false;


	static public Obj_player_base_script _player;
	static public Obj_player_camera _Camera;
	static public Room_set_up _current_room_reference = null;
	static public GLOBAL_SCENE _main_scene;

	static public int _current_save_slot = 0;
	static public String _save_location = "user://save";
	static public String _save_file_type = ".save";
	
	
	static public String _player_name = "???";
	static public String _player_room = "???";	
	static public String _save_group = "???";

	/// <summary>List that holds all of the player stats</summary>
	static public List<int> _player_stats = new List<int>()
	{
		6, //player health
		0, //base experience
		1, //base level
		1, //base strength
		1, //base defence
		10, //next level in 
		0, // sound_volumne
		0, // music_volumne
		0, // z player location
		0, //bonus points
		6 //max health

	};

	/// <summary>List that contains all games completion flags</summary>
	static public List<bool> _completion_flags = new List<bool>()
	{
		false, //first boss not beaten
		false, //second boss not beaten
		false, //door_one_open
		false //boss_door_1
	};

	/// <summary>
	/// Static function that will keep track of the experience the player is collecting 
	/// Once enough is collected it will update the level, experence needed for next level
	/// and give the player a bonus point to spend
	/// </summary>
	static public void _Level_Manager()
	{
		int _exp = _player_stats[I_EXPERIENCE];
		int _next_in = _player_stats[I_NEXT_LEVEL_IN];

		if (_exp >= _next_in)
		{
			GLOBAL_FUNCTIONS.Create_Effect(_player, "Effect_levelup.tscn", false);
			GLOBAL_FUNCTIONS.Play_Sound(_player._level_up, 1, false);
			int _remainder = _exp - _next_in;

			_player_stats[I_NEXT_LEVEL_IN] += _player_stats[I_NEXT_LEVEL_IN]/2;
			_player_stats[I_EXPERIENCE] = 0;

			_player_stats[I_LEVEL]++;
			_player_stats[I_BONUS_POINTS]++;
		}
	}

	/// <summary>
	/// Function that will take the players name, current stats, and 
	/// </summary>
	static public void _Save_Game(int slot)
	{

		String _current_save_location = _save_location + slot.ToString() + _save_file_type;
		ConfigFile _save_configure = new ConfigFile();

		_player_stats[I_MUSIC_VOLUME] = GLOBAL_FUNCTIONS._audio_emitter._music_volume;
		_player_stats[I_SOUND_VOLUME] = GLOBAL_FUNCTIONS._audio_emitter._game_volume;

		_save_group = "Save_Point";

		_player_room = _current_room_reference.GetTree().CurrentScene.SceneFilePath;

		_save_configure.SetValue("Player Name", 0.ToString(), _player_name);
		_save_configure.SetValue("Player Loc", 0.ToString(), _player_room);
		_save_configure.SetValue("Player Target", 0.ToString(), _save_group);
		

		for (int i = 0; i < _player_stats.Count; i++)
			_save_configure.SetValue("Player Stat", i.ToString(), _player_stats[i]);

		for (int i = 0; i < _completion_flags.Count; i++)
			_save_configure.SetValue("Player flags", i.ToString(), _completion_flags[i]);

		_save_configure.Save(_current_save_location);
	}


	static public bool _File_Exists(int slot)
	{
		String _current_save_location = _save_location + slot.ToString() + _save_file_type;
		return FileAccess.FileExists(_current_save_location);
	}

	/// <summary>
	/// Class <c>Point</c> models a point in a two-dimensional plane.
	/// </summary>
	static public void _Load_Game(int slot)
	{
		ConfigFile _load_configure = _Load_Game_info(slot);

		_player_name = _load_configure.GetValue("Player Name", 0.ToString()).ToString();
		_player_room = _load_configure.GetValue("Player Loc", 0.ToString()).ToString();
		_save_group = _load_configure.GetValue("Player Target", 0.ToString()).ToString();

		_player_stats[I_MUSIC_VOLUME] = GLOBAL_FUNCTIONS._audio_emitter._music_volume;
		_player_stats[I_SOUND_VOLUME] = GLOBAL_FUNCTIONS._audio_emitter._game_volume;

		for (int i = 0; i < _player_stats.Count; i++)
			_player_stats[i] = _load_configure.GetValue("Player Stat", i.ToString()).ToString().ToInt();

		GLOBAL_FUNCTIONS._audio_emitter._music_volume = _player_stats[I_MUSIC_VOLUME];
		GLOBAL_FUNCTIONS._audio_emitter._game_volume = _player_stats[I_SOUND_VOLUME];

		for (int i = 0; i < _completion_flags.Count; i++)
			_completion_flags[i] = bool.Parse(_load_configure.GetValue("Player flags", i.ToString()).ToString());

		GLOBAL_FUNCTIONS.Room_Transition(_player_room, _save_group, 0, 1);
	}

	static public ConfigFile _Load_Game_info(int slot)
	{
		String _current_save_location = _save_location + slot.ToString() + _save_file_type;

		ConfigFile _load_configure = new ConfigFile();
		_load_configure.Load(_current_save_location);

		return _load_configure;
	}
}
