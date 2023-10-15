using Godot;
using System;

public partial class Enemy_Egg_AI : Obj_enemy_base
{
    //public AnimationPlayer _animator = null;

    public override void _Ready()
    {
        base._Ready();
        _Animator.Pause();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

	public override Vector3 move_to_player_state(double delta, Vector3 velocity)
	{
		if (_Animator != null)
			_Animator.Play(_base + "walk");
		return base.move_to_player_state(delta, velocity);
	}
}
