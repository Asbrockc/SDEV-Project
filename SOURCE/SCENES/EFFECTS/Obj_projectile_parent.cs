using Godot;
using System;

public partial class Obj_projectile_parent : Area3D
{
	public float _hspd = 0;
	public float _vspd = 0;

	public Node3D _stuck_in = null;
	public bool _active = true;
	public bool _destroy = false;

	public Sprite3D _my_sprite = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_my_sprite = this.GetChild<Sprite3D>(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_destroy)
		{
			this.GetParent<Node3D>().RemoveChild(this);
			this.QueueFree();
		}
		else
		{
			if (_stuck_in == null)
			{
				Sprite3D _sprite = this.GetChild<Sprite3D>(0);

				this.Position += new Vector3(_hspd, 0, _vspd);
			}
			else if (_active)
			{
				this.GetParent<Node3D>().RemoveChild(this);
				_stuck_in.AddChild(this);
				((Obj_enemy_base)_stuck_in).hit_me(this, 1.5f, 2.0f, GLOBAL_STATS._player_stats[GLOBAL_STATS.I_DEFENCE]);
				this.Position = new Vector3(0.0f, 0.0f, 0.0f);
				this._my_sprite.Texture = GLOBAL_FUNCTIONS._broken_arrow;
				GLOBAL_FUNCTIONS.Play_Sound(GLOBAL_STATS._player._sword_hit);
				_active = false;
			}
			else
			{
				//this.GetChild<Sprite3D>(1);
			}
		}
	}

	public void _on_hit(Node3D _node)
	{
		if (_active)
		{
			if (_node.IsInGroup("Enemy_hit_zone"))
			{
				_hspd = 0;
				_vspd = 0;

				_stuck_in = _node;
			}
			else if (_node.IsInGroup("Wall"))
			{
				_destroy = true;
			}
		}
	}
}
