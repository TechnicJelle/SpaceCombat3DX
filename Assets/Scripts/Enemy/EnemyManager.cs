using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class EnemyManager : MonoBehaviour
	{
		public static EnemyManager Instance;

		private readonly List<EnemyHealth> _enemies = new();

		private void Start()
		{
			foreach (EnemyHealth child in GetComponentsInChildren<EnemyHealth>())
			{
				_enemies.Add(child);
			}

			Debug.Log("Enemies in this level: " + GetEnemiesCount());

			Instance = this;
		}

		public int GetEnemiesCount()
		{
			return _enemies.Count;
		}

		public void KillEnemy(EnemyHealth enemy)
		{
			Destroy(enemy.gameObject);
			_enemies.Remove(enemy);
		}
	}
}
