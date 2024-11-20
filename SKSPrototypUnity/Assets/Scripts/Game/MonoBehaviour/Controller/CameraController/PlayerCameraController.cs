using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    InputSystem_Actions playerInputs;

    void Start()
    {
		playerInputs = new InputSystem_Actions();
		playerInputs.Enable();

		playerInputs.Player.MouseMove.performed += ctx => MousePos(ctx.ReadValue<Vector2>());
	}

	void MousePos(Vector2 mousePos)
	{
		Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString("PlayerCameraController", "MouseMove", "Mouse Position = " + mousePos.ToString()), 0f);
		Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString("PlayerCameraController", "MouseMove", "Mouse Position = " + mousePos.ToString()), 0f);
		Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString("PlayerCameraController", "MouseMove", "Mouse Position = " + mousePos.ToString()), 0f);
		Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString("PlayerCameraController", "MouseMove", "Mouse Position = " + mousePos.ToString()), 0f);
		Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString("PlayerCameraController", "MouseMove", "Mouse Position = " + mousePos.ToString()), 0f);
		Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString("PlayerCameraController", "MouseMove", "Mouse Position = " + mousePos.ToString()), 0f);
		Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString("PlayerCameraController", "MouseMove", "Mouse Position = " + mousePos.ToString()), 0f);
	}
}
