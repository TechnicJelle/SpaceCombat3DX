using Player;
using UnityEngine;

namespace Camera
{
	public class FollowTarget : MonoBehaviour
	{
		public GameObject target;
		public Vector3 offset = new(0, 3, -5);
		public Vector3 rotation = new(0, 0, 0);

		public int lowestFOV = 70;
		public int highestFOV = 80;
		public float lowestCameraPosition = 9.04f;
		public float highestCameraPosition = 5;

		private UnityEngine.Camera _myCamera;
		private Transform _myTransform;
		private Rigidbody _targetRigidbody;

		// Start is called before the first frame update
		private void Start()
		{
			_myTransform = transform;
			_myCamera = GetComponent<UnityEngine.Camera>();
			_targetRigidbody = target.GetComponent<Rigidbody>();
			if(_targetRigidbody == null)
			{
				Debug.LogError("Target has no rigidbody");
			}
		}

		// Update is called once per frame
		private void Update()
		{
			// === CAMERA POSITION & OFFSET === //
			Vector3 tempOffset = offset;
			tempOffset.z = (float) Utils.Map(_targetRigidbody.velocity.magnitude, 0, PlayerMovement.MaxSpeed, -lowestCameraPosition, -highestCameraPosition);
			Vector3 targetPosition = target.transform.TransformPoint(tempOffset);
			_myTransform.position = targetPosition;

			// === CAMERA FOV === //
			_myCamera.fieldOfView = Map(_targetRigidbody.velocity.magnitude, 0, PlayerMovement.MaxSpeed, lowestFOV, highestFOV);

			// === CAMERA ROTATION === //
			Quaternion targetRotation = target.transform.rotation;
			_myTransform.rotation = targetRotation;
			_myTransform.Rotate(rotation);

		}

		private static float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
		{
			return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
		}
	}
}
