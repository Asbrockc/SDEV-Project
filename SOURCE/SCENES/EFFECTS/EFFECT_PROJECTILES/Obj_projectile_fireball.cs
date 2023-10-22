using Godot;

public partial class Obj_projectile_fireball : Obj_projectile_parent
{
	public Vector3 _base_location;
	public Vector3 _target_location;

	public float _div = 25.0f;

	public Obj_enemy_base _parent;

	public override void _active_function()
	{
		this.GlobalPosition -= (_base_location - _target_location)/_div;
	}

	public override void _on_hit(Node3D _node)
	{
		if (_active)
		{
			if (_node.IsInGroup("Player") || _node.IsInGroup("Wall"))
			{
				_hspd = 0;
				_vspd = 0;
				Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);
				_test.GetNode<Obj_enemy_hurt_zone>("Explosion_area")._enemy_parent = _parent;
				GD.Print(_test.GetNode<Obj_enemy_hurt_zone>("Explosion_area")._enemy_parent);
				_destroy = true;
			}
		}
	}
}
