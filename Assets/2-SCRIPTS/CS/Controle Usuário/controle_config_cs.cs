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

public class controle_config_cs : MonoBehaviour 
{
#if UNITY_WEBPLAYER
		public static string webplayerQuitURL = "http://www.tfhstudios.com";
#endif

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------			
    void Start () 
	{
		GameObject.Find("Text_playerprefs").GetComponent<Text>().text = 
            "version: " + PlayerPrefs.GetString("version") + "\n" +
		    "release_date: " + PlayerPrefs.GetString("release_date") + "\n" +
		    "id_usuario: " + PlayerPrefs.GetString("id_usuario") +	"\n" +
		    "nome_usuario: " + PlayerPrefs.GetString("nome_usuario") + "\n" +
		    "pontuacao: " + PlayerPrefs.GetInt("pontuacao").ToString() + "\n\n" +

            "score: "+ PlayerPrefs.GetFloat("topscore").ToString() + "\n" +
            "distancia: " + PlayerPrefs.GetFloat("topdistance").ToString() + "\n\n" +

            "cod_usuario: " + PlayerPrefs.GetInt("cod_usuario").ToString() + "\n" +
            "som: " + PlayerPrefs.GetInt("som").ToString() + "\n" +																																		
		    "dica: " + PlayerPrefs.GetInt("dica").ToString() + "\n" +																																																																				
		    "senha: " + PlayerPrefs.GetString("senha") + "\n" +
		    "nome_dicionario_local: " + PlayerPrefs.GetString("nome_dicionario_local") + "\n" +
		    "nome_dicionario_internet: " + PlayerPrefs.GetString("nome_dicionario_internet") + "\n" +																																		
		    "path_dicionario_local: " + PlayerPrefs.GetString("path_dicionario_local") + "\n" +
		    "path_dicionario_internet: " + PlayerPrefs.GetString("path_dicionario_internet") + "\n" +
		    "limpei_playerprefs: " + PlayerPrefs.GetInt("limpei_playerprefs").ToString() + "\n";
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------			
	public void delete_all_PlayerPrefs()
	{
		GameObject.Find("Text_playerprefs").GetComponent<Text>().text = "DELETEI TUDO";
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();	
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------			
	public void carrega_main()
	{
		SceneManager.LoadScene("main");
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void carrega_gameplayScene()
	{
		SceneManager.LoadScene("gameplay_2try");
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------				
	public void sair_aplicativo() 
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
			Application.OpenURL(webplayerQuitURL);
		#else
			Application.Quit();
		#endif		
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------			
}