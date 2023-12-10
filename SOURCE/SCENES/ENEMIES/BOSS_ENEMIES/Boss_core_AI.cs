using Godot;
using System;

///<summary>
/// parent boss core AI
/// this will set up the general boss envirnemnt and handle the bosses status
///</summary>
public partial class Boss_core_AI : Node3D
{	
	public bool _intro = true;
	public bool _defeated = false;
	public string _name = "Boss";
	public int _boss_hp = 0;
	public int _boss_max_hp = 0;

    public override void _Process(double delta)
    {
		if (!_defeated)
			_main_function();
	
    }

	public virtual void _main_function()
	{
		//core boss mechanics go here
	}

	
}
