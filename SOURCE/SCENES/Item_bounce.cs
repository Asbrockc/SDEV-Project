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
			//if (_node.IsInGroup("Player"))
			{
				//GD.Print(_node);
				//_main_item.Free();
			}
			//else
			{
				/*
				     var spaceState = GetWorld3D().DirectSpaceState;
					var query = PhysicsRayQueryParameters3D.Create(_main_item.Position, _node.Position);
					query.Exclude = new Godot.Collections.Array<Rid> { GetRid() };
					var result = spaceState.IntersectRay(query);

					Vector3 _position_of_colidor = (Vector3)result["normal"];
				//Vector3 _test = _node.Position - _main_item.Position;
				GD.Print(result);

				if (_position_of_colidor.X != 0)
					_main_item._hspd = -_main_item._hspd;
				else if (_position_of_colidor.Z != 0)
					_main_item._vspd = -_main_item._vspd;*/
			}
		}
	}
}
