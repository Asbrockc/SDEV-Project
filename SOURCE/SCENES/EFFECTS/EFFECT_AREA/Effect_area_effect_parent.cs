using Godot;

///<summary>
/// 
///</summary>
public partial class Effect_area_effect_parent : Obj_projectile_parent
{
	//public bool _driver = 0;

	private bool _set_parent = false;
	private int _counter = 0;
	private int _lifespan = 60;
	private Vector3 _y_driver = new Vector3(0,5,0);
	private Vector3 _x_driver = new Vector3(0.2f,0,0);
	private Vector3 _z_driver = new Vector3(0,0,0.2f);


	public override void _Ready()
	{
		//just needs to stop the base script from trying to read a sprite that doesnt exist
	}

	///<summary>
	/// Takes the parts of the projectile and expands them outwards
	/// can be called with anohter set at a 45 degree angle to create a circle
	///</summary>
    public override void _Process(double delta)
	{
		this.RotationDegrees += _y_driver;

		if (!_set_parent && _my_parent != null)
		{
			this.GetChild<Node3D>(0).GetChild<Obj_enemy_hurt_zone>(0)._enemy_parent = (Obj_enemy_base)_my_parent;
			this.GetChild<Node3D>(1).GetChild<Obj_enemy_hurt_zone>(0)._enemy_parent = (Obj_enemy_base)_my_parent;
			this.GetChild<Node3D>(2).GetChild<Obj_enemy_hurt_zone>(0)._enemy_parent = (Obj_enemy_base)_my_parent;
			this.GetChild<Node3D>(3).GetChild<Obj_enemy_hurt_zone>(0)._enemy_parent = (Obj_enemy_base)_my_parent;
		}

		if (_counter < _lifespan)
		{
			if (true)//Input.IsKeyPressed(Key.P))
			{
				this.GetChild<Node3D>(0).Position += _z_driver;
				this.GetChild<Node3D>(0).Scale += _x_driver*.8f;

				this.GetChild<Node3D>(1).Position -= _z_driver;
				this.GetChild<Node3D>(1).Scale += _x_driver*.8f;

				this.GetChild<Node3D>(2).Position += _x_driver;
				this.GetChild<Node3D>(2).Scale += _z_driver*.8f;

				this.GetChild<Node3D>(3).Position -= _x_driver;
				this.GetChild<Node3D>(3).Scale += _z_driver*.8f;
			}

			_counter++;
		}
		else
		{
			this.GetParent().RemoveChild(this);
			this.QueueFree();
		}
	}
}
