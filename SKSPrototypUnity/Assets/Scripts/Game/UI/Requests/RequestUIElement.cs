using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RequestUIElement : MonoBehaviour
{
    [SerializeField] Image requestImage;
    [SerializeField] TMPro.TextMeshProUGUI requestName;
    [SerializeField] Button requestButton;
    [SerializeField] GameObject requestResourceCostHolder;

    public void SetupRequestUIElement(Sprite requestSprit, string requestNameString, UnityAction buttonAction, List<ResourceInfo> neededRecources)
    {
        requestImage.sprite = requestSprit;
		requestName.text = requestNameString;
        requestButton.onClick.AddListener(buttonAction);
	}

    public void ClearRequestUIElement()
    {
        requestImage.sprite = null;
        requestName.text = "";
        requestButton.onClick.RemoveAllListeners();
    }
}
