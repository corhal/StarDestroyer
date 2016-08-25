using UnityEngine;
using System.Collections;

public interface IShooter {

	void ToggleShooting (bool shouldShoot);

	bool IsShooting ();
}
