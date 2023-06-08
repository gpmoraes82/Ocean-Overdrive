using UnityEngine;
using System.Collections;

public class PlayerBulletCollider: MonoBehaviour {

	public TravelerPlayerBullet traveler;

	void Start () {
	}
	
	void Update () {
	}
	
	public void OnTriggerEnter(Collider bonk){
		
		//Debug.Log(bonk.gameObject.name);

		if(bonk.gameObject.name == "Cylinder"){

			Debug.Log(bonk.gameObject.name);

			TravelerManager.singletonInstance.removePlayerBullet(traveler);

			TravelerManager.singletonInstance.removeTravelerEnemy(
				bonk.gameObject.GetComponentInParent<TravelerEnemy>());
			
			Destroy(traveler.gameObject);

			Destroy(bonk.gameObject.transform.parent.gameObject);
			//Destroy(bonk.gameObject);

			//GameObject.Instantiate(effectExplosion, transform.position, transform.rotation);
		}
	}
}