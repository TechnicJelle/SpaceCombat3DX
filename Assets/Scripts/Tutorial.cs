using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public List<GameObject> tutorialObjects;
	public List<float> tutorialTimes;

	private void Start()
	{
		if(tutorialObjects.Count != tutorialTimes.Count) Debug.LogError("Tutorial objects and times must have the same amount of elements!");

		foreach (GameObject tutorialObject in tutorialObjects)
		{
			tutorialObject.SetActive(false);
		}
	}

	private void Update()
	{
		//show only the latest tutorial object
		for (int i = 0; i < tutorialObjects.Count; i++)
		{
			if (Time.timeSinceLevelLoad > tutorialTimes[i])
			{
				for (int j = 0; j < tutorialObjects.Count; j++)
				{
					tutorialObjects[j].SetActive(j == i);
				}
			}
		}
	}
}
