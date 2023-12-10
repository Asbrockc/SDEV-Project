using Godot;
using System;

///<summary>
/// Transition revamp that just block the screen and wipes itself away
/// afterwards it will active the player
///</summary>
public partial class Room_transition_improvement : NinePatchRect
{
	private int _state = 0;
	private float _scale = 0.0f;
	private bool _finished = false;

	private float _max;
	private float _div;

	public Room_set_up _parent = null;

    public override void _Ready()
    {
		//GD.Print("open tran");
		_max = GetViewport().GetVisibleRect().Size.X;
		_div = _max/10;
        base._Ready();
    }

	///<summary>
	/// shifts between two modes, 
	/// on that just hesitates for a moment
	/// then it moves the transition screen
	///</summary>
    public override void _Process(double delta)
	{
		
		//GD.Print("this is functioning");
		if (!_finished)
		{
			switch (_state)
			{
				case 0:
					//GD.Print("step 0");
					open_state();
				break;
				case 1:
					//GD.Print("step 3");
					close_state();
				break;


			}
		}
		else 
		{
			//this.GetParent().RemoveChild(this);
			//GLOBAL_FUNCTIONS._transition = null;
			//this.QueueFree();
		}
	}

	private void open_state()
	{
		//GLOBAL_STATS._current_room_reference = null;

		if (_scale < 20)
			_scale++;
		else
		{
			_state = 1;

		}
		//this.SetSize(new Vector2(_scale, this.Size.Y));
	}

	private void close_state()
	{
		/*
		if (_scale - _div > 0)
		{
			_scale -= _div;
		}
		else
		{
			_scale = 2;
			_finished = true;

			GLOBAL_STATS._player._state = 0;
		}*/
		
		if (Position.X < _max)
			this.Position += (new Vector2(_div, 0));
		else
		{
			GLOBAL_STATS._changing_rooms = false;
			GLOBAL_STATS._player._state = 0;
			_finished = true;
			
		}
		//this.SetSize(new Vector2(_scale, this.Size.Y));

		//GC.Collect();
		//GC.WaitForPendingFinalizers();
	}
}
