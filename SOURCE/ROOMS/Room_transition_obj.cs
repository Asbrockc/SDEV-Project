using Godot;
using System;

public partial class Room_transition_obj : NinePatchRect
{
	private int _state = 0;
	private float _scale = 0.0f;
	private bool _finished = false;

	public String _room = null;
	public String _target_zone = "null";
	public float _x_off = 0;
	public float _y_off = 0;

	public int _hold_off_a_second = 0;

	private int _count = 0;

    public override void _Ready()
    {
        base._Ready();
		//GD.Print("CREATED");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		switch (_state)
		{
			case 0:
				//GD.Print("step 0");
				open_state();
			break;
			case 1:
				//GD.Print("step 1");
				transition();
			break;
			case 2:
				//GD.Print("step 2");
				move_room();
			break;
			case 3:
				//GD.Print("step 3");
				close_state();
			break;
			case 4:
				//GD.Print("step 4");
				hold_state();
			break;

		}

		if (_finished)
		{
			this.GetParent().RemoveChild(this);
			GLOBAL_FUNCTIONS._transition = null;
			this.QueueFree();
		}
	}
	private void transition()
	{
		GD.Print(_room + " 0 " + _target_zone);
		GLOBAL_FUNCTIONS.Change_Scene(_room);
		_state = 2; //4
	}

	private void hold_state()
	{
		if (_count < 30)
		{
			_count++;
			//GD.Print(_count);
		}
		else
			_state = 2;
	}

	private void open_state()
	{
		GLOBAL_STATS._current_room_reference = null;

		if (_scale + 115 < 1152)
			_scale += 115;
		else
		{
			_scale = 1152;
			_state = 1;
		}

		this.SetSize(new Vector2(_scale, this.Size.Y));
		//1152
	}

	private void move_room()
	{

		if (_hold_off_a_second > 30)
		{
			GLOBAL_STATS._Camera._target = GLOBAL_STATS._player;
			Node3D _target_Door = null;

			if (GLOBAL_STATS._current_room_reference != null)
			{
				foreach (Node node in GLOBAL_STATS._current_room_reference.GetChildren())
				{
					
					if (node.IsInGroup(_target_zone))
					{
						_target_Door = (Node3D)node;
						break;
					}
				}

				if (_target_Door != null)
				{
					GLOBAL_STATS._player.Position = new Vector3(
						_target_Door.GlobalPosition.X + _x_off,
						_target_Door.GlobalPosition.Y,
						_target_Door.GlobalPosition.Z + _y_off);

					if(GLOBAL_STATS._Camera._target != null)
						GLOBAL_STATS._Camera.Position = new Vector3(
							GLOBAL_STATS._Camera._target.Position.X , 
							GLOBAL_STATS._Camera._target.Position.Y + GLOBAL_STATS._Camera._y_dis, 
							GLOBAL_STATS._Camera._target.Position.Z + GLOBAL_STATS._Camera._z_dis);
				}
				else
				{
					GLOBAL_STATS._player.Position = new Vector3(
						0,
						0,
						0);

					if(GLOBAL_STATS._Camera._target != null)
						GLOBAL_STATS._Camera.Position = new Vector3(
							0, 
							0 + GLOBAL_STATS._Camera._y_dis, 
							0 + GLOBAL_STATS._Camera._z_dis);
				}

				_state = 3;

				if (GLOBAL_STATS._current_room_reference._room_music != "null")
				{
					if (GLOBAL_STATS._current_room_reference._room_music != GLOBAL_FUNCTIONS._audio_emitter._next_song)
						GLOBAL_FUNCTIONS.Change_Music(GLOBAL_STATS._current_room_reference._room_music);
				}
			}
		}
		else
			_hold_off_a_second++;
	}

	private void close_state()
	{
		if (_scale - 115 > 0)
		{
			_scale -= 115;
		}
		else
		{
			_scale = 0;
			_finished = true;

			GLOBAL_STATS._player._state = 0;
		}
		
		this.Position += (new Vector2(115.0f, 0));
		this.SetSize(new Vector2(_scale, this.Size.Y));

		//GC.Collect();
		//GC.WaitForPendingFinalizers();
	}
}
