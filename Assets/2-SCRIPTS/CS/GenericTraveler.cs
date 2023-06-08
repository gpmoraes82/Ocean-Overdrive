using UnityEngine;
using System.Collections;

public class GenericTraveler : MonoBehaviour {

	public float magic_number = 5.0f;
	public float innerAngle = 0.0f;
	public float angleToExit;
	
	public Vector3 movementDirection;
	public Vector3 exitDirection;
	
	public Quaternion targetDirection;
	public Quaternion rotation;
	
	public int pointIndex;

	public static float genericSpeed = 1.8f;

	public void InitGeneric(int pointIndex){
		
		this.pointIndex = pointIndex;
		
		//transform.position = TrackBuilder.singletonInstance.points[pointIndex];
		movementDirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - TrackBuilder.singletonInstance.points[pointIndex];
		
		exitDirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - transform.position;
		angleToExit = Vector3.Angle(movementDirection, exitDirection);
		
	}
	
	public void Beattles (int twist, float shout) { //Funcao que guia o objeto

		//twist -> eh o giro
		//shout -> intencidade do giro

		innerAngle += Time.deltaTime * (twist * shout);
		//transform.RotateAround(TrackBuilder.singletonInstance.points[pointIndex], transform.forward, Time.deltaTime * (twist * shout));
		transform.RotateAround(transform.position, transform.forward, Time.deltaTime * (twist * shout));
		
	}
	
	public bool DoctorWho (float speed) { // define direçao do traveler

		if (pointIndex < 0 || pointIndex > TrackBuilder.singletonInstance.points.Count - 3){
			Debug.Log("SUICIDE");
			Destroy(gameObject);
			return false;
		}

		bool temp = false;
		
		transform.position += (movementDirection.normalized * Time.deltaTime * speed * genericSpeed);
		
		//Debug.Log(movementDirection);
		
		exitDirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - transform.position;
		
		//angleToExit = Vector3.Angle(movementDirection, exitDirection);
		angleToExit = Vector3.Angle(exitDirection, movementDirection);

		if (angleToExit > 90){

			//targetDirection = Quaternion.LookRotation(TrackBuilder.singletonInstance.points[pointIndex + 2] - TrackBuilder.singletonInstance.points[pointIndex + 1], transform.up);
			
			//movementDirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - TrackBuilder.singletonInstance.points[pointIndex];
			
			if(innerAngle > 360.0f){
				innerAngle -= 360.0f;
			} else if(innerAngle < 0.0f){
				innerAngle += 360.0f;
			}
			
			temp = true;

			//readjustPositionOnTransition();

		}

//		if (gameObject.name.Equals("Enemy(Clone)")){
//			Vector3 badonkadonk = (movementDirection.normalized * Time.deltaTime * speed * genericSpeed);
//
//			//Debug.Log(badonkadonk.x.ToString() + badonkadonk.y.ToString() + badonkadonk.z.ToString());
//		}
		
		//rotation = Quaternion.LookRotation(TrackBuilder.singletonInstance.points[pointIndex + 1] - transform.position, transform.up);
		rotation = Quaternion.LookRotation(transform.position - TrackBuilder.singletonInstance.points[pointIndex], transform.up);
		
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * speed * genericSpeed); //revisar
		//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * magic_number * speed); //revisar

		//transform.rotation = rotation;
		
		//Debug.DrawLine(transform.position, TrackBuilder.singletonInstance.points[pointIndex + 1], Color.red);
		Debug.DrawLine(transform.position, transform.position + exitDirection, Color.red);
		Debug.DrawLine(transform.position, transform.position + movementDirection, Color.green);
		
		return temp;
	}

	public void readjustPositionOnTransition()
	{

		float proportion = Vector3.Distance(transform.position, TrackBuilder.singletonInstance.points[pointIndex + 1]);

		Debug.Log(proportion);

		Vector3 nextMovementdirection = TrackBuilder.singletonInstance.points[pointIndex + 1] - TrackBuilder.singletonInstance.points[pointIndex + 2];

		Debug.Log(nextMovementdirection);

		transform.position = TrackBuilder.singletonInstance.points[pointIndex + 1] + (nextMovementdirection * proportion);
	}

}
