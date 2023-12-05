using Godot;
using System;

public partial class Room_set_up : Node3D
{
	[Export] public String _room_music = "null";

	private bool _ran_once = true;
	[Export] private string _test_sting = "nope";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Setting up next room" + _test_sting + " - " + this.GetChildren());
		GLOBAL_STATS._current_room_reference = this;
	}

    public override void _Process(double delta)
    {
		//GLOBAL_STATS._current_room_reference = this;
        //GD.Print("test 1" + this);
		//GD.Print("test 2" + GLOBAL_STATS._current_room_reference);
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
}
