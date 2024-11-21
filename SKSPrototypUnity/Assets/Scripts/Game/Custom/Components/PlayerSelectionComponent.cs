using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectionComponent
{
	Camera camera;
	float pointInteractionRayLengh;

	public PlayerSelectionComponent(Camera camera, float pointInteractionRayLengh)
	{
		this.camera = camera;
		this.pointInteractionRayLengh = pointInteractionRayLengh;

		// LOGS
		if (camera == null)
			Debug.Log(Ultra.Utilities.Instance.DebugErrorString("PlayerSelectionComponent", "PlayerSelectionComponentContstruktor", "Camera was NULL!"));
	}

	public void PointInteract(Vector2 interactionPoint)
	{
		IInteractable interactable = GetInteractable(interactionPoint, pointInteractionRayLengh);
		Select(interactable);
	}

	public IInteractable GetInteractable(Vector2 interactionPoint, float rayLength)
	{
		if (camera == null)
		{
			Debug.Log(Ultra.Utilities.Instance.DebugErrorString("PlayerSelectionComponent", "GetInteractable", "Camera was NULL!"));
			return null;
		}
		RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(camera.ScreenPointToRay(Mouse.current.position.ReadValue()));

		foreach (var hit in hits)
		{
			IInteractable interactable = hit.collider.GetComponent<IInteractable>();
			if (interactable != null)
			{
				return interactable; 
			}
		}
		return null;
	}

	public void Select(IInteractable interactable)
    {
		if (interactable == null) return;
		interactable.Select();
    }

	// TODO Rect Selection
}
