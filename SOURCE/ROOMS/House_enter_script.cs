using Godot;
using System;

public partial class House_enter_script : Area3D
{
	public void _enter(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
			GetParent<Node3D>().Visible = false;
	}

	public void _exit(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
			GetParent<Node3D>().Visible = true;
	}
}
