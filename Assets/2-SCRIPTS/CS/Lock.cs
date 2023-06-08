using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider bonk)
	{
		//Debug.Log("Bonk: " + bonk.gameObject.name);
		if(bonk.gameObject.name == "ColliderPlayer")
		{
			TrackManager.singletonInstance.finishMatch();
		}
	}

	public void animateFinishMatch()
	{
		gameObject.GetComponent<Collider> ().enabled = false;

		iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero,
		                                       "time", 0.5f,
		                                       "easetype", iTween.EaseType.easeInOutQuad));
	}

}
