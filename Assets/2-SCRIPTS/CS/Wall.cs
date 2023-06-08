using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	
	private float innerAngle;
	
	public int pipeCounter;

	private bool animationStarted;

	void Awake(){
		pipeCounter = TrackManager.singletonInstance.pipeCounter;
		
		innerAngle = Random.Range(0.0f, 359.0f);

		Obstacle obstacleAtSamePipeCounter = ImmovableObjectManager.singletonInstance.GetObstacleAtPipeCounter (pipeCounter);
		if (obstacleAtSamePipeCounter != null) {
			innerAngle = obstacleAtSamePipeCounter.GetInnerAngle() + Random.Range(90.0f, 270.0f);
		}
		else{
			innerAngle = Random.Range(0.0f, 359.0f);
		}

//		Wall wall2AtSamePipeCounter = ImmovableObjectManager.singletonInstance.GetWall2AtPipeCounter (pipeCounter);
//		if (wall2AtSamePipeCounter != null) {
//			innerAngle = wall2AtSamePipeCounter.GetInnerAngle() + Random.Range(90.0f, 270.0f);
//		}
//		else{
//			innerAngle = Random.Range(0.0f, 359.0f);
//		}

		transform.RotateAround(transform.position - (transform.up * 0.9f), transform.forward, innerAngle);

		animationStarted = false;

		transform.position += transform.up * 1.0f;
	}
	
	
	void Update (){

	}

	public bool newRingAdded(){

		if(pipeCounter + TrackManager.ringAmmount < TrackManager.singletonInstance.pipeCounter){
			
			Destroy(gameObject);

			return true;
		}

		// 7 aneis antes do fim do tunel
		if (!animationStarted && (pipeCounter + TrackManager.ringAmmount - Random.Range(7, 12) < TrackManager.singletonInstance.pipeCounter)) {
			animateInwards();
		}

		return false;
	}

	private void animateInwards() {
		animationStarted = true;
		//Debug.Log ("RAWR");

		float randomDuration = Random.Range (1.5f, 2.0f);

		iTween.MoveBy(gameObject, iTween.Hash("amount", Vector3.up * -1.1f,
		                                      "time", (3.0f / GenericTraveler.genericSpeed) * randomDuration,//1.5f,
	                                          "easetype", iTween.EaseType.easeInOutQuad));
	}
	
	public void OnTriggerEnter(Collider bonk){
		
		//Debug.Log(bonk.gameObject.name);
		if(bonk.gameObject.name == "ColliderPlayer"){
			//Debug.Log(bonk.gameObject.name);
			TrackManager.singletonInstance.finishMatch();
			//gameObject.collider.enabled = false;
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
		Debug.Log ("PAREDE");
		Destroy(gameObject);
	}

	public float GetInnerAngle(){
		return innerAngle;
	}

}