using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RequestUIElement : MonoBehaviour, IUIElement, IUIPoolableUIElement<RequestUIElement>
{
    [SerializeField] Image requestImage;
    [SerializeField] TMPro.TextMeshProUGUI requestName;
    [SerializeField] Button requestButton;
    [SerializeField] GameObject requestResourceCostHolder;
	public Transform ResourceCostHolder
	{
		get { return requestResourceCostHolder.transform; }
	}

	List<IUIElement> resourceCosts;
	public List<IUIElement> ResourceCosts
	{
		get { return resourceCosts; }
	}

	List<Request> buttonActions;
	GameObjectTypePool<RequestUIElement> ownerPool;
	IRequestOwner requestOwner;

	public void SetupRequestUIElement(Sprite requestSprit, string requestNameString, List<Request> buttonActions, IRequestOwner requestOwner)
    {
		resourceCosts = new List<IUIElement>();
		this.requestOwner = requestOwner;

		requestImage.sprite = requestSprit;
		requestName.text = requestNameString;
        this.buttonActions = buttonActions;

      
		requestButton.onClick.AddListener(ButtonPress);
	}

	void ButtonPress()
	{
		foreach (var action in buttonActions)
		{
			action.QueueRequest(requestOwner);
		}
	}

    public void Cleanup()
    {
        requestImage.sprite = null;
        requestName.text = "Name";
		
		requestButton.onClick.RemoveListener(ButtonPress);
        buttonActions.Clear();

		foreach (var resourceCost in resourceCosts)
		{
			resourceCost.Cleanup();
		}

		requestOwner = null;
		GetPool().ReturnValue(this);
    }

	public void SavePool(GameObjectTypePool<RequestUIElement> pool)
	{
		this.ownerPool = pool;
	}

	public GameObjectTypePool<RequestUIElement> GetPool()
	{
		return ownerPool;
	}
}
