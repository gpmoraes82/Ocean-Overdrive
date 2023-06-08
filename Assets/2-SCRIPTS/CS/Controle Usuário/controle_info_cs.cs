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

public class controle_info_cs : MonoBehaviour 
{
	#if UNITY_WEBPLAYER
		public static string webplayerQuitURL = "http://www.ahoradofilme.com.br";
	#endif	

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------			
	void Start () 
	{
		GameObject.Find("Text_info").GetComponent<Text>().text = "Versão: " + PlayerPrefs.GetString("version") + "\n" +
																																		"Data Lançamento: " + PlayerPrefs.GetString("release_date") + "\n" +
																																		"Nome do Usuário: " + PlayerPrefs.GetString("nome_usuario") + "\n" +
																																		"Pontuação: " + PlayerPrefs.GetInt("pontuacao").ToString() + "\n" +
																																		"Nome do Dicionário: " + PlayerPrefs.GetString("nome_dicionario_local")  + "\n" +
																																		"Número de Palavras: 127.541" + "\n" +
																																		"Resolução Corrente: " + Screen.currentResolution.ToString() + "\n" +
																																		"Largura da Tela: " + Screen.width.ToString() + "\n" + 
																																		"Altura da Tela: " + Screen.height.ToString() + "\n" +
																																		"Plataforma: " + Application.platform.ToString() + "\n\n\n" +
																																		"Dica: Para sair do aplicatico clique no palavra ENIGMA bem no topo e no centro da tela." + "\n" +
																																		"Crédito Sons: freesfx.co.uk." + "\n" +
																																		"Crédito Dicionário: Novo Dicionário da Língua Portuguesa de Cândido de Figueiredo." + "\n";
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------			
	public void carrega_main()
	{
		SceneManager.LoadScene("main");		
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