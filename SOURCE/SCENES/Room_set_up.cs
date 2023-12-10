using Godot;
using System;

public partial class Room_set_up : Node3D
{
	[Export] public String _room_music = "null";
	[Export] public bool _create_transition = true;

	private bool _ran_once = true;
	[Export] private string _test_sting = "nope";

	public bool _trans_time = false;

	public override void _Ready()
	{
		GLOBAL_STATS._current_room_reference = this;

		if (_create_transition)
		{
			Room_transition_improvement _tran = (Room_transition_improvement)ResourceLoader.Load<PackedScene>("res://ROOMS/Room_transition_obj_v2.tscn").Instantiate();
			AddChild(_tran);
			_tran._parent = this;
			
			GLOBAL_STATS._player._state = 0;
			if (_room_music != "null")
			{
				if (_room_music != GLOBAL_FUNCTIONS._audio_emitter._next_song)
					GLOBAL_FUNCTIONS.Change_Music(_room_music);
			}

			GLOBAL_STATS._Camera._target = GLOBAL_STATS._player;
		}


		Node3D _target_Door = null;

		foreach (Node node in GetChildren())
		{
			//GD.Print("foreach");
			if (node.IsInGroup(GLOBAL_STATS._group))
			{
				//GD.Print("GOT IT");
				_target_Door = (Node3D)node;
				break;
			}
		}

		if (_target_Door != null)
		{
			GLOBAL_STATS._player.Position = new Vector3(
				_target_Door.GlobalPosition.X + GLOBAL_STATS._x_off,
				_target_Door.GlobalPosition.Y,
				_target_Door.GlobalPosition.Z + GLOBAL_STATS._y_off);

			if(GLOBAL_STATS._Camera._target != null)
				GLOBAL_STATS._Camera.Position = new Vector3(
					GLOBAL_STATS._Camera._target.Position.X , 
					GLOBAL_STATS._Camera._target.Position.Y + GLOBAL_STATS._Camera._y_dis, 
					GLOBAL_STATS._Camera._target.Position.Z + GLOBAL_STATS._Camera._z_dis);
		}

		//GLOBAL_STATS._player._state = 0;
	}


    public void change_scene(string _new_room)
	{
		if (_ran_once)
		{
			GetTree().ChangeSceneToFile(_new_room);
			_ran_once = true;
			/*
			var level = this.GetTree().Root.GetNode("Room_one");
			this.GetTree().Root.RemoveChild(level);
			level.CallDeferred("free");

			var new_level_resource = ResourceLoader.Load<PackedScene>(_new_room);
			var next_level = new_level_resource.Instantiate();

			this.GetTree().Root.AddChild(next_level);
			*/

		}
	}

    public override void _Process(double delta)
    {
		if (_trans_time)
		{
			change_scene(GLOBAL_STATS._save_current_room);
		}

    }

}
