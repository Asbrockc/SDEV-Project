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
	public const int I_XX = 6;
	public const int I_YY = 7;
	public const int I_ZZ = 8;
	public const int I_BONUS_POINTS = 9;

	/// <summary>Enums with int value that correspond to the completion flags lists</summary>
	public enum Flag_Index : ushort
	{
		Beat_boss_1 = 0,
		Beat_boss_2 = 1,
	}

	static public Obj_player_base_script _player;
	static public Obj_player_camera _Camera;
	static public Node3D _current_room_reference;
	static public GLOBAL_SCENE _main_scene;

	static public int _current_save_slot = 0;
	static public String _save_location = "user://save";
	static public String _save_file_type = ".save";
	static public String _player_name = "???";

	/// <summary>List that holds all of the player stats</summary>
	static public List<int> _player_stats = new List<int>()
	{
		3, //player health
		0, //base experience
		1, //base level
		1, //base strength
		1, //base defence
		10, //next level in 
		0, // x player locaton
		0, // y player location
		0, // z player location
		0 //bonus points
	};

	/// <summary>List that contains all games completion flags</summary>
	static public List<bool> _completion_flags = new List<bool>()
	{
		false, //first boss not beaten
		false, //second boss not beaten
	};





	static public void _Level_Manager()
	{
		int _exp = _player_stats[I_EXPERIENCE];
		int _next_in = _player_stats[I_NEXT_LEVEL_IN];

		if (_exp >= _next_in)
		{
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
	static public void _Save_Game()
	{
		String _current_save_location = _save_location + _current_save_slot.ToString() + _save_file_type;

		ConfigFile _save_configure = new ConfigFile();

		_save_configure.SetValue("Player Name", 0.ToString(), _player_name);
		for (int i = 0; i < _player_stats.Count; i++)
			_save_configure.SetValue("Player Stat", i.ToString(), _player_stats[i]);

		for (int i = 0; i < _completion_flags.Count; i++)
			_save_configure.SetValue("Player flags", i.ToString(), _completion_flags[i]);

		_save_configure.Save(_current_save_location);
	}

	/// <summary>
	/// Class <c>Point</c> models a point in a two-dimensional plane.
	/// </summary>
	static public void _Load_Game()
	{
		String _current_save_location = _save_location + _current_save_slot.ToString() + _save_file_type;

		ConfigFile _load_configure = new ConfigFile();
		_load_configure.Load(_current_save_location);

		_player_name = _load_configure.GetValue("Player Name", 0.ToString()).ToString();

		for (int i = 0; i < _player_stats.Count; i++)
			_player_stats[i] = _load_configure.GetValue("Player Stat", i.ToString()).ToString().ToInt();

		for (int i = 0; i < _completion_flags.Count; i++)
			_completion_flags[i] = bool.Parse(_load_configure.GetValue("Player flags", i.ToString()).ToString());

		GD.Print(_player_name);
		for (int i = 0; i < _player_stats.Count; i++)
			GD.Print(_player_stats[i].ToString());

		for (int i = 0; i < _completion_flags.Count; i++)
			GD.Print(_completion_flags[i].ToString());


	}
}
