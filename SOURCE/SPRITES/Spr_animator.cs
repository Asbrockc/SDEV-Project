using Godot;

public partial class Spr_animator : AnimationPlayer
{
	public override void _Ready()
	{
		Spr_base_script _mysprite = this.GetParent<Spr_base_script>();
		_mysprite.GetParent<Obj_physics_base>()._Animator = this;
	}

	public override void _Process(double delta)
	{
	}

    public override string ToString()
    {
        return "Test to see if I reference this";
    }
}
