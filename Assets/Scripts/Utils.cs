using System;
using UnityEngine;

public class Utils : MonoBehaviour
{
	public static double Map(double x, double inMin, double inMax, double outMin, double outMax, bool clamp = false)
	{
		if (clamp) x = Math.Max(inMin, Math.Min(x, inMax));
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
