using UnityEngine;
using System.Collections;

/// <summary>
/// Interface for anything able to take damage.
/// </summary>
public interface ITakeDamage {
	/// <summary>
	/// Takes the damage at specified point.
	/// </summary>
	/// <param name="amount">Amount.</param>
	/// <param name="hitPoint">Hit point.</param>
	void TakeDamage (int amount, Vector3 hitPoint);
}
