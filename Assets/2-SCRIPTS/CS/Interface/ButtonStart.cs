using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ButtonStart : MonoBehaviour {

	public List<GameObject> Menus = new List<GameObject>();
	public GameObject menuInGame;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick() {

//		foreach(GameObject go in Menus){
//			NGUITools.SetActive(go, false);
//		}
        
        
		NGUITools.SetActive(GameObject.Find("Panel - menuIntro"),false);
		NGUITools.SetActive(menuInGame,true);

        
        GameObject.Find("CameraTarget").GetComponent<AudioSource>().Play();
        TrackManager.singletonInstance.startMatch();

        // TrackBuilder.singletonInstance.roadRenderChangerStarter ();

    }

}
