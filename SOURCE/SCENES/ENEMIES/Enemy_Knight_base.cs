using Godot;

public partial class Enemy_Knight_base : Enemy_Egg_AI
{
	public enum COLOR
	{
		RED, 
		BLUE
	}

	[Export] public COLOR curr_color = COLOR.RED;
	
	public bool _knight_set_up = false;


    public override void _Ready()
    {
        base._Ready();
		_damage_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_hit_metal.wav");
    }

    public override void _PhysicsProcess(double delta)
    {
		if (!_knight_set_up)
		{
			switch (curr_color)
			{
				case COLOR.RED:
					_health = 20;
					_max_health = 20;
					this.GetNode<Sprite3D>("Spr_enemy").Texture = ResourceLoader.Load<Texture2D>("res://SPRITES/ENEMY_SPRITES/spr_red_knight.png");
				break;
				case COLOR.BLUE:
					_health = 40;
					_max_health = 40;
				break;
			}

			_knight_set_up = true;
		}
		
        base._PhysicsProcess(delta);
    }

	public override Vector3 enemy_core_AI(double delta, Vector3 velocity)
	{
		if (!IsOnFloor() && _apply_grav)
			velocity.Y -= gravity * (float)delta;

		if (_jump_spd != 0)
		{
			velocity.Y = _jump_spd;
			_jump_spd = 0.0f;
		}

		switch (_state)
		{
			case IDLE_STATE: 
				velocity = idle_state(delta, velocity);
			break;
			case MOVE_STATE: 
				if (curr_color == COLOR.RED)
					velocity = move_to_player_state(delta, velocity);
				else
					velocity = move_left_right_to_player_state(delta, velocity);
			break;
			case HIT_STATE: 
				velocity = hit_state(delta, velocity);
			break;
			case DEATH_STATE: 
				velocity = death_state(delta, velocity);
			break;

			default:
				_hspd = 0;
				_vspd = 0;
				break;
		}

		_base = GLOBAL_FUNCTIONS.Entity_Dir(_base, _hspd, _vspd);

		return velocity;
	}

    public override void hit_me(Node3D _hit_by, float _hit_force = 0, float _jump_force = 0, int _damage = 0)
    {
			switch (curr_color)
			{
				case COLOR.RED:
					base.hit_me(_hit_by, _hit_force, _jump_force, _damage);
				break;
				case COLOR.BLUE:
					base.hit_me(_hit_by, 0, 0, _damage);
				break;
			}
        
    }
}
