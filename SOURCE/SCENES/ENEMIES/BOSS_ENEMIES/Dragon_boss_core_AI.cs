using Godot;

public partial class Dragon_boss_core_AI : Boss_core_AI
{
	private Vector3 _base_offset;
	private Vector3 _main_head_offset;
	private Vector3 _alt_head_offset;

	public Dragon_boss_head[] _head = new Dragon_boss_head[3];
	public Dragon_neck[] _neck = new Dragon_neck[3];


	///<summary>
	/// Dragon boss core AI
	/// sets up the dragon and his various parts
	/// 3 heads and 3 neck segments
	///</summary>
    public override void _Ready()
    {
		_name = "DRAGON";

		_base_offset = new Vector3(0.0f, 0.0f, -0.3f);
		_main_head_offset = new Vector3(0.0f, 0.0f, 0.1f);
		_alt_head_offset = new Vector3(0.0f, 0.0f, 0.0f);

		int i = 0;
		_head[i] = GetNode<Dragon_boss_head>("Obj_enemy_base_head_1");
		_head[i]._head_id = i++;
		_head[i] = GetNode<Dragon_boss_head>("Obj_enemy_base_head_2");
		_head[i]._head_id = i++;
		_head[i] = GetNode<Dragon_boss_head>("Obj_enemy_base_head_3");
		_head[i]._head_id = i++;

		i = 0;	
		_neck[i] = GetNode<Dragon_neck>("Neck_1");
		_neck[i]._head_id = i++;
		_neck[i] = GetNode<Dragon_neck>("Neck_2");
		_neck[i]._head_id = i++;
		_neck[i] = GetNode<Dragon_neck>("Neck_3");
		_neck[i]._head_id = i++;

		for (i = 0; i < 3; i++)
			_boss_max_hp += _head[i]._max_health;
    }

	///<summary>
	/// This shifts the path the neck segmnets are made out of to keep them connected to the heads
	///</summary>
	public override void _main_function()
	{
		Dragon_boss_base _base = GetNode<Dragon_boss_base>("Obj_enemy_base");

		if (_base._Animator.CurrentAnimation == "")
			_base_offset.Y = -1.0f;
		else
			_base_offset.Y = 0.2f;

		int _head_count = 0;

		_boss_hp = 0;
		for (int i = 0; i < 3; i++)
		{	
			if (_neck[i] != null)
			{
				_neck[i].Curve.SetPointPosition(0, _base.Position + _base_offset);
				if (_head[i] != null)
				{
					_neck[i].Curve.SetPointPosition(1, _head[i].Position + (i == 0 ? _main_head_offset : _alt_head_offset));
					_boss_hp += _head[i]._health;
				}
				else if (!_neck[i]._dead)
					_neck[i]._dead = true;
			}
			else
				_head_count++; //counts defeated heads

		}
		
		//if it is all three the boss is over, trigger the body to finish
		if (_head_count == 3)
		{
			GetNode<Dragon_boss_base>("Obj_enemy_base")._health = 0;
			_defeated = true;
			GLOBAL_STATS._Camera._target = GetNode<Dragon_boss_base>("Obj_enemy_base");
			GLOBAL_STATS._Camera._y_dis = 4.0f;
			GLOBAL_STATS._Camera._z_dis = 2.0f; //4.0
		}
	}
}
