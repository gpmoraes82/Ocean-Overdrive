using UnityEngine;
using System.Collections;

public class ButtonBack : MonoBehaviour {

	public GameObject menuIntro;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnClick() {
		
		NGUITools.SetActive(GameObject.Find("Panel - menuTutorial"),false);
		NGUITools.SetActive(menuIntro,true);
		
		// TrackBuilder.singletonInstance.roadRenderChangerStarter ();
	}

}
