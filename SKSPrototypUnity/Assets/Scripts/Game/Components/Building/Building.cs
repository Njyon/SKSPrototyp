using UnityEngine;

public abstract class Building : MonoBehaviour, ISelectable
{
	public abstract void Select();
	public abstract void Unselect();	
}
