using UnityEngine;
using System.Collections;

public class LabelTopScore : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.GetComponent<UILabel>().text = "score: " + PlayerPrefs.GetFloat("topscore").ToString();

	}
	
	// Update is called once per frame
	void Update () {


	}
}
