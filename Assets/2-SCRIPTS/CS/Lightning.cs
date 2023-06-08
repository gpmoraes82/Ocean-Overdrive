using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.RotateAround(transform.position, gameObject.transform.up, 60.0f * Time.deltaTime);
	
	}
}
