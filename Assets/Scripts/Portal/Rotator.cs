using UnityEngine;

namespace Portal
{
	public class Rotator : MonoBehaviour
	{
		[Tooltip("in degrees per second")] public float speed;

		private void Update()
		{
			transform.Rotate(speed * Time.deltaTime * Vector3.up);
		}

	}
}
