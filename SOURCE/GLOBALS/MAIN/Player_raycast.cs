using Godot;

///<summary>
/// Unused class to define a player raycast 
/// just bounces off the player and gets a refenece to items that are around them
///</summary>
public partial class Player_raycast : RayCast3D
{
	Node3D _current_surface = null;
	Sprite3D _shadow = null;

    public override void _Ready()
    {
        _shadow = this.GetChild<Sprite3D>(0);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Node3D _test2 = (Node3D)this.GetCollider();
		if (_test2 != null && _test2.IsInGroup("floor"))
		{
			_current_surface = _test2;
			//GD.Print(_test2);

			_shadow.GlobalPosition = 
			new (this.GetParentNode3D().GlobalPosition.X,
				_test2.GlobalPosition.Y + 1,
				this.GetParentNode3D().GlobalPosition.Z);
		}
	}
}
