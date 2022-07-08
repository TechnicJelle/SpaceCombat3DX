using System;
using UnityEngine;

namespace Player
{
	public class PlayerMovement : MonoBehaviour
	{
		//https://www.desmos.com/calculator/6vdxfyzafc
		//https://www.desmos.com/calculator/cf6sv6mzq8
		public float thrust = 10;

		public float threshold = 0.1f;
		public float pitchSpeed = 10;
		public float rollSpeed = 10;

		public float speedFacMin = 2f;
		public float speedFacMax = 1f;

		public static float MaxSpeed = 10;

		private Rigidbody _rigidbody;

		// Start is called before the first frame update
		private void Start()
		{
			Debug.Log("Player Movement");
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			// === ROTATION === //

			// Vector2 mousePos = new(Input.mousePosition.x, Input.mousePosition.y);
			// float size = Mathf.Min(Screen.width, Screen.height);
			// mousePos /= size;
			// mousePos.Normalize();
			// mousePos *= size / 2;

			float mouseY = Input.mousePosition.y / Screen.height * 2 - 1;
			float rotSpeedFac = (float) Utils.Map(_rigidbody.velocity.magnitude, 0, MaxSpeed, speedFacMin, speedFacMax);
			if(Math.Abs(mouseY) > threshold)
				transform.Rotate(transform.InverseTransformDirection(transform.right), -mouseY * pitchSpeed * Time.fixedDeltaTime * rotSpeedFac);
			// Debug.Log("mouse pos: " + mouseY);

			float mouseX = Input.mousePosition.x / Screen.width * 2 - 1;
			if (Math.Abs(mouseX) > threshold)
				transform.Rotate(transform.InverseTransformDirection(transform.forward), -mouseX * rollSpeed * Time.fixedDeltaTime * rotSpeedFac);





			// === MOVEMENT ===	//
			Vector3 addedForce = Input.GetAxis("Vertical") * thrust * Time.deltaTime * transform.forward;
			MaxSpeed = Math.Max(MaxSpeed, (addedForce.magnitude / _rigidbody.drag - Time.fixedDeltaTime * addedForce.magnitude) / _rigidbody.mass);
			if(_rigidbody.velocity.magnitude > MaxSpeed * 0.1f)
				MaxSpeed *= 0.999f;
			// Debug.Log("MaxSpeed: " + MaxSpeed);

			// float fac = thrust / (_rigidbody.velocity.sqrMagnitude + thrust / maxSpeed);
			// Debug.Log(fac + ", " + _rigidbody.velocity.sqrMagnitude);
			// _rigidbody.AddForce(Input.GetAxis("Vertical") * fac * Time.deltaTime * transform.forward);

			_rigidbody.AddForce(addedForce);
			// Debug.Log(_rigidbody.velocity.magnitude);
		}
	}
}
