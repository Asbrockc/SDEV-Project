using Godot;
using System;

public partial class Dragon_boss_core_AI : Node3D
{
	//private Path3D[] _neck  = new Path3D[3]; 
	private Vector3 _base_offset;

    public override void _Ready()
    {
		_base_offset = new Vector3(0.0f, -0.1f, 0.0f);
    }

    public override void _Process(double delta)
    {
		GetNode<Path3D>("Neck_1").Curve.SetPointPosition(0, GetNode<Node3D>("Obj_enemy_base").Position + _base_offset);
		GetNode<Path3D>("Neck_1").Curve.SetPointPosition(1, GetNode<Node3D>("Obj_enemy_base_head_1").Position);
		GetNode<Path3D>("Neck_2").Curve.SetPointPosition(0, GetNode<Node3D>("Obj_enemy_base").Position + _base_offset);
		GetNode<Path3D>("Neck_2").Curve.SetPointPosition(1, GetNode<Node3D>("Obj_enemy_base_head_2").Position);
		GetNode<Path3D>("Neck_3").Curve.SetPointPosition(0, GetNode<Node3D>("Obj_enemy_base").Position + _base_offset);
		GetNode<Path3D>("Neck_3").Curve.SetPointPosition(1, GetNode<Node3D>("Obj_enemy_base_head_3").Position);
    }
}
