using Godot;
using System;

public partial class Dragon_boss_head : Obj_enemy_base
{
	private Dragon_boss_core_AI _core;
	private Dragon_boss_base _body;

	private float _rise_up = 0;
	private int counter = 0;
	private int _next_attack = 0;

	[Export] public Vector3 _head_offset = new Vector3(0,0,0);

	private Vector3 _head_shift = new Vector3(0,0,0);

	public override void _Ready()
	{
		_next_attack = GLOBAL_FUNCTIONS.Choose(50,150,300,33,400,500);
		_core = GetParent<Dragon_boss_core_AI>();
		_body = _core.GetChild<Dragon_boss_base>(0);
		_state = MOVE_STATE;
		_apply_grav = false;
		_damage_sound = GLOBAL_STATS._player._sword_hit;
	}

    public override Vector3 move_to_player_state(double delta, Vector3 velocity)
    {
		_head_shift.X = (float)Mathf.Lerp(velocity.X, 0, .1);
		_head_shift.Y = (float)Mathf.Lerp(velocity.Y, 0, .1);
		_head_shift.Z = (float)Mathf.Lerp(velocity.Z, 0, .1);
		velocity = _head_shift;
	
		_head_shift.X = (float)Mathf.Lerp(this.Position.X, _body.Position.X + _head_offset.X - Math.Sin(GlobalPosition.X)/10, .05);
		_head_shift.Y = (float)Mathf.Lerp(this.Position.Y, _body.Position.Y + _head_offset.Y + _rise_up, .05);
		_head_shift.Z = (float)Mathf.Lerp(this.Position.Z, _body.Position.Z + _head_offset.Z, .05);
		this.Position = _head_shift;

		if (counter < _next_attack)
		{
			if (_rise_up > 0)
				_rise_up -= 0.1f;
			else
				_rise_up = 0;

			if (_body._state == 1)
				counter++;
		}
		else
		{
			if (_rise_up < 2)
				_rise_up += 0.05f;
			else
			{
				_next_attack = GLOBAL_FUNCTIONS.Choose(300,400,500);
				counter = 0;
			}
		}
        return velocity;
    }

}
