using Godot;
using System;

public partial class Enemy_Final_boss_base : Boss_enemy_base
{

    private bool _between_turns = true;
    private int _warp_count = 0;
    private int _warp_index = 0;
    private int _warp_max = 60;

    public override void _Ready()
    {
        base._Ready();
        delay_timer = 300;
    }

    public override Vector3 move_to_player_state(double delta, Vector3 velocity)
    {
        _Animator.Play("down_walk");
        if (_warp_count <= _warp_max)
        {
            _warp_index = GLOBAL_FUNCTIONS.Choose(1,2,3,4);
            _warp_count++;

            if (_warp_count == _warp_max)
            {
                Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);
                _between_turns = !_between_turns;
            }
        }
        else
        {
            if (_between_turns)
                this.GlobalPosition = this.GetParent<Enemy_Final_Boss_core>()._target[0].GlobalPosition;
            else
            {
                Final_Boss_Target _targ = this.GetParent<Enemy_Final_Boss_core>()._target[_warp_index];
                Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(_targ, "Effect_Explosion.tscn", true);
                this.GlobalPosition = _targ.GlobalPosition;
            }
            
            _warp_count = 0;
        }

        return velocity;//move_left_right_to_player_state(delta, velocity);
    }
}
