using Godot;
using System;

public partial class Effect_area_shockwave : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_player_hit(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
			GD.Print("hit");
	}
}
