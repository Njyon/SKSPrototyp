using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraMoveScript : MonoBehaviour
{
    [SerializeField] float panBorderThickness = 20;
    [SerializeField] float panSpeed = 5;
	[SerializeField] Vector2 panLimit = new Vector2(40, 40);

	Vector3 currentCameraPos = Vector3.zero;
	Vector3 CurrentCameraPos { get { return currentCameraPos; } set { currentCameraPos = value; } }

	private void Awake()
	{
		CurrentCameraPos = transform.position;
	}

	public void MoveCamera(Vector2 mousePos)
	{
		CurrentCameraPos = transform.position;

		Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString("PlayerCameraController", "MouseMove", "Mouse Position = " + mousePos.ToString()));
		if (mousePos.y >= Screen.height - panBorderThickness && mousePos.y <= Screen.height)
		{
			// Oberer Rand
			float factor = CalculateZoneProgress(Screen.height - panBorderThickness, panBorderThickness, mousePos.y);
			currentCameraPos.y += (panSpeed * factor) * Time.deltaTime;
		}
		if (mousePos.y <= panBorderThickness && mousePos.y >= 0)
		{
			// Unterer Rand
			float factor = CalculateZoneProgress(0, panBorderThickness, mousePos.y, true);
			currentCameraPos.y -= (panSpeed * factor) * Time.deltaTime;
		}
		if (mousePos.x >= Screen.width - panBorderThickness && mousePos.x <= Screen.width)
		{
			// Rechter Rand
			float factor = CalculateZoneProgress(Screen.width - panBorderThickness, panBorderThickness, mousePos.x);
			currentCameraPos.x += (panSpeed * factor) * Time.deltaTime;
		}
		if (mousePos.x <= panBorderThickness && mousePos.x >= 0)
		{
			// Linker Rand
			float factor = CalculateZoneProgress(0, panBorderThickness, mousePos.x, true);
			currentCameraPos.x -= (panSpeed * factor) * Time.deltaTime;
		}

		currentCameraPos.x = math.clamp(currentCameraPos.x, -panLimit.x, panLimit.x);
		currentCameraPos.y = math.clamp(currentCameraPos.y, -panLimit.y, panLimit.y);
	}

	private void LateUpdate()
	{
		transform.position = CurrentCameraPos;
	}

	public static float CalculateZoneProgress(float bound, float range, float value, bool flip = false)
	{
		float progress = Mathf.Clamp((value - bound) / range, 0f, 1f);
		return flip ? 1f - progress : progress;
	}
}
