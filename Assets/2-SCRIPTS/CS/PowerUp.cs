using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	private float innerAngle;
	
	public int pipeCounter;
	
	void Awake(){
		
		pipeCounter = TrackManager.singletonInstance.pipeCounter;
		
		innerAngle = Random.Range(0.0f, 359.0f);
		transform.RotateAround(transform.position - (transform.up * 0.9f), transform.forward, innerAngle);
		
	}

	//TODO : CRIAR um modelo para isto
	
	void Update (){
		
	}

	public bool newRingAdded(){

		if(pipeCounter + TrackManager.ringAmmount < TrackManager.singletonInstance.pipeCounter){
			
			Destroy(gameObject);

			return true;
			
		}

		return false;
		
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