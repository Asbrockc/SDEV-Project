using Godot;
using System;

public partial class Enemy_spawner : Node3D
{
	public enum SUMMON_INDEX : ushort
	{
		SPIDER = 0,
		EGG = 1,
		KNIGHT_RED = 2,
		KNIGHT_BLUE= 3, 
		BAT = 4,
		GOLEM = 5
	}
	
	[Export] public double _time = -4;

	[Export] private bool _active = true;
	[Export] private SUMMON_INDEX _spawn_ref = SUMMON_INDEX.SPIDER;
	public bool _trigger = false;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_trigger)
		{
			if (_active)
			{
				Effect_parent _test = GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);
				_spawn_function();
			}

			_trigger = false;
		}
	}

	public virtual void _spawn_function()
	{
		Node _enem = null;

 		GLOBAL_FUNCTIONS.Create_Effect(this, "Effect_Explosion.tscn", true);
		switch (_spawn_ref)
		{
			case SUMMON_INDEX.SPIDER:
				_enem = GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Spider.tscn");
			break;
			case SUMMON_INDEX.EGG:
				_enem = GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Egg.tscn");
			break;
			case SUMMON_INDEX.KNIGHT_RED:
				_enem = GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Knight.tscn");
				_enem.GetChild<Enemy_Knight_base>(0).curr_color = Enemy_Knight_base.COLOR.RED;
				_enem.GetChild<Enemy_Knight_base>(0)._knight_set_up = false;
				//_temp.
			break;
			case SUMMON_INDEX.KNIGHT_BLUE:
				_enem = GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Knight.tscn");
				_enem.GetChild<Enemy_Knight_base>(0).curr_color = Enemy_Knight_base.COLOR.BLUE;
				_enem.GetChild<Enemy_Knight_base>(0)._knight_set_up = false;
			break;
			case SUMMON_INDEX.BAT:
				_enem = GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Bat.tscn");
			break;
			case SUMMON_INDEX.GOLEM:
				_enem = GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Golem.tscn");
			break;
		}

		if (_enem != null)
			_enem.GetChild<Obj_enemy_base>(0)._target = GLOBAL_STATS._player;
	}

    public override string ToString()
    {
        return "This is a spawner";
    }
}
