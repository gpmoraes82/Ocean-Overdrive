using UnityEngine;
using System.Collections;

public class ButtonTutorial : MonoBehaviour {

	public GameObject menuTutorial;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnClick() {

		NGUITools.SetActive(GameObject.Find("Panel - menuIntro"),false);
		NGUITools.SetActive(menuTutorial,true);

		// TrackBuilder.singletonInstance.roadRenderChangerStarter ();
	}

}
