using Player;
using UnityEngine;

public class EffectController : MonoBehaviour
{

	// === THRUSTERS === //
	public ParticleSystem thrusters;
	public float thrusterThreshold = 0.1f;
	private ParticleSystem.EmissionModule _thrustersEmission;
	private float _thrustersEmissionRateOverTimeMultiplier;

	// === SPEED LINES === //
	public ParticleSystem speedLines;
	public float speedLinesThreshold = 0.4f;
	private ParticleSystem.EmissionModule _speedLinesEmission;
	private float _speedLinesEmissionRateOverTimeMultiplier;

	private Rigidbody _rigidbody;

	// Start is called before the first frame update
	private void Start()
	{
		if (thrusters == null) Debug.LogError("Thrusters not assigned");
		_thrustersEmission = thrusters.emission;
		_thrustersEmissionRateOverTimeMultiplier = _thrustersEmission.rateOverTimeMultiplier;

		if (speedLines == null) Debug.LogError("SpeedLines not assigned");
		_speedLinesEmission = speedLines.emission;
		_speedLinesEmissionRateOverTimeMultiplier = _speedLinesEmission.rateOverTimeMultiplier;

		Debug.Log($"ThrustersEmissionOverTimeMultiplier: {_thrustersEmissionRateOverTimeMultiplier}" + " " +
		          $"SpeedLinesEmissionOverTimeMultiplier: {_speedLinesEmissionRateOverTimeMultiplier}");

		_rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	private void Update()
	{
		Vector3 velocity = _rigidbody.velocity;
		float fac = velocity.magnitude / PlayerMovement.MaxSpeed;
		float thrustersEmissionRateOverTimeMultiplier = (fac < thrusterThreshold ? 0 : fac) *  _thrustersEmissionRateOverTimeMultiplier;
		_thrustersEmission.rateOverTimeMultiplier = thrustersEmissionRateOverTimeMultiplier;
		float speedLinesEmissionRateOverTimeMultiplier = (fac < speedLinesThreshold ? 0 : fac) * _speedLinesEmissionRateOverTimeMultiplier;
		_speedLinesEmission.rateOverTimeMultiplier = speedLinesEmissionRateOverTimeMultiplier;

		Debug.Log($"ThrustersEmissionOverTimeMultiplier: {thrustersEmissionRateOverTimeMultiplier}" + " " +
		          $"SpeedLinesEmissionOverTimeMultiplier: {speedLinesEmissionRateOverTimeMultiplier}");
	}
}
