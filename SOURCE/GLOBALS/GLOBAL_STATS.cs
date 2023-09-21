using Godot;
using System;

public partial class GLOBAL_STATS : Node
{
	static public Obj_pLayer_script _player;
	static public Obj_player_camera _Camera;
	static public Node3D _current_room_reference;
	static public GLOBAL_SCENE _main_scene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
