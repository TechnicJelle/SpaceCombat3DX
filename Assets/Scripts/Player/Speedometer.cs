using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
	public Slider slider;

	private Rigidbody _rb;

	// Start is called before the first frame update
	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	private void Update()
	{
		slider.value = _rb.velocity.magnitude / PlayerMovement.MaxSpeed;
	}
}
