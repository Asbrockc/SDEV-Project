using Godot;
using System;

public partial class Npc_base_code : Interactive_Action
{
	[Export] private int _core = 1; 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		string _npc = "_one";
		switch (_core)
		{
			case 0: _npc = "_one"; break;
			case 1: _npc = "_two"; break;
			case 2: _npc = "_three"; break;
			case 3: _npc = "_four"; break;
		}

		this.GetNode("spr_npc").GetNode<AnimationPlayer>("AnimationPlayer").Play("npc" + _npc);
	}
}
