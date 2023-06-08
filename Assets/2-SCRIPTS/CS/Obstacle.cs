using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	private float innerAngle;

	public int pipeCounter;

	public GameObject effectPick;

	void Awake(){

		pipeCounter = TrackManager.singletonInstance.pipeCounter;

		innerAngle = Random.Range(0.0f, 359.0f);
		transform.RotateAround(transform.position - (transform.up * 0.9f), transform.forward, innerAngle);

	}


	void Update (){

		/*
		if(Vector3.Distance(transform.position,TravelerPlayer.singletonInstance.transform.position) < 2){

			TravelerPlayer.singletonInstance.shootTarget(transform);
		}
		*/

	}

	public float GetInnerAngle(){
		return innerAngle;
	}

	public bool newRingAdded(){
		
		if(pipeCounter + TrackManager.ringAmmount < TrackManager.singletonInstance.pipeCounter){

			Destroy(gameObject);

			return true;

		}

		return false;
		
	}


	//public void OnCollisionEnter(Collision bonk){
	public void OnTriggerEnter(Collider bonk){

		//Debug.Log(bonk.gameObject.name);

		if(bonk.gameObject.name == "ColliderPlayer"){
			//TravelerPlayer.singletonInstance.shootTarget(gameObject.transform);
			//gameObject.GetComponent<Collider>().enabled = false;
			//Debug.Log("PONTO");

			TravelerPlayer.singletonInstance.incrementPlayerScore(1);

			ImmovableObjectManager.singletonInstance.OpenLockNextDoor(this);

			GameObject explosion = (GameObject)GameObject.Instantiate(effectPick, gameObject.transform.position, gameObject.transform.rotation);
            //explosion.transform.parent = TravelerPlayer.singletonInstance.transform;

            TravelerPlayer.singletonInstance.GetComponent<AudioSource>().Play();
		}
	}

	public void animateFinishMatch()
	{
		gameObject.GetComponent<Collider> ().enabled = false;

		iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero,
		                                       "time", 1,
		                                       "oncomplete", "destroySelf",
		                                       "oncompletetarget", gameObject,
		                                       "easetype", iTween.EaseType.easeInOutQuad));
	}
	
	public void destroySelf()
	{
		Debug.Log ("OBSTACULO");
		Destroy(gameObject);
	}


}
