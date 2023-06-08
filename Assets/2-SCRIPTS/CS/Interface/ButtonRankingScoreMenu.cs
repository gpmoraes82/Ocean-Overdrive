using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Authentication;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ButtonRankingScoreMenu : MonoBehaviour {
	
	//	public GameObject menuRanking;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnClick() {

		TrackManager.singletonInstance.btnRankingId = 2;

        if (String.Compare(PlayerPrefs.GetString ("id_usuario"), "") > 0) {
        //if (String.IsNullOrEmpty(PlayerPrefs.GetString("id_usuario"))) { 
            NGUITools.SetActive (TrackManager.singletonInstance.menuScore, false);
			SceneManager.LoadScene ("rankingMenuScore", LoadSceneMode.Additive);
			
		} else {
			NGUITools.SetActive (TrackManager.singletonInstance.menuScore, false);
			TrackManager.singletonInstance.canvasFormRanking.SetActive(true);
		}
	}
	
}
