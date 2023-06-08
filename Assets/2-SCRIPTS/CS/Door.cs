using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour {
	
	private float innerAngle;

	public List<GameObject> locks;
	
	public int pipeCounter;

	public int openLocks;

	public int preOpenedLocks;

	public List<ParticleSystem> effects;

	// Use this for initialization
	void Start () {
		openLocks = 0;

//		for (int i = 0; i < gameObject.transform.childCount; i++)
//		{
//			locks.Add(gameObject.transform.GetChild(i).gameObject);
//		}

		for (int l = 0; l < preOpenedLocks; l++)
		{
			OpenNextLock();
		}
	}
	
	void Awake(){
		gameObject.transform.position = TrackBuilder.singletonInstance.transform.position;
		pipeCounter = TrackManager.singletonInstance.pipeCounter;
		innerAngle = Random.Range(0.0f, 359.0f);
		transform.RotateAround(transform.position, transform.forward, innerAngle);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OpenNextLock();
		}
	}

	public virtual void OpenNextLock()
	{

	}

	public bool newRingAdded()
	{
		if(pipeCounter + TrackManager.ringAmmount < TrackManager.singletonInstance.pipeCounter)
		{
			Destroy(gameObject);
			
			return true;
		}
		return false;
	}

	public void DestroyLock(int index)
	{
		//Debug.Log("Index: " + index.ToString());
		//locks.RemoveAt(0);

		//Destroy(locks[0]);

		if (locks.Count == (int)index)
		{
			ImmovableObjectManager.singletonInstance.removeDoor(this);

			Destroy(gameObject);
		}
	}

	public void animateFinishMatch()
	{
		for (int l = 0; l < locks.Count; l++)
		{
			locks[l].GetComponent<Lock>().animateFinishMatch();
		}

		iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero,
		                                       "time", 1,
		                                       "oncomplete", "destroySelf",
		                                       "oncompletetarget", gameObject,
		                                       "easetype", iTween.EaseType.easeInOutQuad));
	}
	
	public void destroySelf()
	{
		Debug.Log ("PORTA");
		Destroy(gameObject);
	}
}
