using UnityEngine;
using System.Collections;

public class MenuLeftVisor : MonoBehaviour {
	
	public float x;
	public float y;
	public float z;
	
	public GameObject LabelX;
	
	void Start () {
		ligaVisor();
	}
	
	void Update () {
		
	}
	
	
	void ligaVisor(){
		
		transform.localScale = Vector3.one;
			
		//exemplo iTween HASH
		/*	
			iTween.ScaleTo(gameObject, iTween.Hash(
				"x", x,							// cordenada x
				"y", y,							// cordenada y
				"z", z,							// cordenada z
				"easyType", "easyInOutExpo",	// tipo de tweens, animacoes
				"time", 0.1,						// tempo de animacao
				"onComplete", "ligaVisor",		// metodo adicinal, utilizado no fim do itween
				"onCompleteTarget", gameObject	// indica o gamebject que contem um script, que contem o metodo a ser chamado
			));
			
			cont++;
		*/	
		iTween.ScaleTo(gameObject, iTween.Hash(
			"x", x,
			"y", y,
			"z", z,
			"easyType", "easyInOutExpo",
			"time", 4.0f,
			"onComplete", "ligaLabel",
			"onCompleteTarget", gameObject
		));
	}
	
	void ligaLabel(){
		LabelX.GetComponent<TweenAlpha>().enabled = true;
	}
}
