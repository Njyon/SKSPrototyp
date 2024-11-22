using UnityEngine;

public enum CurrencyType
{
    Unknown,
    Gold,
    Wood,
    Iron,
    Food,
    Stone,
    Worker,
}

[CreateAssetMenu(fileName = "ScriptableCurrency", menuName = "Scriptable Objects/Currency/ScriptableCurrency")]
public class ScriptableCurrency : ScriptableObject
{
    public Sprite CurrencyImage;
    public CurrencyType CurrencyTyp;
}
