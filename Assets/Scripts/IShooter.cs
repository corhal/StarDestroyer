using UnityEngine;
using System.Collections;

/// <summary>
/// Interface for anything that is able to shoot.
/// </summary>
public interface IShooter {

	/// <summary>
	/// Toggles shooting on or off.
	/// </summary>
	/// <param name="shouldShoot">If set to <c>true</c> should start shooting.</param>
	void ToggleShooting (bool shouldShoot);

	/// <summary>
	/// Determines whether this instance is shooting.
	/// </summary>
	/// <returns><c>true</c> if this instance is shooting; otherwise, <c>false</c>.</returns>
	bool IsShooting ();
}
