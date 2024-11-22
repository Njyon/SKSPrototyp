using System;
using UnityEngine;

[Serializable]
public abstract class Request
{
    public Request() { }

    public abstract void SendRequest();
}
