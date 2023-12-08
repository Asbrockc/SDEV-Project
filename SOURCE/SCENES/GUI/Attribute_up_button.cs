using Godot;

public partial class Attribute_up_button : Button
{
 	public override void _Pressed()
    {
        attribute_leveler_parent _parent = this.GetParent<attribute_leveler_parent>();
		Label _label = _parent.GetNode<Label>("Attribute_label");

		GD.Print(_label.Text);


		
		
		
		if (_parent._parent_node._avalable_level > 0)
		{
			bool _good_to_go = true;

			switch (_label.Text)
			{
				case "Health":
					_good_to_go = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_MAX_HEALTH]/2 + _parent._current_level < 10;
				break;
				case "SWORD DAMAGE":
					_good_to_go = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_STRENGTH] + _parent._current_level < 10;
				break;
				case "BOW DAMAGE":
					_good_to_go = GLOBAL_STATS._player_stats[GLOBAL_STATS.I_DEFENCE] + _parent._current_level < 10;
				break;
			}
			
			if (_good_to_go)
			{
				_parent._current_level++;
				_parent._parent_node._avalable_level--;
			}
			else
				GD.Print("Maxed out");
		}

    }
}
