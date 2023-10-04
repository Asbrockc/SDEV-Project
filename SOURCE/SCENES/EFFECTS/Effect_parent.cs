using Godot;
using System;

public partial class Effect_parent : Sprite3D
{
	public override void _Ready()
	{
		this.GetChild<AnimationPlayer>(0).Play("Animate");
	}	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void free_myself()
	{
		this.GetParent<Node3D>().RemoveChild(this);
		//GD.Print("Cleared");
		this.QueueFree();
	}
}
