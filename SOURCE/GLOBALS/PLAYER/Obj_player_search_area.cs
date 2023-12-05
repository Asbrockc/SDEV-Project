using Godot;

/// <summary>
/// A scripte for the area surounding the player character to search for interactable items
/// </summary>
public partial class Obj_player_search_area : Area3D
{
	private Obj_player_base_script _player_parent;

	public override void _Ready()
	{
		_player_parent = this.GetParent<Obj_player_base_script>();
	}

	/// <summary>
	/// if there is an object in the area and it is an interact area, give the player a reference to it
	/// </summary>
	public void _on_area_entered(Area3D _node)
	{
		//GD.Print("ENTER: " + _node.ToString());
		
		if (_node.IsInGroup("Interact"))// && _player_parent._node_in_range == null)
		{
			_player_parent._node_in_range = (Interactive_Action)_node;
			//GD.Print("touch");
		}
	}

	/// <summary>
	/// when anything leaves the area it clears the players interact reference
	/// that way they can attack again
	/// </summary>
	public void _on_area_exited(Area3D _node)
	{
		//GD.Print("LEAVE: " + _node.ToString());

		if (_node == _player_parent._node_in_range)
		{
			_player_parent._node_in_range = null;
			//GD.Print("letgo");
		}
	}
}
