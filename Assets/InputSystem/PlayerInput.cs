using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
	protected InputMap inputMap;
	protected GameObject pauseMenu = null;
	[SerializeField] protected PrefabData prefData;

	protected void OnEnable()
	{
		inputMap = new InputMap();
		inputMap.Player.Enable();
		inputMap.Player.PauseGame.performed += PauseTheGame;
	}

	protected void OnDisable()
	{
		inputMap.Player.Disable();
		inputMap.Player.PauseGame.performed -= PauseTheGame;
	}

	protected void PauseTheGame(InputAction.CallbackContext ctx)
	{
		if (!pauseMenu && prefData)
		{
			Time.timeScale = 0.0f;
			Cursor.visible = true;
			pauseMenu = Instantiate(prefData.pauseMenuPref);
		}
	}
}
