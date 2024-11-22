using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildingScriptableRequest", menuName = "Scriptable Objects/Request/BuildingScriptableRequest")]
public class BuildingScriptableRequest : ScriptableRequest
{
    public Sprite RequestSprite;
    public string RequestName;
    public string RequestDescription;
}
