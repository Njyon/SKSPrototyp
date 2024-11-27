using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("GameObjectLinks")]
    [SerializeField] Canvas canvas;
    [SerializeField] GraphicRaycaster graphicRaycaster;
    [SerializeField] GameObject selectionHolder;

    public Canvas Canvas {  get { return canvas; } }   
    public GraphicRaycaster GraphicRaycaster { get {  return graphicRaycaster; } }

    public void CreateSelectorPanelForRequests(List<BuildingScriptableRequest> requests, out SelectorPanelElement selectorPanelElement, IRequestOwner requestOwner)
    {
        selectorPanelElement = CreateSelectionPanel();
        selectorPanelElement.SetupSelectorPanelElement();
		if (selectorPanelElement == null || selectorPanelElement.ContentHolder == null)
		{
			Debug.LogError(Ultra.Utilities.Instance.DebugErrorString("UIManager", "CreateSelectorPanelForRequests", "Panel or ContentTransform was NULL!"));
            return;
		}
        foreach (var request in requests)
        {
            RequestUIElement requestUIElement = CreateRequestUIElement(selectorPanelElement.ContentHolder);
            requestUIElement.SetupRequestUIElement(request.RequestSprite, request.RequestName, request.Requests, requestOwner);
            foreach (var cost in request.Costs)
            {
				ResourceUIElement resourceElement = CreateResourceUIElement(requestUIElement.ResourceCostHolder);
                resourceElement.SetupResourceElement(cost.Currency.CurrencyImage, cost.CostString);
				requestUIElement.ResourceCosts.Add(resourceElement);
			}
			selectorPanelElement.UIElements.Add(requestUIElement);
		}
	}

    public void UnselectSelectorPanel(SelectorPanelElement selectorPanelElement)
    {
		RemoveSelectorPanelElement(selectorPanelElement);
	}

    ResourceUIElement CreateResourceUIElement(Transform parent)
    {
		if (parent == null)
		{
			Debug.LogError(Ultra.Utilities.Instance.DebugErrorString("UIManager", "CreateResourceUIElement", "Parent for ResourceUIElement was NULL!"));
			return null;
		}
        ResourceUIElement resourceElement = GameAssets.Instance.UI.ResourceUIElementPool.GetValue();
		resourceElement.transform.parent = parent;
        resourceElement.SavePool(GameAssets.Instance.UI.ResourceUIElementPool);
        return resourceElement;
	}

    RequestUIElement CreateRequestUIElement(Transform parent)
    {
        if (parent == null)
        {
			Debug.LogError(Ultra.Utilities.Instance.DebugErrorString("UIManager", "CreateRequestUIElement", "Parent for RequestUIElement was NULL!"));
            return null;
		}
        RequestUIElement requestElement = GameAssets.Instance.UI.RequestUIElementPool.GetValue();
        requestElement.transform.parent = parent;
        requestElement.SavePool(GameAssets.Instance.UI.RequestUIElementPool);
        return requestElement;
	}

    SelectorPanelElement CreateSelectionPanel()
    {
        SelectorPanelElement selectorPanelElement = GameAssets.Instance.UI.SelectorPanelElementPool.GetValue();
		selectorPanelElement.transform.SetParent(selectionHolder.transform, false);
		selectorPanelElement.SavePool(GameAssets.Instance.UI.SelectorPanelElementPool);
        return selectorPanelElement;   
    }

    void RemoveSelectorPanelElement(SelectorPanelElement selectorPanelElement)
    {
        if (selectorPanelElement == null)
        {
            Debug.LogError(Ultra.Utilities.Instance.DebugErrorString("UIManager", "RemoveSelectorPanelElement", "SelectorPanelElement was NULL!"));
            return;
        }
		selectorPanelElement.Cleanup();
	}
    
}
