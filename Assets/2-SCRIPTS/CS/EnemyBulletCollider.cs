using UnityEngine;
using System.Collections;

public class EnemyBulletCollider: MonoBehaviour {


	void Start () {
	}
	
	void Update () {
	}
	
	public void OnTriggerEnter(Collider bonk){
		
		//Debug.Log(bonk.gameObject.name);
		if(bonk.gameObject.name == "ColliderPlayer"){
			//Debug.Log(bonk.gameObject.name);
			TrackManager.singletonInstance.finishMatch();
			//gameObject.collider.enabled = false;
		}
	}
}