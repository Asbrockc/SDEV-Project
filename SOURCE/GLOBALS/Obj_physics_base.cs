using Godot;




///<summary>
/// A new base phsycis class that inherits the character body3D
/// This will supply the basic fields I need to find tune movment that all
/// physics based objects will use.
///</summary>
public partial class Obj_physics_base : CharacterBody3D
{
	public float Speed = 3.5f;
	public const float JumpVelocity = 8f;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public AnimationPlayer _Animator;

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

	/// <summary>
	/// defines hspd and vspd so every physics class can just handle how they move through variables 
	/// that can be interupted 
	/// i.e., every class calls thisapply speed so to prevent unwanted movement anywhere in the code before this point
	/// the _hspd and _vspd can just be zerod out. 
	/// </summary>
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
