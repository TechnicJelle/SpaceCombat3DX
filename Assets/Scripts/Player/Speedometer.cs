using UnityEngine;
using UnityEngine.UI;

namespace Player
{
	public class Speedometer : MonoBehaviour
	{
		public Slider slider;

		private Rigidbody _rb;

		private void Start()
		{
			_rb = GetComponent<Rigidbody>();
			if(_rb == null) Debug.LogError("Speedometer: Rigidbody not found");
		}

		private void Update()
		{
			slider.value = _rb.velocity.magnitude / PlayerMovement.MaxSpeed;
		}
	}
}
