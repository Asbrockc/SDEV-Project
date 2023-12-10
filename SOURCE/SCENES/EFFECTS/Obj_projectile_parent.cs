using Godot;

///<summary>
/// Projectitle parent 
/// ment for objects that fly in various direction and interact with the wall
/// player or enemies. 
///</summary>
public partial class Obj_projectile_parent : Area3D
{
	public float _hspd = 0;
	public float _vspd = 0;

	private int _des_count = 0;

	public Node3D _stuck_in = null;
	public bool _active = true;
	public bool _destroy = false;

	public Sprite3D _my_sprite = null;
	public Node3D _my_parent = null;

	public override void _Ready()
	{
		_my_sprite = this.GetChild<Sprite3D>(0);
	}

	public override void _Process(double delta)
	{
		if (_destroy)
		{
			this.GetParent<Node3D>().RemoveChild(this);
			this.QueueFree();
		}
		else
		{
			_active_function();
		}
	}

	///<summary>
	/// Core logic for the arrow
	/// this will be overriten in other projectiles to act in the way
	/// that specific projectile needs
	/// the defualt arrow gets the ID of the object it hits and sticks into it
	///</summary>
	public virtual void _active_function()
	{
		if (_stuck_in == null)
		{
			Sprite3D _sprite = this.GetChild<Sprite3D>(0);
			this.Position += new Vector3(_hspd, 0, _vspd);
		}
		else if (_active)
		{
			this.GetParent<Node3D>().RemoveChild(this);


			if (!((Obj_enemy_base)_stuck_in)._immune_to_bow)
			{
				((Obj_enemy_base)_stuck_in).hit_me(this._my_parent, 1.5f, 2.0f, GLOBAL_STATS._player_stats[GLOBAL_STATS.I_DEFENCE]);
				_stuck_in.AddChild(this);
				this.Position = new Vector3(0.0f, 0.0f, 0.0f);
				this._my_sprite.Texture = GLOBAL_FUNCTIONS._broken_arrow;	
			}
			else
			{
				((Obj_enemy_base)_stuck_in).hit_me(null);	
				if (this.GetParentOrNull<Node3D>() != null)
				{
					this.GetParent().RemoveChild(this);
					this.QueueFree();
				}
			}
			
			
			_active = false;
		}
		else
		{
			if (_des_count < 200)
				_des_count++;
			else
			{
				//GetParentOrNull
				if (this.GetParentOrNull<Node3D>() != null)
				{
					this.GetParent().RemoveChild(this);
					this.QueueFree();
				}
			}
			//this.GetChild<Sprite3D>(1);
		}
	}

	///<summary>
	/// manages what the projectile will do when it hits an object 	
	/// ment to be overritten for other porjectiles
	/// defualts to hitting enemy hitzones and handle itself like an arrow
	///</summary>
	public virtual void _on_hit(Node3D _node)
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
