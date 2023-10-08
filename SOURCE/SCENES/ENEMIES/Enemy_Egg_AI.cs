using Godot;
using System;

public partial class Enemy_Egg_AI : Obj_enemy_base
{
	private AnimationPlayer _animator = null;

    public override void _Process(double delta)
    {
        drop_amount = 2;
		_animator = this.GetNode("Spr_Egg_enemy").GetChild<AnimationPlayer>(0);
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

	public override Vector3 move_to_player_state(double delta, Vector3 velocity)
	{
		if (_animator != null)
			_animator.Play(_base + "walk");
		return base.move_to_player_state(delta, velocity);
	}
}
