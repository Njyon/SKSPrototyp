using System.Collections.Generic;
using UnityEngine;

public class TownHall : Building
{
	public List<BuildingScriptableRequest> possibleRequests = new List<BuildingScriptableRequest>();

	public override void Select()
	{
		Ultra.Utilities.Instance.DebugLogOnScreen("Selected " + gameObject.name, 5f);
	}

	public override void Unselect()
	{
		Ultra.Utilities.Instance.DebugLogOnScreen("Unselect " + gameObject.name, 5f);
	}
}
