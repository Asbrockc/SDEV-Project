public partial class Door_town_to_area_1 : Door_base
{

	public override void _Ready()
	{
		this._desination = "res://ROOMS/Room_town.tscn";
		this._target_zone = "AREA_1_DOOR_1";
		this._x_off = 0;
		this._y_off = -1;
	}


	public override void _Process(double delta)
	{
	}
}
