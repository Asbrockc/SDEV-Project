using Godot;

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

    public virtual void _summon_event()
	{
		
	}
}
