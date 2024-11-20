using UnityEngine;

/// <summary>
/// This class creates an Instance of its Typ, so Members of T can be modified in the Inspector
/// </summary>
/// <typeparam name="T">Class that is supposed to be SerializeReferenced</typeparam>
[System.Serializable]
public class ClassInstance<T>
{
	[SerializeReference] public T instance;
}