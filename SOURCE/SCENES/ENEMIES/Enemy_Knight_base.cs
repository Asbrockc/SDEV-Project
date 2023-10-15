using Godot;
using System;

public partial class Enemy_Knight_base : Enemy_Egg_AI
{
	public enum COLOR
	{
		RED, 
		BLUE
	}

	[Export] private COLOR curr_color = COLOR.RED;
	
	private bool _knight_set_up = false;


    public override void _Ready()
    {
        base._Ready();
		_damage_sound = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_hit_metal.wav");
    }

    public override void _PhysicsProcess(double delta)
    {
		if (!_knight_set_up)
		{
			if (curr_color == COLOR.RED)
				this.GetNode<Sprite3D>("Spr_enemy").Texture = ResourceLoader.Load<Texture2D>("res://SPRITES/ENEMY_SPRITES/spr_red_knight.png");

			_knight_set_up = true;
		}
		
        base._PhysicsProcess(delta);
    }
}
