using Godot;
using System;

public partial class Enemy_Final_Boss_phase_one_core : Boss_core_AI
{
	private Final_boss_first_phase _base;

	public override void _Ready()
	{
		_name = "Shadow";
		_boss_hp = 1000;
		_boss_max_hp = 1000;

		_base = this.GetNode<Final_boss_first_phase>("Obj_enemy_base");
	}

    public override void _main_function()
    {
		_boss_hp = _base._health;
		_boss_max_hp = _base._max_health;
    }
}
