using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RequestCost
{
    public ScriptableCurrency Currency;
    public int Cost;
    public string FormatCostString;

    public string CostString
    {
        get { return Cost.ToString(FormatCostString); }
    }
}

public abstract class ScriptableRequest : ScriptableObject
{
    [SerializeReference, SubclassSelector]
    public List<Request> Requests;

    public List<RequestCost> Costs;
}
