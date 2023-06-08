using UnityEngine;
using System.Collections;

public class Tiro : MonoBehaviour {

	public GameObject effectExplosion;

	void Start () {
		
		
	}
	
	void Update () {
		
	}
	
	public void OnTriggerEnter(Collider bonk){
		
		//Debug.Log(bonk.gameObject.name);

		if(bonk.gameObject.name == "ObstacleCollider"){
			Destroy(gameObject);
			Destroy(bonk.gameObject);

			GameObject.Instantiate(effectExplosion, transform.position, transform.rotation);
		}
	}
}