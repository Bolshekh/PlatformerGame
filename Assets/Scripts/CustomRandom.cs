
using UnityEngine;

public static class Random
{
	/// <summary>
	/// Is base chance succesful?
	/// </summary>
	/// <param name="BaseChance">Chance of success from 0% to 100%</param>
	/// <returns>true if Succesful</returns>
	public static bool FullRandom(float BaseChance)
	{
		var roll = UnityEngine.Random.Range(0f, 100f);

		return roll <= BaseChance;
	}
}