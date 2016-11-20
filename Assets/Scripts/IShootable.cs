using UnityEngine;
using System.Collections;

/// <summary>
/// Interface for anything able to take damage.
/// </summary>
public interface IShootable {
	/// <summary>
	/// Takes the damage at specified point.
	/// </summary>
	/// <param name="amount">Amount.</param>
	/// <param name="hitPoint">Hit point.</param>
	void TakeDamage (int amount, Vector3 hitPoint);

	/// <summary>
	/// Determines whether this instance is alive.
	/// </summary>
	/// <returns><c>true</c> if this instance is alive; otherwise, <c>false</c>.</returns>
	bool IsAlive ();

	GameObject ShootableGameObject ();
}
