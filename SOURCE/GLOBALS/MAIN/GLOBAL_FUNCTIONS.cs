using Godot;
using System;

public partial class GLOBAL_FUNCTIONS : Node
{
	static public Chat_Box _active_chat = null;
	static public Audio_Emt _audio_emitter = null;
	static public Texture2D _broken_arrow = ResourceLoader.Load<Texture2D>("res://SPRITES/FILE/Arrow_2.png");

	static public Room_transition_obj _transition = null;
	
	static public void Change_Scene(String _new_scene)
	{
		GLOBAL_STATS._main_scene.GetTree().ChangeSceneToFile(_new_scene);
	}

	static public void UI_Visibiity(bool _visible)
	{
		GLOBAL_STATS._main_scene.GetNode<Control>("UI_Player_stats").Visible = _visible;
	}

	static public void Play_Sound<T>(T _sound, float _volume_perc = 1.0f, bool _shift_tone = true)
	{
		_audio_emitter.play_sound<T>(_sound, _volume_perc, _shift_tone);
	}

	static public void Change_Music(String _sound_location, float _rate = 1)
	{
		_audio_emitter._fade_rate = _rate;
		_audio_emitter._next_song = _sound_location;
		_audio_emitter._music_run_state = 0;
	}

	///<summary>
	/// Universal direction decider for all entities
	/// Each entity has a string to deside which why they are facing
	/// this takes the current direction and shifts it as needed based on their
	/// horizontal and vertical speed.
	///</summary>
	static public string Entity_Dir(string _curr, float _hspd, float _vspd)
	{
			if (_hspd > 0)
				return "right_";
			else if (_hspd < 0)
				return "left_";
			else if (_vspd > 0)
				return "down_";
			else if (_vspd < 0)
				return "up_";
			else 
				return _curr;
	}

	static public bool GetFlag(GLOBAL_STATS.FLAG_INDEX _index)
	{
		return GLOBAL_STATS._completion_flags[(int)_index];
	}

	static public void SetFlag(GLOBAL_STATS.FLAG_INDEX _index, bool _set = true)
	{
		GLOBAL_STATS._completion_flags[(int)_index] = _set;
	}

	static public T Choose<T>(params T[] _range)
	{
		Random _rand = new Random();
		return _range[_rand.NextInt64(_range.Length)];
	}

	static public void Call_Chatbox(params string[] _messages)
	{
		//GD.Print("make chat");
		if (_active_chat == null)
		{
			_active_chat = (Chat_Box)ResourceLoader.Load<PackedScene>("res://SCENES/GUI/Chat_Box.tscn").Instantiate();
			GLOBAL_STATS._main_scene.AddChild(_active_chat);
			_active_chat.Set_Messages(_messages);
		}
	}

	static public void Room_Transition(string _room, string _group, int x_off, int y_off)
	{
		Room_transition_obj _active_chat = (Room_transition_obj)ResourceLoader.Load<PackedScene>("res://ROOMS/Room_transition_obj.tscn").Instantiate();
		GLOBAL_STATS._main_scene.AddChild(_active_chat);
		_active_chat._room = _room;
		_active_chat._target_zone = _group;
		_active_chat._x_off = x_off;
		_active_chat._y_off = y_off;
	}

	static public Effect_parent Create_Effect(Node3D _target_node, String _effect_location, bool _is_global, float _x = 0.0f, float _y = 0.0f,float _z = 0.0f)
	{
		Effect_parent _test = (Effect_parent)ResourceLoader.Load<PackedScene>("res://SCENES/EFFECTS/" + _effect_location).Instantiate();
		
		if (_is_global)
		{
			GLOBAL_STATS._current_room_reference.AddChild(_test);
			_test.Position = _target_node.GlobalPosition;

			if (_x != 0 || _y != 0 || _z != 0)
				_test.Position += new Vector3(_x, _y, _z);
		}
		else
			_target_node.AddChild(_test);

		return _test;
	}
	
	static public void Spawn_item(Vector3 _position, float _scale, float _range)
	{
		Obj_item _curr_item = (Obj_item)ResourceLoader.Load<PackedScene>("res://SCENES/obj_item_parent.tscn").Instantiate();
		GLOBAL_STATS._current_room_reference.AddChild(_curr_item);

		_curr_item.GlobalPosition = _position;
		_curr_item.Scale = new Vector3(_scale,_scale,_scale);
		Random r = new Random();
		_curr_item._charge_up = Choose(_range,_range*1.5f, _range*2.0f);
		_curr_item._hspd = Choose(-_range, -_range/2.0f, -_range/4.0f, 0.0f ,_range/4.0f,_range/2.0f, _range);
		_curr_item._vspd = Choose(-_range, -_range/2.0f, -_range/4.0f, 0.0f ,_range/4.0f,_range/2.0f, _range);
		_curr_item._my_base = Choose<string>("exp","exp","exp","exp","exp","exp","exp","exp","exp","exp","hp");
	}

	static public Level_up_menu Summon_stat_menu()
	{
		if (GLOBAL_STATS._current_room_reference != null)
		{
			Level_up_menu _curr_item = (Level_up_menu)ResourceLoader.Load<PackedScene>("res://SCENES/GUI/Level_up_menu.tscn").Instantiate();
			GLOBAL_STATS._current_room_reference.AddChild(_curr_item);
			GLOBAL_STATS._player._state = 2;

			return _curr_item;
		}

		return null;
	}

	static public Obj_projectile_parent Create_projectile(Node3D _source, string _type = "res://SCENES/EFFECTS/Obj_projectile_parent.tscn")
	{
		Obj_projectile_parent _curr_item = (Obj_projectile_parent)ResourceLoader.Load<PackedScene>(_type).Instantiate();
		GLOBAL_STATS._current_room_reference.AddChild(_curr_item);
		_curr_item._my_parent = _source;
		_curr_item.Position = _source.GlobalPosition;
		return _curr_item;
	}

	static public void Shake_Camera(float _amount)
	{
		GLOBAL_STATS._Camera._shake = _amount;
	}

	static public float Distance_Between_Nodes(Node3D first, Node3D second)
	{
		return 
		((second.GlobalPosition.X - first.GlobalPosition.X)*(second.GlobalPosition.X - first.GlobalPosition.X)) +
		((second.GlobalPosition.Y - first.GlobalPosition.Z)*(second.GlobalPosition.Y - first.GlobalPosition.Z));
	}

	static public void Spawn_enemy(Vector3 _position, string _enemy_path)
	{
		Node3D _curr_item = (Node3D)ResourceLoader.Load<PackedScene>(_enemy_path).Instantiate();
		GLOBAL_STATS._current_room_reference.AddChild(_curr_item);

		_curr_item.GlobalPosition = _position;
		//_curr_item.Scale = new Vector3(.2f, .2f, .2f);
		//Random r = new Random();
		//_curr_item._charge_up = r.Next(5, 10);
		//_curr_item._hspd = r.Next(-5, 5);
		//_curr_item._vspd = r.Next(-5, 5);
	}

	static public float Gradual_Stop(float _start_number, float _rate)
	{
		if (_rate != 0)
			return _start_number - _start_number/_rate;
		else
			return 0;
	}
}
