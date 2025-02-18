using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

	public void PointSelection(Vector2 selectionPoint)
	{
		if (IsUIBlockingPointSelect(selectionPoint)) return;
		ISelectable interactable = GetInteractable(selectionPoint, pointInteractionRayLengh);
		SelectionManager.Instance.Select(interactable);
	}

	ISelectable GetInteractable(Vector2 interactionPoint, float rayLength)
	{
		if (camera == null)
		{
			Debug.Log(Ultra.Utilities.Instance.DebugErrorString("PlayerSelectionComponent", "GetInteractable", "Camera was NULL!"));
			return null;
		}
		RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(camera.ScreenPointToRay(Mouse.current.position.ReadValue()));

		foreach (var hit in hits)
		{
			ISelectable interactable = hit.collider.GetComponent<ISelectable>();
			if (interactable != null)
			{
				return interactable; 
			}
		}
		return null;
	}

	bool IsUIBlockingPointSelect(Vector2 selectionPoint)
	{
		PointerEventData pointerData = new PointerEventData(EventSystem.current)
		{
			position = Input.mousePosition
		};

		List<RaycastResult> results = new List<RaycastResult>();
		Ultra.SKSUtilities.UIManager.GraphicRaycaster.Raycast(pointerData, results);

		foreach (RaycastResult result in results)
		{
			if (result.gameObject != null)
			{
				Debug.Log("Getroffenes UI-Element: " + result.gameObject.name);
				return true;
			}
		}
		return false;
	}

	public void Select(ISelectable interactable)
    {
		if (interactable == null) return;
		interactable.Select();
    }

	// TODO Rect Selection
}
