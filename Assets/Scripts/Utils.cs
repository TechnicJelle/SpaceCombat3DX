using System;
using UnityEngine;

public class Utils : MonoBehaviour
{
	public static double Clamp(double x, double min, double max)
	{
		return Math.Max(min, Math.Min(max, x));
	}

	public static double Map(double x, double inMin, double inMax, double outMin, double outMax, bool clamp = false)
	{
		if (clamp) x = Clamp(x, inMin, inMax);
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
