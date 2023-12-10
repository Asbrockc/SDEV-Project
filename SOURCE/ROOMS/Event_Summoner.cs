using Godot;

///<summary>
/// Reads a flag when it is triggered
/// if it is true it will run the _summon_event
///</summary>
public partial class Event_Summoner : Node3D
{
	[Export] GLOBAL_STATS.FLAG_INDEX _flag;

	private bool _triggered = false;

    public override void _Process(double delta)
    {
	   //GD.Print("Event_Summoner");
       if (!_triggered)
	   {
			if (!GLOBAL_FUNCTIONS.GetFlag(_flag))
			{
				_summon_event();
			}

			_triggered = true;
	   }
    }

	///<summary>
	/// Empty summon event, this class can be inherted and fine tuned to override this event
	///</summary>
    public virtual void _summon_event()
	{
		
	}
}
