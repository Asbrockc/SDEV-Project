using Godot;
using System;

public partial class Item_bounce : Area3D
{
	private Obj_item _main_item;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_main_item = GetParent<Obj_item>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _bounce_area(Node3D _node)
	{
		if (!_node.IsInGroup("Item"))
		{
			if (_node.IsInGroup("Player") && _main_item.count > 30)
			{
				_main_item._used = true;
				GLOBAL_FUNCTIONS.play_sound(((Obj_player_base_script)_node)._item_pickup);
			}
		}
	}
}
