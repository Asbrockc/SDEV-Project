using Godot;
using System;
using System.Data;
using System.Net.Http;

public partial class Chat_Box : NinePatchRect
{
	public Chat_Type _chat_typer;
	public Chat_Timer _chat_timer;

	public string[] _messages;
	public int _message_count = 0;

	public int _width = 100;
	public int _height = 100;


	public int _state = 0;



	public override void _Ready()
	{		
		this.OffsetTop = 260.0f;

		_messages = new string[] {"Just a test.", "Hopefully this will work.", "Seems simple enough right?"};

		this._chat_typer.Text = _messages[_message_count++];
	}

	public override void _Process(double delta)
	{
		switch (_state)
		{
			case 0:
				Open_state();
			break;
			case 1:
				Run_state();
			break;
			case 2:
				Close_state();
			break;
		}
	}

	public void Open_state()
	{
		if (this.OffsetTop - 20 > -40)
			this.OffsetTop -= 20;
		else
		{
			this.OffsetTop = -40;
			_state = 1;
		}
	}

	public void Run_state()
	{
		if (_chat_typer.VisibleCharacters < _chat_typer.Text.Length)
		{
			if (this._chat_timer.IsStopped())
				this._chat_timer.Start();
		}
		else
		{
			this._chat_timer.Stop();

			if (Input.IsActionJustPressed("ui_up"))
			{
				if (_message_count >= _messages.Length)
				{
					_state = 2;
				}
				else
				{
					this._chat_typer.Text = _messages[_message_count++];
				}

				_chat_typer.VisibleCharacters = 0;
			}
		}
	}

	public void Close_state()
	{
		if (this.OffsetTop + 20 < 260)
			this.OffsetTop += 20;
		else
		{
			GLOBAL_FUNCTIONS._active_chat = null;
			this.GetParent().RemoveChild(this);
			this.Free();

			GLOBAL_STATS._player.player_state = 0;
		}
	}

	public void Set_Messages(string[] _messages)
	{
		this._messages = (string[])_messages.Clone();
		this._chat_typer.Text = _messages[0];
	}
}
