using UnityEngine;

public class PortalManager : MonoBehaviour
{
	public string scene;
	public float time;

	private Vector3 _localScale;
	private void Start()
	{
		Transform trans = transform;
		_localScale = trans.localScale;
		trans.localScale = Vector3.zero;
	}

	private void Update()
	{
		Debug.Log(Time.timeSinceLevelLoad);
		//after ten seconds of waiting
		if (Time.timeSinceLevelLoad > time)
		{
			//scale up the portal
			transform.localScale = _localScale;
		}
	}
}
