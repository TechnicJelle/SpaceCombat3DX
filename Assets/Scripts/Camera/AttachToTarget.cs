using Player;
using UnityEngine;

namespace Camera
{
	public class AttachToTarget : MonoBehaviour
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

		private UnityEngine.Camera[] _childCameras;

		private void Start()
		{
			_myTransform = transform;
			_myCamera = GetComponent<UnityEngine.Camera>();
			if(_myCamera == null) Debug.LogError("This game object does not have a camera component!");
			_targetRigidbody = target.GetComponent<Rigidbody>();
			if(_targetRigidbody == null) Debug.LogError("Target has no rigidbody");
			_childCameras = GetComponentsInChildren<UnityEngine.Camera>();
		}

		private void Update()
		{
			// === CAMERA POSITION & OFFSET === //
			Vector3 tempOffset = offset;
			tempOffset.z = (float) Utils.Map(_targetRigidbody.velocity.magnitude, 0, PlayerMovement.MaxSpeed, -lowestCameraPosition, -highestCameraPosition);
			Vector3 targetPosition = target.transform.TransformPoint(tempOffset);
			_myTransform.position = targetPosition;

			// === CAMERA FOV === //
			float fov = (float) Utils.Map(_targetRigidbody.velocity.magnitude, 0, PlayerMovement.MaxSpeed, lowestFOV, highestFOV);
			_myCamera.fieldOfView = fov;
			foreach (UnityEngine.Camera childCamera in _childCameras)
			{
				childCamera.fieldOfView = fov;
			}

			// === CAMERA ROTATION === //
			Quaternion targetRotation = target.transform.rotation;
			_myTransform.rotation = targetRotation;
			_myTransform.Rotate(rotation);
		}
	}
}
