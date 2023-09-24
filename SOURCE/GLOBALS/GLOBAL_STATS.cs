using Godot;
using System;
using System.Collections.Generic;

public partial class GLOBAL_STATS : Node
{
	public enum Stat_Index : ushort
	{
		Health = 0,
		Experience = 1,
		Level = 2,
		Strength = 3,
		Defence = 4,
		Next_Level_In = 5,
		XX = 6,
		YY = 7,
		ZZ = 8,
	}

	public enum Flag_Index : ushort
	{
		Beat_boss_1 = 0,
		Beat_boss_2 = 1,
	}

	static public Obj_pLayer_script _player;
	static public Obj_player_camera _Camera;
	static public Node3D _current_room_reference;
	static public GLOBAL_SCENE _main_scene;

	static public int _current_save_slot = 0;
	static public string _save_location = "user://save";
	static public string _save_file_type = ".save";
	static public String _player_name = "???";

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
		0 // z player location
	};

	static public List<bool> _completion_flags = new List<bool>()
	{
		false, //first boss not beaten
		false, //second boss not beaten
	};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	static public void _Save_Game()
	{
		String _current_save_location = _save_location + _current_save_slot.ToString() + _save_file_type;

		ConfigFile _save_configure = new ConfigFile();

		_save_configure.SetValue("Player Name", _player_name, 0);
		for (int i = 0; i < _player_stats.Count; i++)
			_save_configure.SetValue("Player Stat", i.ToString(), _player_stats[i]);

		for (int i = 0; i < _completion_flags.Count; i++)
			_save_configure.SetValue("Player flags", i.ToString(), _completion_flags[i]);

		_save_configure.Save(_current_save_location);
	}

	static public void _Load_Game()
	{
		String _current_save_location = _save_location + _current_save_slot.ToString() + _save_file_type;

		ConfigFile _load_configure = new ConfigFile();
		_load_configure.Load(_current_save_location);

		_player_name = _load_configure.GetValue("Player Name", _player_name, 0).ToString();

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
