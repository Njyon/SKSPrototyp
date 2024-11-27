using UnityEngine;

public interface IRequestOwner
{
    public void QueueRequest(Request request);
}
