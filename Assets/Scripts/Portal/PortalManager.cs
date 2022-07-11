using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Portal
{
	public class PortalManager : MonoBehaviour
	{
		public string scene;

		private Vector3 _localScale;

		private void Start()
		{
			Transform trans = transform;
			_localScale = trans.localScale;
			trans.localScale = Vector3.zero;

			InvokeRepeating(nameof(CheckEnemies), 5, 1.0f);

			if(scene == "")
				Debug.LogWarning("PortalManager: No scene specified for the portal!");
		}

		private void CheckEnemies()
		{
			// Debug.Log(_enemyManager.GetEnemiesCount());
			if (EnemyManager.Instance.GetEnemiesCount() == 0)
			{
				transform.localScale = _localScale;
				CancelInvoke(nameof(CheckEnemies));
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				SceneManager.LoadScene(scene);
			}
		}
	}
}
