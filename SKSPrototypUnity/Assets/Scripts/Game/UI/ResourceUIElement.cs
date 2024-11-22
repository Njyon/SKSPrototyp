using UnityEngine;
using UnityEngine.UI;

public class ResourceUIElement : MonoBehaviour
{
    [SerializeField] Image resourceImage;
    [SerializeField] TMPro.TextMeshProUGUI resourceText;

    public void SetupResourceElement(Sprite resourceSprit, string costAmountString)
    {
        resourceImage.sprite = resourceSprit;
        resourceText.text = costAmountString;
	}

    public void ClearResourceElement()
    {
        resourceImage.sprite = null;
        resourceText.text = "";
	}
}
