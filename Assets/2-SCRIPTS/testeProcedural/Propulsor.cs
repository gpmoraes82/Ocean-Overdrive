using UnityEngine;
using System.Collections;

public class Propulsor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	// Update is called once per frame
	void LateUpdate () {
		//rigidbody.constantForce.relativeForce = new Vector3(0.0f, -80.8f, 1.5f) * Time.deltaTime;
		
		if (Input.GetAxis("Vertical") != 0){
			GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * 80.0f) * Time.deltaTime);
		}
		else{
			GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, -0.5f) * Time.deltaTime);
		}
		
		//MUST REMOVE THIS CRAP L8R
		transform.RotateAround(transform.position, transform.forward, 40.0f * Time.deltaTime * Input.GetAxis("Horizontal"));
		
		GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Input.GetAxis("Horizontal") * 10, 0, 0) * Time.deltaTime);
		
		if (Input.GetKeyDown(KeyCode.Space)){
			GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity * -50f);
		}
		
		if (Input.GetKeyDown(KeyCode.LeftControl)){
			transform.rotation = Quaternion.identity;
		}
	}
}
