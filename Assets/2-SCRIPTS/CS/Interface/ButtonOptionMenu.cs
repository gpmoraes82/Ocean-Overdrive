using UnityEngine;
using System.Collections;

public class ButtonOptionMenu : MonoBehaviour {
	
	public GameObject irPara;
	
	void OnClick(){		

		NGUITools.SetActive(GameObject.Find("Panel - Option"),false);
		NGUITools.SetActive(irPara,true);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
