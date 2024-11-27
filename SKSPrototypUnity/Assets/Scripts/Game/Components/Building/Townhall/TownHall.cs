using System.Collections.Generic;
using UnityEngine;

public class TownHall : Building, IRequestOwner
{
	public List<BuildingScriptableRequest> possibleRequests = new List<BuildingScriptableRequest>();
	SelectorPanelElement selectorPanel;

	public override void Select()
	{
		Ultra.Utilities.Instance.DebugLogOnScreen("Selected " + gameObject.name, 5f, 200);
		if (selectorPanel != null)
		{
			Debug.LogError(Ultra.Utilities.Instance.DebugErrorString("TownHall", "Select", "SelectorPanel was not Null but should be!"));
			return;
		}
		Ultra.SKSUtilities.UIManager.CreateSelectorPanelForRequests(possibleRequests, out selectorPanel, this);
	}

	public override void Unselect()
	{
		Ultra.Utilities.Instance.DebugLogOnScreen("Unselect " + gameObject.name, 5f, 200);
		if (selectorPanel == null)
		{
			Debug.LogError(Ultra.Utilities.Instance.DebugErrorString("TownHall", "Unselect", "SelectorPanel was Null but shouldn't be!"));
			return;
		}
		Ultra.SKSUtilities.UIManager.UnselectSelectorPanel(selectorPanel);
		selectorPanel = null;
	}
}
