using Godot;
using System;

public partial class Name_box_cancel : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Pressed()
    {
        Node _base = this.GetParent().GetParent();
		_base.GetParent().RemoveChild(_base);
		_base.QueueFree();
    }
}
