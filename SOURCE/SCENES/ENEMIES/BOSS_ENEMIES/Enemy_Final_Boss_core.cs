using Godot;
using System;

///<summary>
/// final boss phase 2 core AI
/// this tracks down sever boss warp points for the boss to reference 
/// it also tracks his glowing eyes. 
///</summary>
public partial class Enemy_Final_Boss_core : Boss_core_AI
{
	private Enemy_Final_boss_base _base; 
	private OmniLight3D _left;
	private OmniLight3D _right;
	private float light_counter = 0;

	public Final_Boss_Target[] _target = new Final_Boss_Target[9];

	public override void _Ready()
	{
		 _name = "Shadow";
		_boss_hp = 100;
		_boss_max_hp = 1000;
		

		_base = this.GetNode<Enemy_Final_boss_base>("Obj_enemy_base");

		_right = _base.GetNode<OmniLight3D>("right_light");
		_left = _base.GetNode<OmniLight3D>("left_light");

		//GD.Print("why is this not showing up?");
		int i = 0;
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Off_Target");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_1");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_2");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_3");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_4");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_5");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_6");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_7");
		_target[i++] = this.GetParent().GetNode<Final_Boss_Target>("Boss_Target_8");

		//GD.Print("after the list?");
		//GD.Print(i);

		
	}

	///<summary>
	/// Shifts the lights in the bosses eyes eerily using a sin wave
	///</summary>
	public override void _Process(double delta)
	{
		//GD.Print(_left.Position.ToString() + " - " + _right.Position.ToString());

		if (!_defeated)
		{

			_boss_hp = _base._health;
			_boss_max_hp = _base._max_health;
			
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
