using Godot;
using System;

public partial class Obj_player_animator : AnimationPlayer
{
	public override void _Ready()
	{
		Obj_player_sprite _mysprite = this.GetParent<Obj_player_sprite>();
		_mysprite.GetParent<Obj_pLayer_script>()._Animator = this;
	}

	public override void _Process(double delta)
	{
	}

    public override string ToString()
    {
        return "Test to see if I reference this";
    }
}
