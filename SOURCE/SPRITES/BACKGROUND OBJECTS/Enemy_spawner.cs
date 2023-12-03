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
	
	[Export] private bool _active = true;
	[Export] private SUMMON_INDEX _spawn_ref = SUMMON_INDEX.SPIDER;
	public bool _trigger = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_trigger)
		{
			if (_active)
				_spawn_function();

			_trigger = false;
		}
	}

	public virtual void _spawn_function()
	{
		switch (_spawn_ref)
		{
			case SUMMON_INDEX.SPIDER:
				GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Spider.tscn");
			break;
			case SUMMON_INDEX.EGG:
				GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Egg.tscn");
			break;
			case SUMMON_INDEX.KNIGHT_RED:
				GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Knight.tscn");
			break;
			case SUMMON_INDEX.KNIGHT_BLUE:
				GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Knight.tscn");
			break;
			case SUMMON_INDEX.BAT:
				GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Bat.tscn");
			break;
			case SUMMON_INDEX.GOLEM:
				GLOBAL_FUNCTIONS.Spawn_enemy(this.GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Golem.tscn");
			break;
		}
	}

    public override string ToString()
    {
        return "This is a spawner";
    }
}
