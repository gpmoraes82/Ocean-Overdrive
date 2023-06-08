using UnityEngine;
using System.Collections;

public class testeCurve : MonoBehaviour {
	
	void Start () {
	}
	

	void Update () {
		
	}
	
	void OnClick() {
		
		if(TrackManager.singletonInstance.getTtunnelType() == TunnelType.CURVE)
			TrackManager.singletonInstance.setTtunnelType(TunnelType.STRAIGHT);
		else
			TrackManager.singletonInstance.setTtunnelType(TunnelType.CURVE);
	}
}
