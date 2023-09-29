using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class GLOBAL_FUNCTIONS : Node
{
	static public Chat_Box _active_chat = null;
	static public Audio_Emt _audio_emitter = null;
	static public Texture2D _broken_arrow = ResourceLoader.Load<Texture2D>("res://SPRITES/FILE/Arrow_2.png");
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print("Just a test to see if git finds this");
		//GD.Print("well it's here");
	}

	static public void Change_Scene(String _new_scene)
	{
		GLOBAL_STATS._main_scene.GetTree().ChangeSceneToFile(_new_scene);
	}

	static public void Play_Sound<T>(T _sound)
	{
		_audio_emitter.play_sound<T>(_sound, true);
	}

	static public void Play_Sound<T>(T _sound, bool _shift_tone)
	{
		_audio_emitter.play_sound<T>(_sound, _shift_tone);
	}

	static public float Choose_Float(params float[] _range)
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

	static public void Create_Effect(Node3D _target_node, String _effect_location, bool _is_global)
	{
		Effect_parent _test = (Effect_parent)ResourceLoader.Load<PackedScene>("res://SCENES/EFFECTS/" + _effect_location).Instantiate();
		
		if (_is_global)
		{
			GLOBAL_STATS._current_room_reference.AddChild(_test);
			_test.Position = _target_node.GlobalPosition;
		}
		else
			_target_node.AddChild(_test);
	}


	static public void Spawn_item(Vector3 _position, float _scale, int _range)
	{
		Obj_item _curr_item = (Obj_item)ResourceLoader.Load<PackedScene>("res://SCENES/obj_item_parent.tscn").Instantiate();
		GLOBAL_STATS._current_room_reference.AddChild(_curr_item);

		_curr_item.Position = _position;
		_curr_item.Scale = new Vector3(_scale,_scale,_scale);
		Random r = new Random();
		_curr_item._charge_up = r.Next(_range, _range*2);
		_curr_item._hspd = r.Next(-_range, _range);
		_curr_item._vspd = r.Next(-_range, _range);
	}

	static public Level_up_menu Summon_stat_menu()
	{
		Level_up_menu _curr_item = (Level_up_menu)ResourceLoader.Load<PackedScene>("res://SCENES/GUI/Level_up_menu.tscn").Instantiate();
		GLOBAL_STATS._current_room_reference.AddChild(_curr_item);
		GLOBAL_STATS._player._state = 2;

		return _curr_item;
	}

	static public Obj_projectile_parent Create_projectile(Node3D _source)
	{
		Obj_projectile_parent _curr_item = (Obj_projectile_parent)ResourceLoader.Load<PackedScene>("res://SCENES/EFFECTS/Obj_projectile_parent.tscn").Instantiate();
		GLOBAL_STATS._current_room_reference.AddChild(_curr_item);

		_curr_item.Position = _source.GlobalPosition;
		return _curr_item;
	}

	static public float distance_between_nodes(Node3D first, Node3D second)
	{
		return 
		((second.Position.X - first.Position.X)*(second.Position.X - first.Position.X)) +
		((second.Position.Y - first.Position.Z)*(second.Position.Y - first.Position.Z));
	}

	static public void Spawn_enemy(Vector3 _position)
	{
		Obj_enemy_base _curr_item = (Obj_enemy_base)ResourceLoader.Load<PackedScene>("res://SCENES/ENEMIES/Enemy_base.tscn").Instantiate();
		GLOBAL_STATS._current_room_reference.AddChild(_curr_item);

		_curr_item.Position = _position + new Vector3(3.0f, 0.0f, 0.0f);
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
