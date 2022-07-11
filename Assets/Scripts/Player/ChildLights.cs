using System.Linq;
using UnityEngine;

namespace Player
{
	public class ChildLights : MonoBehaviour
	{
		public Color color = Color.magenta;
		public float bigLightRange = 3.0f;
		public float mediumLightRange = 2.0f;
		public float smallLightRange = 1.0f;

		public float bigLightIntensity = 10f;
		public float mediumLightIntensity = 8f;
		public float smallLightIntensity = 6f;


		private Light[] _bigLights;
		private Light[] _mediumLights;
		private Light[] _smallLights;

		private void Start()
		{
			_bigLights = GetComponentsInChildren<Light>().Where(l => l.name.Contains("Big")).ToArray();
			_mediumLights = GetComponentsInChildren<Light>().Where(l => l.name.Contains("Mid")).ToArray();
			_smallLights = GetComponentsInChildren<Light>().Where(l => l.name.Contains("Smol")).ToArray();

			if(_bigLights.Length + _mediumLights.Length + _smallLights.Length == 0) Debug.LogError("Player child lights not found");
		}

		private void Update()
		{
			foreach (Light bigLight in _bigLights)
			{
				bigLight.color = color;
				bigLight.range = bigLightRange;
				bigLight.intensity = bigLightIntensity;
			}

			foreach (Light mediumLight in _mediumLights)
			{
				mediumLight.color = color;
				mediumLight.range = mediumLightRange;
				mediumLight.intensity = mediumLightIntensity;
			}

			foreach (Light smallLight in _smallLights)
			{
				smallLight.color = color;
				smallLight.range = smallLightRange;
				smallLight.intensity = smallLightIntensity;
			}
		}
	}
}
