using Godot;
using System;

///<summary>
/// Programming for the colored button mini game
/// ALl the buttons are red and the player needs to change them to blue
/// however, once a button is pressed it also changes the ones next to, below, and above it.
///</summary>
public partial class Button_game_control : Node3D
{
	private int BUTTON_COUNT = 16;
	private Game_button_base[,] _button_grid = new Game_button_base[4,4];

	private Color _red = new Color(1,0,0);
	private Color _blue = new Color(0,0,1);

	public int pressed_x = 0;
	public int pressed_y = 0;
	public bool pressed_active = false;

	///<summary>
	/// Grabs all of the buttons that are children of this class and makes a quick 4x4 array out of them
	///</summary>
	public override void _Ready()
	{
		_button_grid =  new Game_button_base[,]
		{ 
			{ this.GetChild<Game_button_base>(0), this.GetChild<Game_button_base>(1),this.GetChild<Game_button_base>(2),this.GetChild<Game_button_base>(3) }, 
			{ this.GetChild<Game_button_base>(4), this.GetChild<Game_button_base>(5),this.GetChild<Game_button_base>(6),this.GetChild<Game_button_base>(7) }, 
			{ this.GetChild<Game_button_base>(8), this.GetChild<Game_button_base>(9),this.GetChild<Game_button_base>(10),this.GetChild<Game_button_base>(11) }, 
			{ this.GetChild<Game_button_base>(12), this.GetChild<Game_button_base>(13),this.GetChild<Game_button_base>(14),this.GetChild<Game_button_base>(15) } 
		};

		//change them all to red
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
			{
				_button_grid[i,j].my_x = i;
				_button_grid[i,j].my_y = j;
				var _test = _button_grid[i,j].GetChild<Standing_platform>(0).Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
				_test!.AlbedoColor = _red; 	
			}
		
		for (int i = 0; i < 10; i++)
		{
			//shift_button(GLOBAL_FUNCTIONS.Choose(0,1,2,3), GLOBAL_FUNCTIONS.Choose(0,1,2,3));
		}
			
	}

	///<summary>
	/// shift the buttons surroning the passed in button to the opposite color
	///</summary>
	private void shift_button(int xx, int yy)
	{
		if (xx >= 0 && xx < 4 && yy >= 0 && yy < 4)
		{
			var _test = _button_grid[xx,yy].GetChild<Standing_platform>(0).Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
				//GD.Print(_test!);
			if (_test!.AlbedoColor == _red)
				_test!.AlbedoColor = _blue;
			else
				_test!.AlbedoColor = _red;
		}
	}

	///<summary>
	/// if a button is pressed it will alert this class and this will adjust the surounding buttons while
	/// also checking if all the buttons are blue
	///</summary>
	public override void _Process(double delta)
	{
		if (pressed_active)
		{
			shift_button(pressed_x, pressed_y);
			shift_button(pressed_x+1, pressed_y);
			shift_button(pressed_x-1, pressed_y);
			shift_button(pressed_x, pressed_y+1);
			shift_button(pressed_x, pressed_y-1);

			int check = 0;

			
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
			{
				//StandardMaterial3D _temp = new StandardMaterial3D();
				//_button_grid[i,j].GetChild<Standing_platform>(0).Mesh.SurfaceSetMaterial(0, _temp);

				_button_grid[i,j].my_x = i;
				_button_grid[i,j].my_y = j;
				var _test = _button_grid[i,j].GetChild<Standing_platform>(0).Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
				if (_test!.AlbedoColor == _blue) //GLOBAL_FUNCTIONS.Choose(_red, _blue);
					check++;
			}

			if (check == 16)
			{
				GLOBAL_STATS.FLAG_INDEX _flag = GLOBAL_STATS.FLAG_INDEX.Puzzle_door_1;
				GLOBAL_STATS._completion_flags[(int)_flag] = true;
				//GD.Print("WINNER");
			}

			//GD.Print(pressed_x);
			//GD.Print(pressed_y);
			pressed_active = false;
		}
		/*
		var _test = _button_grid[1,3].GetChild<Standing_platform>(0).Mesh.SurfaceGetMaterial(0) as StandardMaterial3D;
		//GD.Print(_test!);
		_test!.AlbedoColor = new Color(
			GLOBAL_FUNCTIONS.Choose(0,1),
			GLOBAL_FUNCTIONS.Choose(0,1),
			GLOBAL_FUNCTIONS.Choose(0,1));
		*/

		//var material = mesh.GetActiveMaterial(0) as StandardMaterial3D;
		
		
		//GD.Print(_button_grid[1,3].GetChild<Standing_platform>(0).Mesh.SurfaceGetMaterial(0));

		//_button_grid[1,3].GetChild<Standing_platform>(0).Mesh.SurfaceSetMaterial(0, _test);
		//Position += new Vector3(.001f, 0, 0);
	}

	
}
