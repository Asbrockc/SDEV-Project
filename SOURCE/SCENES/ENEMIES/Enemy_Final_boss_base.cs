using Godot;
using System;

public partial class Enemy_Final_boss_base : Boss_enemy_base
{

    private bool _ready_to_fight = true;

    private bool _between_turns = true;
    private int _warp_count = 0;
    private int _warp_index = 0;
    private int _warp_max = 60;

    private bool spell_flag = true;

    private int _jump_count = 0;
    private int _fire_count = 0;

    private Vector3 _jump_height = new Vector3(0,10,0);

    private AudioStreamWav _attack_sound;

    private int _random_move = 0;
    private int _prior_move = 0;

    private AudioStreamWav _chirp_sound = null;

    public override void _Ready()
    {
        _chirp_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_bat_chirp.wav");
        _slam_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_heavy_slam.wav");
		//_flap_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_flap_small.wav");
		_attack_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_dragon_jump.wav");
		_destroy_sound = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_boom_sound.mp3");
        Speed = 4.5f;//7.0f;
        base._Ready();
        _state = 6;
        //delay_timer = 300;
        _warp_max = 150;
        _boss_music = "null"; //"res://SOUNDS/ALL_SOUNDS/MUSIC/snd_final_boss.wav";
    }

    public override void _PhysicsProcess(double delta)
    {
       // if (Input.IsKeyPressed(Key.L))
           // _ready_to_fight = true;

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
            _warp_index = GLOBAL_FUNCTIONS.Choose(1,2,3,4,5,6,7,8);
            _warp_count++;

            if (_warp_count == _warp_max)
            {
                Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);
                _between_turns = !_between_turns;
            }

            _immune_to_sword = false;
            _immune_to_bow = false;
           
            {
                switch (_random_move)
                {
                    case 0:
                        _jump_count = 0;
                        return base.move_to_player_state(delta, velocity);
                    case 1:
                        if (_warp_count == 20 && GLOBAL_FUNCTIONS.Choose(1,2,3,4) == 1)
                        {
                            {
                                Final_Boss_Target _node = this.GetParent<Enemy_Final_Boss_core>()._target[GLOBAL_FUNCTIONS.Choose(1,2,3,4,5,6,7,8)];

                                Node _temp = GLOBAL_FUNCTIONS.Spawn_enemy(_node.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Egg.tscn");
                                _temp.GetChild<Obj_enemy_base>(0)._drops = new string[]{"hp"};
                            }
                        }
                        _jump_count = 0;
                        return base.move_to_player_state(delta, velocity);
                        //return move_left_right_to_player_state(delta, velocity);
                    case 3:

                        if (spell_flag)
                        {
                            _warp_index = GLOBAL_FUNCTIONS.Choose(1,2,3,4,5,6,7,8);
                            //GD.Print(this.GetParent<Enemy_Final_Boss_core>()._target.Length);
                            Final_Boss_Target _targ = this.GetParent<Enemy_Final_Boss_core>()._target[_warp_index];
                            this.GlobalPosition = _targ.GlobalPosition;
                            Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(_targ, "Effect_Explosion.tscn", true);

                            _Animator.Play("spell");  
                            spell_flag = false;
                        }

                        if (!_Animator.IsPlaying())
                        {
                            spell_flag = true;
                    
                            GLOBAL_FUNCTIONS.Play_Sound(GLOBAL_STATS._player._player_bow_shot, 0.9f);
                            for (int i = 0; i < 4; i++)
                            {
                                Obj_projectile_fireball _arrow = (Obj_projectile_fireball)GLOBAL_FUNCTIONS.Create_projectile(this, "res://SCENES/EFFECTS/EFFECT_PROJECTILES/Obj_projectile_fireball.tscn");
                                
                                if (_arrow != null)
                                {
                                    _arrow._base_location = this.GlobalPosition;
                                    _arrow._target_location = GLOBAL_STATS._player.GlobalPosition;
                                    _arrow._parent = this;
                                }
                            }

                            _fire_count = 0;
                    
                        }
                    break;
                    case 4:
                        //_warp_max = 
                        if (_warp_count == 20)
                        {
                            {
                                Final_Boss_Target _node = this.GetParent<Enemy_Final_Boss_core>()._target[GLOBAL_FUNCTIONS.Choose(1,2,3,4,5,6,7,8)];

                                Node _temp = GLOBAL_FUNCTIONS.Spawn_enemy(_node.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Egg.tscn");
                                _temp.GetChild<Obj_enemy_base>(0)._drops = new string[]{"hp"};
                            }
                        }

                        //GD.Print("hidden for a sec");
                    break;
                }
            }
            
        }
        else
        {
            _hspd = 0;
            _vspd = 0;
            if (_between_turns || _random_move == 4)
            {
                _prior_move =_random_move;
                while (_prior_move == _random_move)
                    _random_move = GLOBAL_FUNCTIONS.Choose(0,1,2,3);

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
                        GLOBAL_FUNCTIONS.Play_Sound(_attack_sound);
                        _jump_spd = 10;
                        _Animator.Play("jump");  
                        _state = 5;
                    break;
                }
            
            }

            //Speed = GLOBAL_FUNCTIONS.Choose(6.5f, 4.5f);
            _warp_count = 0;
        }

        return velocity;
    }

    public override void hit_me(Node3D _hit_by, float _hit_force = 0, float _jump_force = 0, int _damage = 0)
    {
        base.hit_me(_hit_by, 0, 0, _damage);
        this._state = HIT_STATE;
    }



    public override Vector3 hit_state(double delta, Vector3 velocity)
	{
		if (!_death_flag)
		{
            _hspd = 0;
            _vspd = 0;
            GLOBAL_FUNCTIONS.Play_Sound(_chirp_sound);
            GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);

            Final_Boss_Target _targ = this.GetParent<Enemy_Final_Boss_core>()._target[_warp_index];
            Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(_targ, "Effect_Explosion.tscn", true);
            this.GlobalPosition = _targ.GlobalPosition;
            _state = MOVE_STATE;
            

            /*
			if (hit_timer <  delay_timer)
			{
				hit_timer++;
			}
			else
			{
				hit_timer = 0;
				_state = IDLE_STATE;
			}*/
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
                //GD.Print(_jump_count);
            }

        
			counter = 0;
			//next_jump_in = GLOBAL_FUNCTIONS.Choose(200,400,500);
			_state = 4;
		}

		return velocity;
	}

}
