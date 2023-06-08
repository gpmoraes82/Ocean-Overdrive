using UnityEngine;
using System.Collections;

public class TravelerPlayerBullet: GenericTraveler {

	private float speed = 0.1f;

	void Start () {
		//TODO destruir as balas 
	}
	
	void Update () {

		if(DoctorWho(speed)){
			
			pointIndex++;
			
//			if(pointIndex <= TravelerPlayer.singletonInstance.pointIndex + 2){
//				
//				speed = 1; // pode ser controlado
//				
//			}
			movementDirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - transform.position;
		}
	}
	
	public void InitBullet(int pointIndex, float speed) {
		
		//transform.position = TrackBuilder.singletonInstance.points[pointIndex];
		
		base.InitGeneric(pointIndex);
		
		//Debug.Log(pointIndex);

		this.speed = speed;
	}
	
	public bool newRingAdded(){

		pointIndex--;

//		if (gameObject == null)
//		{
//			return true;	
//		}

		if(pointIndex > TrackBuilder.singletonInstance.points.Count - 8){
			//Debug.Log (pointIndex);
			Destroy(gameObject);

			return true;
		}

		return false;
	}

}
