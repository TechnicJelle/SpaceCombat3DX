using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Portal
{
	public class ColliderGenerator : MonoBehaviour
	{
		public int amount = 10;
		public float circleRadius = 2f;

		public float capsuleRadius = 0.1f;
		public float capsuleHeight = 1.5f;

		private void Start()
		{
			Quaternion beginRotation = transform.rotation;
			transform.rotation = Quaternion.identity;

			float stepSize = Mathf.PI * 2 / amount;

			List<GameObject> empties = new();
			for (float i = 0; i < Mathf.PI * 2; i += stepSize)
			{
				GameObject child = new();
				child.transform.parent = transform;
				child.transform.localScale = Vector3.one;
				child.transform.localPosition = Vector3.zero;
				empties.Add(child);

				CapsuleCollider capsuleCollider = child.AddComponent<CapsuleCollider>();
				capsuleCollider.transform.parent = child.transform;
				capsuleCollider.center = Vector3.right * circleRadius;
				capsuleCollider.radius = capsuleRadius;
				capsuleCollider.height = capsuleHeight;
				capsuleCollider.direction = 2;
			}

			for (int i = 0; i < empties.Count; i++)
			{
				empties[i].transform.Rotate(0, i * 360f / amount, 0);
			}

			transform.rotation = beginRotation;
		}
	}
}
