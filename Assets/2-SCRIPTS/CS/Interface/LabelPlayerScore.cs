using UnityEngine;
using System.Collections;

public class LabelPlayerScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<UILabel>().text = TravelerPlayer.singletonInstance.getPlayerScore().ToString();
	}
}
