using UnityEngine;

namespace Enemy
{
	public class SoundPlayer : MonoBehaviour
	{
		[Range(0, 1)] public float volume = 1f;

		private AudioSource _audioSource;
		private Rigidbody _rb;
		private float _maxSpeed = 7f;

		private void Start()
		{
			_audioSource = GetComponent<AudioSource>();
			if(_audioSource == null) Debug.LogError("Enemy SoundPlayer: AudioSource is null");

			_rb = GetComponent<Rigidbody>();
			if(_rb == null) Debug.LogError("Enemy SoundPlayer: Rigidbody is null");
		}

		private void Update()
		{
			Vector3 velocity = _rb.velocity;
			float speed = velocity.magnitude;
			// Debug.Log(speed);
			_maxSpeed = Mathf.Max(speed, _maxSpeed);

			float fac = velocity.magnitude / _maxSpeed;

			_audioSource.volume = fac * volume;
			_audioSource.pitch = fac * 3;
		}
	}
}
