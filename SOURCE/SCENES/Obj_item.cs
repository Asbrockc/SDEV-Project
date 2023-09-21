using Godot;
using System;

public partial class Obj_item : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public float _bounce = 0;
	public float _hspd = -3.5f;
	public float _vspd = 6.5f;
	public float _charge_up = 0.0f;

	public bool _used = false;

	public int count = 0;


	public override void _PhysicsProcess(double delta)
	{
		count++;
		
		if (_used)
		{
			player_grab();
			GetParent().RemoveChild(this);
			this.Free();
		}
		else
		{
			Vector3 velocity = Velocity;

		


			Vector3 _test = GetWallNormal();

			if (_test.X != 0)
				_hspd = -_hspd;

			if (_test.Z != 0)
				_vspd = -_vspd;

			if (_charge_up != 0.0f)
			{
				velocity.Y = _charge_up;
				_charge_up = 0.0f;
			}
			
			if (!IsOnFloor())
			{
				velocity.Y -= gravity * (float)delta;
				_bounce = velocity.Y * 3/4;
			}
			else
			{
				if (Math.Abs(_bounce) > 1.0f)
					velocity.Y = -_bounce;
				else	
				{
					velocity.Y = 0.0f;
					_hspd = GLOBAL_FUNCTIONS.Gradual_Stop(_hspd, 10);
					_vspd = GLOBAL_FUNCTIONS.Gradual_Stop(_vspd, 10);
				}
			}

			//GD.Print(_bounce);

			velocity.X = _hspd;
			velocity.Z = _vspd;

			Velocity = velocity;
			MoveAndSlide();
		}
	}

	public virtual void player_grab()
	{
		GD.Print("Player Grabbed Item");
	}
}
