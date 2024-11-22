using UnityEngine;

public enum ResourceTyp
{
    Unknown,
    Gold,
    Wood,
    Iron
}

public struct ResourceInfo 
{
    public ResourceTyp typ;
    public int Amount;

    public ResourceInfo(ResourceTyp typ, int amount)
    {
        this.typ = typ;
        this.Amount = amount;
    }
}
