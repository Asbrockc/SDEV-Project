using Godot;

public partial class Level_up_menu : Control
{
	public int _avalable_level = 0;
	public bool _destroy = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_avalable_level = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_BONUS_POINTS];

		GetChild<attribute_leveler_parent>(1)._attribute_label.Text = "Health";
		GetChild<attribute_leveler_parent>(2)._attribute_label.Text = "SWORD DAMAGE";
		GetChild<attribute_leveler_parent>(3)._attribute_label.Text = "BOW DAMAGE";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_destroy)
		{
			GetParent<Node3D>().RemoveChild(this);
			this.QueueFree();
		}
		else
			GetChild<Label>(5).Text = _avalable_level.ToString();
	}
}
