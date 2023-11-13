using Godot;
using System;

public partial class Enemy_Final_boss_base : Boss_enemy_base
{

    private bool _ready_to_fight = true;

    private bool _between_turns = true;
    private int _warp_count = 0;
    private int _warp_index = 0;
    private int _warp_max = 60;

    private int _jump_count = 0;

    private Vector3 _jump_height = new Vector3(0,10,0);

    private int _random_move = 0;
    private int _prior_move = 0;

    public override void _Ready()
    {
        Speed = 7.0f;
        base._Ready();
        //delay_timer = 300;
        _warp_max = 120;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsKeyPressed(Key.L))
            _ready_to_fight = true;

        if (_ready_to_fight)
            base._PhysicsProcess(delta);
        else    
            this.GlobalPosition = this.GetParent<Enemy_Final_Boss_core>()._target[0].GlobalPosition;
    }

    public override Vector3 move_to_player_state(double delta, Vector3 velocity)
    {
        //_Animator.Play("down_walk");
        if (_warp_count <= _warp_max)
        {
            _warp_index = GLOBAL_FUNCTIONS.Choose(1,2,3,4);
            _warp_count++;

            if (_warp_count == _warp_max)
            {
                Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);
                _between_turns = !_between_turns;
            }

            _immune_to_sword = false;
            _immune_to_bow = false;
           
            //if (!_between_turns)
            {
                switch (_random_move)
                {
                    case 0:
                        _jump_count = 0;
                        return base.move_to_player_state(delta, velocity);
                    case 1:
                        _jump_count = 0;
                        return move_left_right_to_player_state(delta, velocity);
                }
            }
            
        }
        else
        {
            _hspd = 0;
            _vspd = 0;
            if (_between_turns)
            {
                _prior_move =_random_move;
                while (_prior_move == _random_move)
                    _random_move = GLOBAL_FUNCTIONS.Choose(0,1,2);

                this.GlobalPosition = this.GetParent<Enemy_Final_Boss_core>()._target[0].GlobalPosition;
            }
            else
            {
                Final_Boss_Target _targ = this.GetParent<Enemy_Final_Boss_core>()._target[_warp_index];
                Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(_targ, "Effect_Explosion.tscn", true);
                this.GlobalPosition = _targ.GlobalPosition;

                 switch (_random_move)
                {
                    case 2:
                        _jump_spd = 10;
                        _Animator.Play("jump");  
                        _state = 5;
                    break;
                }
            
            }

            Speed = GLOBAL_FUNCTIONS.Choose(6.5f, 4.5f);
            _warp_count = 0;
        }

        return velocity;
    }

  
    public override Vector3 jump_state(double delta, Vector3 velocity)
	{
		counter++;
		if (IsOnFloor() && counter > 5)
		{
            if (_jump_count < 3)
            {
                _warp_count = _warp_max;
                _jump_count++;
                GD.Print(_jump_count);
            }

        
			counter = 0;
			//next_jump_in = GLOBAL_FUNCTIONS.Choose(200,400,500);
			_state = 4;
		}

		return velocity;
	}

}
