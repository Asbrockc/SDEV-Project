using Godot;
using System;

public partial class Enemy_Final_Boss_core : Boss_core_AI
{
	private Enemy_Final_boss_base _base; 
	private OmniLight3D _left;
	private OmniLight3D _right;
	private float light_counter = 0;

	public Final_Boss_Target[] _target = new Final_Boss_Target[5];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 _name = "Shadow";
		_boss_hp = 100;
		_boss_max_hp = 1000;

		_base = this.GetNode<Enemy_Final_boss_base>("Obj_enemy_base");

		_right = _base.GetNode<OmniLight3D>("right_light");
		_left = _base.GetNode<OmniLight3D>("left_light");

		int i = 0;
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Off_Target");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_1");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_2");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_3");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_4");

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!_defeated)
		{
			light_counter += 0.02f;

			if (light_counter >= 180)
				light_counter = 0;

			_left.LightEnergy = (float)Math.Sin(light_counter) * 5.0f; 
			_right.LightEnergy = (float)Math.Sin(light_counter + 90) * 5.0f; 
		}

		//if (Input.IsKeyPressed(Key.Space))
		{
			//GetNode<Dragon_boss_base>("Obj_enemy_base")._health = 0;
			//_defeated = true;
		}
	}
}
