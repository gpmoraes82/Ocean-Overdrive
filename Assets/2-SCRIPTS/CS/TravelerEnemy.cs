using UnityEngine;
using System.Collections;

public class TravelerEnemy : GenericTraveler {

	private float speed = 0.5f;

	private bool bang = true;
	
	private int rotationDirection = 0;
	
	private float rotationSpeed = 0.0f;

	void Start () {
		rotationDirection = Random.Range (0, 200) > 100 ? -1 : 1;
		rotationSpeed = Random.Range (0.2f, 1.8f);
	}
	
	void Update () {

		//Beattles(rotationDirection, 90 * speed * rotationSpeed);

		if(DoctorWho(speed)){

			pointIndex++;

			if(pointIndex <= TravelerPlayer.singletonInstance.pointIndex + 2){

				speed = 1; // pode ser controlado

				if(bang == true){
					TravelerManager.singletonInstance.spawnBullet(transform.position,transform.rotation, pointIndex, speed / 10.0f);
					bang = false;
					Invoke("triggerShot", 2.0f);
				}
								
			}
			movementDirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - transform.position;
		}

	}

	public void InitEnemy(int pointIndex) {
		
		transform.position = TrackBuilder.singletonInstance.points[pointIndex];
		
		base.InitGeneric(pointIndex);

		//TravelerManager.singletonInstance.spawnBullet();

		//Debug.Log(pointIndex);
		
	}

	public void newRingAdded(){
		pointIndex--;
	}

	public void triggerShot(){
		bang = true;
	}

	public void recalculateTargetInnerAngle()
	{

	}
	
}
