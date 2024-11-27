using System;
using UnityEngine;

[Serializable]
public abstract class Request
{
    public IRequestOwner Owner;

    public Request() { }

    public virtual void QueueRequest(IRequestOwner owner)
    {
        owner.QueueRequest(this); 
    }

    public abstract void TriggerRequest(IRequestOwner owner);
}
