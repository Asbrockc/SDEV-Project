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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		switch (_state)
		{
			case 0:
			open_state();
			break;
			case 1:
			move_room();
			break;
			case 2:
			close_state();
			break;
		}

		if (_finished)
			this.Free();
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
		GLOBAL_FUNCTIONS.Change_Scene(_room);

		Door_base _target_Door = null;

		if (GLOBAL_STATS._current_room_reference != null)
		{
			foreach (Node node in GLOBAL_STATS._current_room_reference.GetChildren())
			{
				if (node.IsInGroup(_target_zone))
				{
					_target_Door = (Door_base)node;
					break;
				}
			}

			if (_target_Door != null)
				GLOBAL_STATS._player.Position = new Vector3(
					_target_Door.GlobalPosition.X + _x_off,
					_target_Door.GlobalPosition.Y,
					_target_Door.GlobalPosition.Z + _y_off);

			_state = 2;
		}
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

		GC.Collect();
		GC.WaitForPendingFinalizers();
	}
}
