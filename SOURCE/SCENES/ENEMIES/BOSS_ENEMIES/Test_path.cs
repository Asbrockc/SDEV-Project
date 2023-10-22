using Godot;

public partial class Test_path : Path3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Dragon_boss_core_AI _body = this.GetParent<Dragon_boss_core_AI>();
		Curve.SetPointPosition(0, _body.GetNode<Node3D>("Obj_enemy_base").Position);
		Curve.SetPointPosition(1, _body.GetNode<Node3D>("Obj_enemy_base_head").Position);
	}
}
