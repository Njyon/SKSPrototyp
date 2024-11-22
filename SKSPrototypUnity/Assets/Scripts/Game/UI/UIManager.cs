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

    SelectorPanelElement CreateSelectionPanel()
    {
        SelectorPanelElement selectorPanelElement = GameAssets.Instance.UI.SelectorPanelElementPool.GetValue();
		selectorPanelElement.transform.parent = selectionHolder.transform;
        return selectorPanelElement;   
    }

    void RemoveSelectorPanelElement(SelectorPanelElement selectorPanelElement)
    {
        if (selectorPanelElement == null)
        {
            Debug.LogError(Ultra.Utilities.Instance.DebugErrorString("UIManager", "RemoveSelectorPanelElement", "SelectorPanelElement was NULL!"));
            return;
        }
		GameAssets.Instance.UI.SelectorPanelElementPool.ReturnValue(selectorPanelElement);
	}
    
}
