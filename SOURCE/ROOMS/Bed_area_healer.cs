using Godot;
using System;

///<summary>
/// Code for the healing bed the player can rest in. 
/// just adjust the sprite to look like is over the player
/// then if they enter the area, it will heal them one health ever 50 steps
///</summary>
public partial class Bed_area_healer : Area3D
{
	private bool _healing = false;

	private int _timer = 0; 

	private Vector3 _sheets_up = new Vector3(0, -.25f, -.15f);
	private Vector3 _sheets_down = new Vector3(0, -.55f, -.55f);

	public override void _Process(double delta)
	{
		if (_healing)
		{
			if (_timer > 50)
			{
				GLOBAL_FUNCTIONS.Play_Sound(GLOBAL_STATS._player._item_pickup);
				if (GLOBAL_STATS._player_stats[GLOBAL_STATS.I_HEALTH] < GLOBAL_STATS._player_stats[GLOBAL_STATS.I_MAX_HEALTH])
					GLOBAL_STATS._player_stats[GLOBAL_STATS.I_HEALTH]++;
				_timer = 0;
			}
			else
				_timer++;

			GetParent().GetNode<Node3D>("Bed_Top").Position = _sheets_up;
		}
		else
		{
			_timer = 0;
			GetParent().GetNode<Node3D>("Bed_Top").Position = _sheets_down;
		}
				
	}

	public  void _enter_bed(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
			_healing = true;
	}

	public  void _leave_bed(Node3D _node)
	{
		if (_node.IsInGroup("Player"))
			_healing = false;
	}

}
