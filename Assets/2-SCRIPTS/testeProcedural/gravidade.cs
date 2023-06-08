using UnityEngine;
using System.Collections;

public class gravidade : MonoBehaviour {
	
	public WheelCollider FL;
	public WheelCollider FR;
	
	// Use this for initialization
	void Start () {
		//rigidbody.centerOfMass = new Vector3(0.0f , 0.0f, -0.025f);
		
		//rigidbody.constantForce.relativeForce = new Vector3(0.0f, -9.8f, 0.5f);
		//rigidbody.inertiaTensor = new Vector3(1, 1, 1);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		GetComponent<Rigidbody>().GetComponent<ConstantForce>().relativeForce = new Vector3(0.0f, -100.8f, 1.5f) * Time.deltaTime;
		
		
		print(GetComponent<Rigidbody>().velocity.z);
		
		if(gameObject.GetComponent<Rigidbody>().velocity.z < 3.0f){
			if (Input.GetAxis("Vertical") != 0){
				GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * 5.0f) * Time.deltaTime);
			}
			else{
				GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, -1.5f) * Time.deltaTime);
			}
		}
		
		

		FL.steerAngle = 5 * Input.GetAxis("Horizontal");
		FR.steerAngle = 5 * Input.GetAxis("Horizontal");
		
	}
}
