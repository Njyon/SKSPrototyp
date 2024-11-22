using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RequestCost
{
    public ScriptableCurrency Currency;
    public int Cost;
}

public abstract class ScriptableRequest : ScriptableObject
{
    [SerializeReference, SubclassSelector]
    public List<Request> Requests;

    public List<RequestCost> Costs;
}
