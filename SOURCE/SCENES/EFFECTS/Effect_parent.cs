using Godot;

///<summary>
/// effect parent just runs the animation and calls the free_myself() function
///</summary>
public partial class Effect_parent : Sprite3D
{
	public override void _Ready()
	{
		this.GetChild<AnimationPlayer>(0).Play("Animate");
	}	

	///<summary>
	/// once the animation is done it will free itself from the scene tree when it gets the chance
	///</summary>
	public void free_myself()
	{
		this.GetParent<Node3D>().RemoveChild(this);
		//GD.Print("Cleared");
		this.QueueFree();
	}
}
