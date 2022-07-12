using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
	public class EnemyHealth : MonoBehaviour
	{
		public int startingHealth = 100;
		[Tooltip("Lower than this and the enemy dies")] public int deadHealth = 10;
		[Tooltip("Player Collision Damage Multiplier")] public float playerCollisionDamageMultiplier = 1.0f;
		private int _currentHealth;

		private Image _healthBar;

		private void Start()
		{
			_healthBar = GetComponentInChildren<Image>();
			if(_healthBar == null)
			{
				Debug.LogError("Health bar is null");
			}
			_currentHealth = startingHealth;
		}

		private void Update()
		{
			_healthBar.fillAmount = (float)_currentHealth / startingHealth;
		}

		private void OnCollisionEnter(Collision collision)
		{
			Damage((int) (collision.relativeVelocity.magnitude * playerCollisionDamageMultiplier));
		}

		public void Damage(int damage)
		{
			_currentHealth -= damage;
			if(_currentHealth <= deadHealth)
			{
				EnemyManager.Instance.KillEnemy(this);
			}
		}
	}
}
