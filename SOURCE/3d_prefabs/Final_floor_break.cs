using Godot;
using System;

public partial class Final_floor_break : Node3D
{
	public bool _broken = false;
	private Sprite3D[] _cracks;

	private AudioStreamMP3 _crack_sound;
	
	private int current_crack = 0;

	private int current_countdown = 0;
	private int current_max = 200;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		current_countdown = current_max;

		_crack_sound = ResourceLoader.Load<AudioStreamMP3>("res://SOUNDS/ALL_SOUNDS/snd_crack.mp3");
		
		_cracks = new Sprite3D[8];

		int i = 0;
		_cracks[i++] = GetNode<Sprite3D>("crack_" + i.ToString());
		_cracks[i++] = GetNode<Sprite3D>("crack_" + i.ToString());
		_cracks[i++] = GetNode<Sprite3D>("crack_" + i.ToString());
		_cracks[i++] = GetNode<Sprite3D>("crack_" + i.ToString());

		_cracks[i++] = GetNode<Sprite3D>("crack_" + i.ToString());
		_cracks[i++] = GetNode<Sprite3D>("crack_" + i.ToString());
		_cracks[i++] = GetNode<Sprite3D>("crack_" + i.ToString());
		_cracks[i++] = GetNode<Sprite3D>("crack_" + i.ToString());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_broken)
		{
			if (current_crack < _cracks.Length)
			{
				if (current_countdown < current_max)
				{
					current_countdown++;
				}
				else
				{
					GLOBAL_FUNCTIONS.Play_Sound(_crack_sound);
					GLOBAL_FUNCTIONS.Shake_Camera(1);
					_cracks[current_crack].Visible = true;
					current_crack++;
					current_max = current_max - 30;
					current_countdown = 0;
				}
			}
			else
			{
				GLOBAL_FUNCTIONS.Change_Music("res://SOUNDS/ALL_SOUNDS/MUSIC/snd_final_boss_part_1.wav", 100);
				QueueFree();
			}
		}
	}
}
