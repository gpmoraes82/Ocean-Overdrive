using UnityEngine;
using System.Collections;

public class ControleGeral : MonoBehaviour {
	
	GameObject MainCamera;

	// Use this for initialization
	void Start () {
		
		MainCamera = GameObject.Find("Main Camera");
		
		if(MainCamera != null)
			Debug.Log("MORANGOS");
		else
			Debug.Log("ABACA X");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
