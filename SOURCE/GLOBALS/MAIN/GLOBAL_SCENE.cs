using Godot;

///<summary>
/// simple class that just defines the global Node for the GLobal scripts
///</summary>
public partial class GLOBAL_SCENE : Node3D
{
	public override void _Ready()
	{
		GLOBAL_STATS._main_scene = this;
	}

    public override void _Process(double delta)
    {
        GLOBAL_STATS._Level_Manager();
    }
}
