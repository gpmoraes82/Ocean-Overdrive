using UnityEngine;
using System.Collections;

public class Bullet: MonoBehaviour {

	public Vector3 veloBullet;

	public Transform alvo;

	void Start () {


	}
	
	void Update () {

		if(alvo){

			transform.Translate(veloBullet * Time.deltaTime);
			transform.LookAt(alvo);
		}

	}


}

