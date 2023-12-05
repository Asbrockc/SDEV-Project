using Godot;

///<summary>
/// simple class that just defines the global Node for the GLobal scripts
///</summary>
public partial class GLOBAL_SCENE : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GLOBAL_STATS._main_scene = this;
	}

    public override void _Process(double delta)
    {
		//GD.Print(GLOBAL_STATS._current_room_reference);
        GLOBAL_STATS._Level_Manager();
    }
}
