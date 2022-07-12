using UnityEngine;

namespace Menu
{
	public class PauseManager : MonoBehaviour
	{
		public GameObject pauseMenu;

		public void Pause()
		{
			pauseMenu.SetActive(true);
			Time.timeScale = 0f;
		}

		public void Resume()
		{
			pauseMenu.SetActive(false);
			Time.timeScale = 1f;
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (pauseMenu.activeInHierarchy)
				{
					Resume();
				}
				else
				{
					Pause();
				}
			}
		}
	}
}
