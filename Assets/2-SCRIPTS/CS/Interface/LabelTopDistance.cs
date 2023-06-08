using UnityEngine;
using System.Collections;

public class LabelTopDistance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<UILabel>().text = "distance: " + PlayerPrefs.GetFloat("topdistance").ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
