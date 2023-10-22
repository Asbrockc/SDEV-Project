using Godot;

public partial class Item_bounce : Area3D
{
	private Obj_item _main_item;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_main_item = GetParent<Obj_item>();
	}

	public void _bounce_area(Node3D _node)
	{
		if (!_node.IsInGroup("Item"))
		{
			if (_node.IsInGroup("Player") && _main_item.count > 30)
			{
				_main_item._used = true;
				if (_main_item._my_base == "exp")
					GLOBAL_FUNCTIONS.Play_Sound(((Obj_player_base_script)_node)._exp_pickup, .9f);
				else if (_main_item._my_base == "hp")
					GLOBAL_FUNCTIONS.Play_Sound(((Obj_player_base_script)_node)._item_pickup);
			}
		}
	}
}
