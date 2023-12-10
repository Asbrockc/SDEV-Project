using Godot;
using System;

///<summary>
/// NPC base code, inherits the interactive_action class
/// and the chatbox it creates
///</summary>
public partial class Npc_base_code : Interactive_Action
{
	[Export] private int _core = 1; 

	///<summary>
	/// decides the sprite it will use based on the core number
	///</summary>
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
