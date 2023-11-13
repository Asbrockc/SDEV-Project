using Godot;
using System;

public partial class Final_boss_first_phase : Boss_enemy_base
{
    private bool _death_set_up = false;

	public override Vector3 death_finale(double delta, Vector3 velocity)
	{
		
		if (!_death_set_up)
        {
            GD.Print("CLICKED");
            //velocity.Y = 10;
            _jump_spd = 10;
            _death_set_up = true;
        }

		counter++;
		if (IsOnFloor() && counter > 5)
		{
			counter = 0;
		
            GD.Print("CLICKED _boom");
            _death_flag = true;
            QueueFree();

			GLOBAL_STATS._current_room_reference.GetNode("L_floor_one").QueueFree();
		}

		return velocity;

	}
	
}
