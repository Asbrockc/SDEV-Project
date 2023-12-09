using Godot;
using System;

public partial class After_Enemy_Button : Button_base
{
	private bool _started = false;
	private int _amount_defeated = 0;
	private int _amount_spawned = 0;
	private int _amount_to_defeat = 40;
	private Label3D _label = null;

	private int _counter = 0; 
	private int _next = 120; 

	private Enemy_spawner[]	_spawn_points;
	private Color _green = new Color(0,1,0);
	private Color _red = new Color(1,0,0);

	private AudioStreamWav _bad_noise;
	private StandardMaterial3D _test;

	public override void _Ready()
	{
		_test = GetChild<Standing_platform>(0).Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
		_bad_noise = ResourceLoader.Load<AudioStreamWav>("res://SOUNDS/ALL_SOUNDS/snd_bad_button.wav");

		int i = 0;
		foreach (Node3D _node in this.GetParent().GetChildren())
		{
			if (_node.IsInGroup("enemy_spawn"))
				i++;
		}

		_spawn_points = new Enemy_spawner[i];
		
		i = 0;
		foreach (Node3D _node in this.GetParent().GetChildren())
		{
			if (_node.IsInGroup("enemy_spawn"))
				_spawn_points[i++] = (Enemy_spawner)_node;
		}

	
		_label = this.GetNode<Label3D>("Label3D");
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_started)
		{
			if (_amount_defeated >= _amount_to_defeat)
			{
				if (_test!.AlbedoColor != _green)
				{
					GLOBAL_FUNCTIONS.SetFlag(GLOBAL_STATS.FLAG_INDEX.locked_behind_player_puzzle, true);
					GLOBAL_FUNCTIONS.Change_Music("res://SOUNDS/ALL_SOUNDS/MUSIC/snd_forest_theme.wav", 10);
					this.GetParent().GetNode<SpotLight3D>("SpotLight3D").LightColor = new Color(1,1,1);
					_test!.AlbedoColor = _green;
				}

				base._Process(delta);
			}
			else
			{
				if (_test!.AlbedoColor != _red)
				{
					GLOBAL_FUNCTIONS.SetFlag(GLOBAL_STATS.FLAG_INDEX.locked_behind_player_puzzle, false);
					this.GetParent().GetNode<SpotLight3D>("SpotLight3D").LightColor = new Color(1,.8f,.8f);
					GLOBAL_FUNCTIONS.Play_Sound(_bad_noise);

					GLOBAL_FUNCTIONS.Change_Music("res://SOUNDS/ALL_SOUNDS/MUSIC/snd_boss_one_part_1.wav", 100);
					_test!.AlbedoColor = _red;
				}

				if (_counter >= _next)//) && _amount_spawned < 50)
				{   
					_counter = 0;
					Node _enemy = null;

					if (_amount_spawned != 0 && _amount_spawned % 40 == 0)
					{
						_enemy = GLOBAL_FUNCTIONS.Spawn_enemy(_spawn_points[GLOBAL_FUNCTIONS.Choose(0,1,2,3,4)].GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Golem.tscn");
					}
					if (_amount_spawned != 0 && _amount_spawned % 20 == 0)
					{
						_enemy = GLOBAL_FUNCTIONS.Spawn_enemy(_spawn_points[GLOBAL_FUNCTIONS.Choose(0,1,2,3,4)].GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Knight.tscn");
					}
					else if (_amount_spawned % 9 == 0)
					{
						_enemy = GLOBAL_FUNCTIONS.Spawn_enemy(_spawn_points[5].GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Bat.tscn");
					}
					else if (_amount_spawned % 7 == 0)
					{
						_enemy = GLOBAL_FUNCTIONS.Spawn_enemy(_spawn_points[GLOBAL_FUNCTIONS.Choose(0,1,2,3,4)].GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Spider.tscn");
					}
					else if (_amount_spawned % 5 == 0)
					{
						_enemy = GLOBAL_FUNCTIONS.Spawn_enemy(_spawn_points[GLOBAL_FUNCTIONS.Choose(0,1,2,3,4)].GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_wizard.tscn");
					}
					else
					{
						_enemy = GLOBAL_FUNCTIONS.Spawn_enemy(_spawn_points[GLOBAL_FUNCTIONS.Choose(0,1,2,3,4)].GlobalPosition, "res://SCENES/ENEMIES/BASE_ENEMIES/Enemy_Egg.tscn");
					}

					if (_enemy != null)
					{
						_enemy.GetChild<Obj_enemy_base>(0)._target = GLOBAL_STATS._player;
						_enemy.GetChild<Obj_enemy_base>(0)._on_death = () => _amount_defeated++;
					}

					GLOBAL_FUNCTIONS.Create_Effect((Node3D)_enemy, "Effect_Explosion.tscn", false);
					
				
					_amount_spawned++;
				}
				else
					_counter++;
			}
				
			//_label.Text = _amount_defeated.ToString() + " / " + "_amount_to_defeat".ToString();
		}
		else
		{
			if (_button._Player_Zone._player != null && !_started)
			{
				_started = true;
			}
		}
	}
}
