using Godot;

/// <summary>
/// global camera object
/// if there is a target the camera will move towards it
/// </summary>
public partial class Obj_player_camera : Camera3D
{
	public Node3D _target = null;

	public float _shake = 0;//0.01f;
	public float _y_dis = 3.0f; //4.0
	public float _z_dis = 3.0f;

	private Vector3 _head_shift = new (0,0,0);

	public override void _Ready()
	{
		GLOBAL_STATS._Camera = this;
	}

	/// <summary>
	/// if there is a target the camera uses to the Lerp function to returns locations that are 
	/// gradually close and closer to the target to give a smooth camera
	/// </summary>
	public override void _Process(double delta)
	{
		if (_target != null)
		{

			if (_shake - .02 > 0)
				_shake -= .01f;
			else
				_shake = 0;


			_head_shift.X = (float)Mathf.Lerp(this.Position.X, _target.Position.X + GLOBAL_FUNCTIONS.Choose(-_shake,_shake), .1);
			_head_shift.Y = (float)Mathf.Lerp(this.Position.Y, _target.Position.Y + _y_dis + GLOBAL_FUNCTIONS.Choose(-_shake,_shake), .1);
			_head_shift.Z = (float)Mathf.Lerp(this.Position.Z, _target.Position.Z + _z_dis + GLOBAL_FUNCTIONS.Choose(-_shake,_shake), .1);
			this.Position = _head_shift;

			/*
			Position = new Vector3(
				_target.Position.X , 
				_target.Position.Y + _y_dis + GLOBAL_FUNCTIONS.Choose(-_shake,_shake), 
				_target.Position.Z + _z_dis + GLOBAL_FUNCTIONS.Choose(-_shake,_shake));
				*/
		}
	}
}
