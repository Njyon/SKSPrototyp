using UnityEngine;

public class TownHall : Building
{
	public override void Select()
	{
		Ultra.Utilities.Instance.DebugLogOnScreen("Interacted with " + gameObject.name, 5f);
	}
}
