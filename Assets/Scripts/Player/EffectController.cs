using UnityEngine;

namespace Player
{
	public class EffectController : MonoBehaviour
	{

		// === THRUSTERS === //
		public ParticleSystem thrusters;
		public float thrusterThreshold = 0.1f;
		private float _thrustersEmissionRateOverTimeMultiplier;

		// === SPEED LINES === //
		public ParticleSystem speedLines;
		public float speedLinesThreshold = 0.4f;
		private float _speedLinesEmissionRateOverTimeMultiplier;

		private Rigidbody _rb;

		private void Start()
		{
			if (thrusters == null) Debug.LogError("Thrusters not assigned");
			ParticleSystem.EmissionModule thrustersEmission = thrusters.emission;
			_thrustersEmissionRateOverTimeMultiplier = thrustersEmission.rateOverTimeMultiplier;

			if (speedLines == null) Debug.LogError("SpeedLines not assigned");
			ParticleSystem.EmissionModule speedLinesEmission = speedLines.emission;
			_speedLinesEmissionRateOverTimeMultiplier = speedLinesEmission.rateOverTimeMultiplier;

			Debug.Log($"ThrustersEmissionOverTimeMultiplier: {_thrustersEmissionRateOverTimeMultiplier}" + " " +
			          $"SpeedLinesEmissionOverTimeMultiplier: {_speedLinesEmissionRateOverTimeMultiplier}");

			_rb = GetComponent<Rigidbody>();
			if(_rb == null) Debug.LogError("EffectController: Rigidbody not found");
		}

		private void Update()
		{
			Vector3 velocity = _rb.velocity;
			float fac = velocity.magnitude / PlayerMovement.MaxSpeed;

			float thrustersEmissionRateOverTimeMultiplier = (fac < thrusterThreshold ? 0 : fac) *  _thrustersEmissionRateOverTimeMultiplier;
			ParticleSystem.EmissionModule thrustersEmission = thrusters.emission;
			thrustersEmission.rateOverTimeMultiplier = thrustersEmissionRateOverTimeMultiplier;

			float speedLinesEmissionRateOverTimeMultiplier = (fac < speedLinesThreshold ? 0 : fac) * _speedLinesEmissionRateOverTimeMultiplier;
			ParticleSystem.EmissionModule speedLinesEmission = speedLines.emission;
			speedLinesEmission.rateOverTimeMultiplier = speedLinesEmissionRateOverTimeMultiplier;

			// Debug.Log($"ThrustersEmissionOverTimeMultiplier: {thrustersEmissionRateOverTimeMultiplier}" + " " +
			          // $"SpeedLinesEmissionOverTimeMultiplier: {speedLinesEmissionRateOverTimeMultiplier}");
		}
	}
}
