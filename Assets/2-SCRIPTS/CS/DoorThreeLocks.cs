using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorThreeLocks : Door {

	public override void OpenNextLock()
	{
		//Debug.Log("Lock: " + openLocks.ToString());
		if (locks.Count > openLocks)
		{
			iTween.MoveBy(locks[openLocks], iTween.Hash("amount", Vector3.up * 2,
			                                            "time", 3.0f / GenericTraveler.genericSpeed,//1,
			                                            "oncomplete", "DestroyLock",
			                                            "oncompleteparams", openLocks,
			                                            "oncompletetarget", gameObject,
			                                            "easetype", iTween.EaseType.easeInOutQuad));

			effects[openLocks].Play();

			openLocks++;
		}
	}
}
