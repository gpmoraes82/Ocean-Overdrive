using UnityEngine;
using System.Collections;

public class LabelWave : MonoBehaviour {

	public bool positive;
	public float temp;

	// Use this for initialization

	void Awake () {
		
		positive = true;
		
	}

	void Start () {

		//Debug.Log(gameObject.GetComponent<UILabel>().effectDistance);
	
	}
	
	// Update is called once per frame
	void Update () {

		if(positive){
			temp += 1.5f * Time.deltaTime;
			gameObject.GetComponent<UILabel>().effectDistance = new Vector2(temp + 10, 7);
		} else {
			temp -= 1.5f * Time.deltaTime;
			gameObject.GetComponent<UILabel>().effectDistance = new Vector2(temp + 10, 7);
		}

		if(gameObject.GetComponent<UILabel>().effectDistance.x >= 13){
			positive = false;
		}

		if(gameObject.GetComponent<UILabel>().effectDistance.x <= 7){
			positive = true;
		}
	}
}
