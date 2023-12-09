using Godot;

public partial class Test_path : Path3D
{
	public override void _Process(double delta)
	{
		Dragon_boss_core_AI _body = this.GetParent<Dragon_boss_core_AI>();
		Curve.SetPointPosition(0, _body.GetNode<Node3D>("Obj_enemy_base").Position);
		Curve.SetPointPosition(1, _body.GetNode<Node3D>("Obj_enemy_base_head").Position);
	}
}
