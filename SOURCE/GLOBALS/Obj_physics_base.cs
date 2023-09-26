using Godot;
using System;




///<summary>
/// A new base phsycis class that inherits the character body3D
/// This will supply the basic fields I need to find tune movment that all
/// physics based objects will use.
///</summary>
public partial class Obj_physics_base : CharacterBody3D
{

	public const float Speed = 3.5f;
	public const float JumpVelocity = 8f;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public int _state = 0;
	public float _hspd = 0.0f;
	public float _vspd = 0.0f;
	public float _jump_spd = 0.0f;


	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
	}

	protected Vector3 apply_speed(Vector3 velocity)
	{
		velocity.X = _hspd;
		velocity.Z = _vspd;
		//velocity.Y = _jump_spd;

		return velocity;
	}

	public override string ToString()
	{
		return "This item uses the physics base!";
	}
}
