using UnityEngine;
using System.Collections;

public class ButtonMainMenu : MonoBehaviour {

	public GameObject irPara;
	
	void OnClick(){
		
		NGUITools.SetActive(GameObject.Find("Panel - Main"),false);
		NGUITools.SetActive(irPara,true);
	}	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
