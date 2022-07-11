using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
	public class TrackTarget : MonoBehaviour
	{
		public Transform target;

		[Tooltip("Rotation Speed in degrees per second")]
		public float rotationSpeed;

		public float thrust;

		public float maxApproachDistance = 10f;
		public float brakeFactor = 0.5f;

		public int rays = 10;
		public float angleBetweenRays = 1f;
		public float rayLength = 10f;

		private Rigidbody _rb;

		private Transform _transformCached;
		private Vector3 _position;

		private void Start()
		{
			if (target == null) Debug.LogError("No target assigned");
			_rb = GetComponent<Rigidbody>();
			if(_rb == null) Debug.LogError("This game object does not have a Rigidbody");
		}

		private void Update()
		{
			_transformCached = transform;
			_position = _transformCached.position;


			// Determine which direction to rotate towards
			Vector3 targetDirection = target.position - _position;

			Vector3 dir = FindDirToTarget(targetDirection);
			dir.Normalize();

			bool hitSomething = false;
			bool missedAnything = false;
			Dictionary<float, Vector3> anglesAndDirections = new();
			for (int x = -rays; x <= rays; x++)
			{
				for (int y = -rays; y <= rays; y++)
				{
					Vector3 rayDir = Quaternion.AngleAxis(x * angleBetweenRays, _transformCached.TransformDirection(Vector3.up))
					                 * Quaternion.AngleAxis(y * angleBetweenRays, _transformCached.TransformDirection(Vector3.right)) * dir;
					if(Physics.Raycast(_position, rayDir, out RaycastHit hit, rayLength))
					{
						//hit
						if(!hitSomething && hit.rigidbody.isKinematic)
							hitSomething = true;
						Debug.DrawRay(_position, rayDir * hit.distance, Color.green);
					}
					else
					{
						//miss
						missedAnything = true;
						Debug.DrawRay(_position, rayDir * rayLength, Color.red);
						float angle = Vector3.Angle(dir, rayDir);
						//if angle is already in the dictionary, check if the direction is better than the current one by comparing the angle to the direction to the target
						if (anglesAndDirections.ContainsKey(angle))
						{
							if (Vector3.Angle(anglesAndDirections[angle], target.position - _position) > Vector3.Angle(rayDir, target.position - _position))
							{
								anglesAndDirections[angle] = rayDir;
							}
						}
						else
						{
							anglesAndDirections.Add(angle, rayDir);
						}
					}
				}
			}

			if (hitSomething)
			{
				// Debug.Log("Hit something" + anglesAndDirections.Count);
				//find in dictionary anglesAndDirections the biggest angle
				float biggestAngle = 0f;
				Vector3 biggestDir = Vector3.zero;
				foreach (KeyValuePair<float, Vector3> angleAndDir in anglesAndDirections)
				{
					if (angleAndDir.Key > biggestAngle)
					{
						biggestAngle = angleAndDir.Key;
						biggestDir = angleAndDir.Value;
					}
				}
				dir = FindDirToTarget(biggestDir);
			}

			if (!missedAnything)
			{
				//stuck
				Debug.LogWarning(this + " got stuck!");
			}

			// Calculate a rotation a step closer to the target and applies rotation to this object
			_transformCached.rotation = Quaternion.LookRotation(dir);

			MoveTowardsTarget(dir);
		}

		private void MoveTowardsTarget(Vector3 dir)
		{
			if(Vector3.Distance(_position, target.position) > maxApproachDistance)
				_rb.AddForce(thrust * Time.deltaTime * dir);
			else
				_rb.AddForce(-thrust * brakeFactor * Time.deltaTime * dir); //brake
		}

		private Vector3 FindDirToTarget(Vector3 targetDirection)
		{
			const float convFac = Mathf.PI / 180f; //for converting degrees to radians
			// The step size is equal to speed times frame time.
			float singleStep = rotationSpeed * convFac * Time.deltaTime;

			// Rotate the forward vector towards the target direction by one step
			Vector3 newDirection = Vector3.RotateTowards(_transformCached.forward, targetDirection, singleStep, 0.0f);

			// Draw a ray pointing at our target in
			Debug.DrawRay(_position, newDirection, Color.blue);

			return newDirection;
		}
	}
}
