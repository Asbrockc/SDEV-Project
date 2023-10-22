using Godot;

public partial class Attribute_down_button : Button
{
    public override void _Pressed()
    {
        attribute_leveler_parent _parent = this.GetParent<attribute_leveler_parent>();

		if (_parent._current_level > 0)
		{
			_parent._current_level--;
			_parent._parent_node._avalable_level++;
		}
    }
}
