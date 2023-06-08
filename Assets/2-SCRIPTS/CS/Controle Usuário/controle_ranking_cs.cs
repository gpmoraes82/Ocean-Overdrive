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
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
public class controle_ranking_cs : MonoBehaviour 
{
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public struct registro_ranking
	{
		public string cod_usuario;
		public string nome_usuario;		
        public string topscore;
        public string topdistance;
        public string exibir;				
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public List<registro_ranking> ranking_VL;	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------		
	string php_path_VL = "http://www.tfhstudios.com/php_ocd_v1001/";
	
	int nro_recordes_VL =0;
	
	[SerializeField]
	public GameObject Text_linha_prefab;
	public GameObject Text_linha_nova_prefab;
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	void Start () 
	{
        string topscore = "0";
        string topdistance = "0";
        string id_usuario ="";
		string nome_usuario="";
        int plataforma = 0;


        ranking_VL = new List<registro_ranking>(); 
		nro_recordes_VL=0;

		id_usuario = PlayerPrefs.GetString("id_usuario");
		nome_usuario = PlayerPrefs.GetString("nome_usuario");
        topscore = PlayerPrefs.GetFloat("topscore").ToString();
        topdistance = PlayerPrefs.GetFloat("topdistance").ToString();

        //--- ANDROID
        if (Application.platform == RuntimePlatform.Android) 
		{	
			plataforma=0;
		}
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer) 
		{
			plataforma=1;
		}	
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.IPhonePlayer) 
		{
			plataforma=2;
		}

		Debug.Log (id_usuario);
		Debug.Log (nome_usuario);
		Debug.Log (topscore);
		Debug.Log (topdistance);
		Debug.Log (plataforma);


        //StartCoroutine(CARREGA_versao_sistema());

		StartCoroutine(CARREGA_grava_record(id_usuario, nome_usuario, topscore, topdistance, plataforma.ToString()));

    }
    
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------	

    IEnumerator CARREGA_versao_sistema()
    {
        string url = php_path_VL + "busca_versao_sistema.php";
        WWW www = new WWW(url);
        yield return www;

        if (www.error == null)
        {
            Debug.Log(www.text);
        }
        else
        {
            //mostra_mensagem("Sem conexão com a internet!\nCódigo Erro: 0001\nRaw Message Error.:" + www.error, 1);
        }
    }

	IEnumerator CARREGA_grava_record(string id_usuario, string nome_usuario, string topscore, string topdistance, string plaforma)
	{
		string url = php_path_VL + "grava_record.php";

        Debug.Log(url);

		WWWForm form = new WWWForm();
		form.AddField("id_usuario", id_usuario.ToString());
		form.AddField("nome_usuario", nome_usuario.ToString());
		form.AddField("topscore", topscore.ToString());
		form.AddField("topdistance", topdistance.ToString());
		form.AddField("plaforma", plaforma.ToString());

        //---- WWWWWWWWWWWWWW
        WWW www_record = new WWW(url, form); //---- Quando passa o form no php pega por POST
		yield return www_record;



		if (www_record.error == null)
		{

			Debug.Log("Gravei Record: " + www_record.text);
            /*
			if(volta == 1)
			{	
			  SceneManager.LoadScene("main");
			}	
			else
			{
				StartCoroutine(CARREGA_posicoes_ranking());
			}
            */
			StartCoroutine(CARREGA_posicoes_ranking());
        }
		else
		{
			Debug.Log("Sem conexão com a internet!\nCódigo Erro: 0001\n");
            // mostra_mensagem("Sem conexão com a internet!\nCódigo Erro: 0001\n" + www.error);
            Debug.Log(www_record.error);
            Debug.Log(www_record.text);

        }
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator CARREGA_posicoes_ranking()
	{
        string url = php_path_VL + "posicoes_ranking.php";
		WWW www_rank = new WWW(url);
		yield return www_rank;
		
		if (www_rank.error == null)
		{
			byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(www_rank.text);
            string str_converted = System.Text.Encoding.UTF8.GetString(utf8Bytes);
			Debug.Log("Busquei Posições Ranking");
  		    processa_rank(str_converted);
		}
		else
		{
			Debug.Log("Sem conexão com a internet!\nCódigo Erro: 0002\n");
			//mostra_mensagem("Sem conexão com a internet!\nCódigo Erro: 0001\n" + www.error);
		}		
	} 
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public void processa_rank(string str_leitura_file) 
	{
		int z=0;
		int i=0;
		int ct_linha=0;
		int espacamento = -102;		
		
		string[] ArrayDeFields = str_leitura_file.Split(new char[] { '\n' , '#' }, System.StringSplitOptions.RemoveEmptyEntries);

        Debug.Log(str_leitura_file);

		nro_recordes_VL = 0;
		for(z=0; z < ArrayDeFields.Length-1; z++)
		{
			registro_ranking temp_registro_ranking = new registro_ranking(); 

			temp_registro_ranking.cod_usuario = ArrayDeFields[z]; 
			temp_registro_ranking.nome_usuario = ArrayDeFields[z+1];
			//temp_registro_ranking.pontuacao = ArrayDeFields[z+2];
            temp_registro_ranking.topscore = ArrayDeFields[z + 2];
            temp_registro_ranking.topdistance = ArrayDeFields[z + 3];
            temp_registro_ranking.exibir = ArrayDeFields[z+4];			
			
			ranking_VL.Add(temp_registro_ranking);			
			z++;
			z++;
			z++;
            z++;
            nro_recordes_VL++;
		}
		
		for(i=0; i < ranking_VL.Count; i++)
		{		
			GameObject go_linha = Instantiate(Text_linha_nova_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			go_linha.name = "nome_"+i;
			
			go_linha.transform.SetParent(GameObject.Find("!_alinhamento_central").transform);
			go_linha.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			go_linha.transform.position = GameObject.Find("!_alinhamento_central").transform.position;
			go_linha.transform.rotation = GameObject.Find("!_alinhamento_central").transform.rotation;
			go_linha.transform.position = go_linha.transform.position + new Vector3(0, (espacamento*ct_linha)+1, 0);
			go_linha.GetComponentInChildren<Transform>().Find("Text_rank").GetComponent<Text>().text = (i+1).ToString();
			go_linha.GetComponentInChildren<Transform>().Find("Text_nome").GetComponent<Text>().text = ranking_VL[i].nome_usuario;
			//go_linha.GetComponentInChildren<Transform>().Find("Text_pontos").GetComponent<Text>().text = ranking_VL[i].pontuacao;
            go_linha.GetComponentInChildren<Transform>().Find("Text_pontos").GetComponent<Text>().text = ranking_VL[i].topscore;
            go_linha.GetComponentInChildren<Transform>().Find("Text_distancia").GetComponent<Text>().text = ranking_VL[i].topdistance;
            ct_linha++;			
		}		
		Debug.Log("fim do processamento do ranking!");
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
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

    public void buttonVoltar() {

        GameObject[] goArray = SceneManager.GetSceneByName("gameplay_2try").GetRootGameObjects();

        for(int i=0; i < goArray.Length; i++)
        {
            GameObject rootGo = goArray[i];
            //Debug.Log(rootGo + "-" + i);
			if(rootGo.name == "TrackManager"){
				NGUITools.SetActive(rootGo.GetComponent<TrackManager>().menuIntro, true);
			}

        }

//        if (goArray.Length > 0)
//        {
//            GameObject rootGo = goArray[7];
//            NGUITools.SetActive(rootGo.GetComponent<TrackManager>().menuIntro, true);
//        }

        SceneManager.UnloadSceneAsync("ranking");

    }

	public void buttonVoltarMenuScore() {
		
		GameObject[] goArray = SceneManager.GetSceneByName("gameplay_2try").GetRootGameObjects();
		
		for(int i=0; i < goArray.Length; i++)
		{
			GameObject rootGo = goArray[i];
			//Debug.Log(rootGo + "-" + i);

			if(rootGo.name == "TrackManager"){
				NGUITools.SetActive(rootGo.GetComponent<TrackManager>().menuScore, true);
			}
		}
		

		SceneManager.UnloadSceneAsync("rankingMenuScore");
		
	}
	
}
