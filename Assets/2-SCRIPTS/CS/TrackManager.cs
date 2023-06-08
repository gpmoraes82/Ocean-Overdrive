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

public class TrackManager : MonoBehaviour {

	static public TrackManager singletonInstance;

	public GameObject Player;

	public GameObject menuScore;
	public GameObject menuInGame;
	public GameObject menuIntro;

	public GameObject canvasFormRanking;
	public int btnRankingId;

    public GameObject btnStart;

	public GameObject effectExplosion;

	static public short ringAmmount = 20; //inicia o tamanho da pista

	public bool inGame;

	public TravelerManager travelerManager;

	public ImmovableObjectManager mumia;

	public int pipeCounter;

    public AudioClip[] musics;
    public int randMusic;
    public int randMusicAtual;

    public string path_php_VL = "";

    public string path_dicionario_local_VL = "";
    public string path_dicionario_internet_VL = "";

    public string nome_dicionario_local_VL = "";
    public string nome_dicionario_internet_VL = "";

    public string id_Usuario_VL = "";
    public string NomeUsuario_VL = "";
    public string Senha_VL = "";
    int pontuacao_VL = 0;

    public int plataforma_VL = 0;

#if UNITY_WEBPLAYER
	public static string webplayerQuitURL = "http://www.tfhstudios.com";
#endif

    string str_info_VL = "";
    string largura_VL = "";
    string altura_VL = "";
    string current_resolution_VL = "";

    void Start () {

		singletonInstance = this;

		Player.GetComponent<TravelerPlayer>().InitPlayer(2);

		inGame = false;

        randMusicAtual = 0;

        limpa_playerprefs_uma_vez();
        str_info_VL = "0";
        largura_VL = "0";
        altura_VL = "0";
        current_resolution_VL = "0";

        debug_log("Iniciei em Start();");

        largura_VL = Screen.width.ToString();
        altura_VL = Screen.height.ToString();
        current_resolution_VL = Screen.currentResolution.ToString();

        path_php_VL = "http://www.tfhstudios.com/php_ocd_v1001/";

        //--- ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            plataforma_VL = 0;
        }
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            plataforma_VL = 1;
        }
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            plataforma_VL = 2;
        }

        PlayerPrefs.SetString("version", "1.0.0.1");
        PlayerPrefs.SetString("release_date", "03/05/2017");
        PlayerPrefs.Save();

        if (PlayerPrefs.GetString("nome_usuario") == "")
        {
            capta_nome_usuario();
        }
        else
        {
			if(GameObject.Find("form")){
            	GameObject.Find("form").active = false;
            	//GameObject.Find("form").GetComponent<CanvasRenderer>().
				StartCoroutine(CARREGA_versao_sistema());
			}
        }


    }

    // Update is called once per frame
    void Update () {

        //Debug.Log(GameObject.Find("Main Camera").GetComponent<AudioSource>().isPlaying);

        if (GameObject.Find("Main Camera").GetComponent<AudioSource>().isPlaying != true) {

            Debug.Log("entrei E ESCOLHI A MUSICA");

            randMusic = Random.Range(1, 4);
            if (randMusicAtual != randMusic) {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = musics[randMusic];
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
                randMusicAtual = randMusic;
            }
        }

	}

	public void setTtunnelType(TunnelType type){
		TrackBuilder.singletonInstance.tunnelType = type;
	}

	public TunnelType getTtunnelType(){
		return TrackBuilder.singletonInstance.tunnelType;
	}

	public void startMatch(){
		inGame = true;

		//TravelerPlayer.singletonInstance.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
		TravelerPlayer.singletonInstance.gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = true;
		TravelerPlayer.singletonInstance.setPlayerScore(0);
		TravelerPlayer.singletonInstance.setPlayerDistance(0);

		GenericTraveler.genericSpeed = 1.5f;

		TrackBuilder.singletonInstance.startMatch();

		mumia.startMatch();

        GameObject.Find("default").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("effect_rastro").GetComponent<Renderer>().enabled = true;
        GameObject.Find("effect_rastro2").GetComponent<Renderer>().enabled = true;

        GameObject.Find("Main Camera").GetComponent<AudioSource>().loop = false;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();


        randMusic = Random.Range(1, 4);
        if (randMusicAtual != randMusic)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = musics[randMusic];
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
            randMusicAtual = randMusic;
        }

    }

    public void finishMatch(){

		ImmovableObjectManager.singletonInstance.finishMatch();

		GameObject explosion = (GameObject)GameObject.Instantiate(effectExplosion, TravelerPlayer.singletonInstance.ColliderPlayer.transform.position, TravelerPlayer.singletonInstance.ColliderPlayer.transform.rotation);
		explosion.transform.parent = TravelerPlayer.singletonInstance.transform;

        GameObject.Find("AudioExplosion").GetComponent<AudioSource>().Play();

        GameObject.Find("default").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("effect_rastro").GetComponent<Renderer>().enabled = false;
        GameObject.Find("effect_rastro2").GetComponent<Renderer>().enabled = false;

        //TravelerPlayer.singletonInstance.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        TravelerPlayer.singletonInstance.gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;

		TrackBuilder.singletonInstance.tunnelType = TunnelType.STRAIGHT;

		NGUITools.SetActive(menuInGame,false);
		NGUITools.SetActive(menuScore,true);

		if(PlayerPrefs.GetFloat("topscore") < TravelerPlayer.singletonInstance.getPlayerScore()){
			PlayerPrefs.SetFloat("topscore", TravelerPlayer.singletonInstance.getPlayerScore());
			NGUITools.SetActive(GameObject.Find("lblNew1"),true);
		} else {
			NGUITools.SetActive(GameObject.Find("lblNew1"),false);
		}
		
		//PlayerPrefs.SetFloat("score", TravelerPlayer.singletonInstance.getPlayerScore());
		
		if(PlayerPrefs.GetFloat("topdistance") < TravelerPlayer.singletonInstance.getPlayerDistance()){
			PlayerPrefs.SetFloat("topdistance", TravelerPlayer.singletonInstance.getPlayerDistance());
			NGUITools.SetActive(GameObject.Find("lblNew2"),true);
		} else {
			NGUITools.SetActive(GameObject.Find("lblNew2"),false);
		}

		inGame = false;

	}


	public void addNewRing(){ // A D D new ANHOLINI

		TrackManager.singletonInstance.pipeCounter += 1;
		
		TrackBuilder.singletonInstance.internalRemoveRing();
		TrackBuilder.singletonInstance.internalAddNewRing();


		if(TrackManager.singletonInstance.inGame){

//			if(TrackManager.singletonInstance.pipeCounter % 5 == 0){
//				TrackManager.singletonInstance.addObstacle();
//			}
//			
//			if(TrackManager.singletonInstance.pipeCounter %10 == 0){
//				TrackManager.singletonInstance.addWall();
//			}

			/*if(TrackManager.singletonInstance.pipeCounter %50 == 0){
				
				if(TrackBuilder.singletonInstance.tunnelType == TunnelType.STRAIGHT){
					TrackBuilder.singletonInstance.tunnelType = TunnelType.CURVE;
				}else{
					TrackBuilder.singletonInstance.tunnelType = TunnelType.STRAIGHT;
				}
				
			}*/
		}

		//Debug.Log("RAIOS");

		travelerManager.newRingAdded();

		mumia.newRingAdded(); // adiciona objetos imoveis


				
		//singletonInstance.internalAddEnemy();

		//singletonInstance.internalAddPowerups();
		
	}

    public void limpa_playerprefs_uma_vez()
    {
        //---- Limpa uma vez o player prefs
        if (PlayerPrefs.GetInt("limpei_playerprefs") == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("limpei_playerprefs", 1);
            PlayerPrefs.Save();
            debug_log("limpei_playerprefs");
        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void capta_nome_usuario()
    {
        //---- Essa função só roda na primeira vez que o usuário instalar o sistema
        string RandomString = "";

		if (GameObject.Find ("InputField_nome_usuario")) {
			if (GameObject.Find ("InputField_nome_usuario").GetComponent<InputField> ().text == "Digite aqui..." || GameObject.Find ("InputField_nome_usuario").GetComponent<InputField> ().text == "") {
				debug_log ("É necessário digitar um nome!");
			} else {

				RandomString = Gera_Ramdom_String (8);
				id_Usuario_VL = DateTime.Now.ToString ("yyyy_MM_dd_HH_mm_ss") + "_id_usuario_" + RandomString + ".id";
				Senha_VL = Gera_Ramdom_String (8);
				NomeUsuario_VL = GameObject.Find ("InputField_nome_usuario").GetComponent<InputField> ().text;
				PlayerPrefs.SetString ("id_usuario", id_Usuario_VL);
				PlayerPrefs.SetString ("nome_usuario", NomeUsuario_VL);
				PlayerPrefs.SetString ("senha", Senha_VL);
				PlayerPrefs.SetInt ("pontuacao", 1);
				PlayerPrefs.Save ();

				//GameObject.Find("Tex_versao").GetComponent<Text>().text = "Versão: " + PlayerPrefs.GetString("version") + " -> " + PlayerPrefs.GetString("release_date");
				// mostra_mensagem("Carregando...", 0);
				StartCoroutine (CARREGA_versao_sistema ());

			}
		}

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
    IEnumerator CARREGA_versao_sistema()
    {
        string url = path_php_VL + "busca_versao_sistema.php";
        WWW www = new WWW(url);
        yield return www;

        if (www.error == null)
        {
            if (PlayerPrefs.GetString("version") != www.text)
            {
                //mostra_mensagem("A sua versão é a: [" + PlayerPrefs.GetString("version") + "].\n\nEla está desatualizada.\n\nInstale a versão corrente: [" + www.text + "].\n\nCódigo Erro: 0002", 1);
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
            }
            else
            {
                id_Usuario_VL = PlayerPrefs.GetString("id_usuario");
                NomeUsuario_VL = PlayerPrefs.GetString("nome_usuario");
                Senha_VL = PlayerPrefs.GetString("senha");
                pontuacao_VL = PlayerPrefs.GetInt("pontuacao");

                Debug.Log(id_Usuario_VL);
                Debug.Log(NomeUsuario_VL);
                Debug.Log(Senha_VL);
                Debug.Log(plataforma_VL);

                StartCoroutine(CARREGA_cadastra_usuario(id_Usuario_VL, NomeUsuario_VL, Senha_VL, plataforma_VL));

                //StartCoroutine(CARREGA_autentica_usuario(id_Usuario_VL, NomeUsuario_VL, Senha_VL, plataforma_VL));
                //  GameObject.Find("Tex_versao").GetComponent<Text>().text = "Versão: " + PlayerPrefs.GetString("version") + " - " + PlayerPrefs.GetString("release_date");

            }
        }
        else
        {
            //mostra_mensagem("Sem conexão com a internet!\nCódigo Erro: 0001\nRaw Message Error.:" + www.error, 1);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------	

    IEnumerator CARREGA_autentica_usuario(string id_usuario, string nome_usuario, string senha, int plataforma)
    {
        string cod_usuario = "";

        WWWForm form = new WWWForm();

        form.AddField("id_usuario", id_usuario);
        form.AddField("nome_usuario", nome_usuario);
        form.AddField("senha", senha);
        form.AddField("plataforma", plataforma);
        //---- WWWWWWWWWWWWWW
        string url = path_php_VL + "autentica_usuario.php";
        WWW www = new WWW(url, form); //---- Quando passa o form no php pega por POST
        yield return www;

        if (www.error == null)
        {
            if (www.text == "0")
            {
                debug_log("Usuário não autenticado retorno 0 zero vou cadastrar.");
                StartCoroutine(CARREGA_cadastra_usuario(id_Usuario_VL, NomeUsuario_VL, Senha_VL, plataforma_VL));
            }
            else
            {
                cod_usuario = www.text;
                Debug.Log("codigo do user" + cod_usuario.ToString());
                debug_log("Usuário AUTENTICADO com sucesso. O códido dele é:" + cod_usuario + ". Agora vou logar internamente");
                PlayerPrefs.SetInt("cod_usuario", int.Parse(cod_usuario));
                PlayerPrefs.Save();
            }
        }
        else
        {
            mostra_mensagem("Erro! Durante a autenticação do usuário.\n\nCódigo Erro: 0007\n\nRaw Message Error.:" + www.error, 1);
            debug_log("Erro! Durante a autenticação do usuário.\n\nCódigo Erro: 0007\n\nRaw Message Error.:" + www.error);
        }
        esconde_mensagem("");
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------		
    IEnumerator CARREGA_cadastra_usuario(string id_usuario, string nome_usuario, string senha, int plataforma_VL)
    {
        //---- WWWWWWWWWWWWWW
        WWWForm form = new WWWForm();

        form.AddField("id_usuario", id_usuario);
        form.AddField("nome_usuario", nome_usuario);
        form.AddField("senha", senha);
        form.AddField("largura", largura_VL);
        form.AddField("altura", altura_VL);
        form.AddField("current_resolution", current_resolution_VL);
        form.AddField("str_info", str_info_VL);
        form.AddField("plataforma", plataforma_VL);

        //---- WWWWWWWWWWWWWW
        string url = path_php_VL + "cadastra_usuario.php";

        debug_log(url);

        WWW www = new WWW(url, form); //---- Quando passa o form no php pega por POST
        yield return www;

        if (www.error == null)
        {
            debug_log("Usuario Cadastrado com Sucesso");

            if (GameObject.Find("form"))
            {

                GameObject.Find("form").active = false;

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------
				if (TrackManager.singletonInstance.btnRankingId == 1) {
					NGUITools.SetActive(TrackManager.singletonInstance.menuIntro,true);
					TrackManager.singletonInstance.canvasFormRanking.SetActive(false);
				}
				
				if (TrackManager.singletonInstance.btnRankingId == 2) {
					NGUITools.SetActive(TrackManager.singletonInstance.menuScore,true);
					TrackManager.singletonInstance.canvasFormRanking.SetActive(false);
				}
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------

            }
        }
        else
        {
            mostra_mensagem("Sem conexão com a internet.\n\nCódigo Erro: 0008\n\nRaw Message Error.:" + www.error, 1);
        }
    }

    public void carrega_ranking()
    {
        SceneManager.LoadScene("ranking");
    }

    public void debug_log(string mensagem)
    {
        Debug.Log(mensagem);
    }

    public string Gera_Ramdom_String(int length)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string returnString = "";

        for (int i = 0; i < length; i++)
        {
            returnString += chars[UnityEngine.Random.Range(0, chars.Length)];
        }
        return returnString;
    }

    public void mostra_mensagem(string msg, int botao)
    {
        debug_log("mostra_mensagem: " + msg);
        if (botao == 1)
        {
            GameObject.Find("Button_sair_msg").GetComponent<Button>().enabled = true;
            GameObject.Find("Button_sair_msg").transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GameObject.Find("Button_sair_msg").GetComponent<Button>().enabled = false;
            GameObject.Find("Button_sair_msg").transform.localScale = new Vector3(0, 0, 0);
        }

        GameObject.Find("Text_mensagem_principal").GetComponent<Text>().text = msg;
        GameObject.Find("RawImage_MSG").transform.position = GameObject.Find("!_centro_tela").transform.position;
    }

    public void esconde_mensagem(string msg)
    {
        //GameObject.Find("Text_mensagem_principal").GetComponent<Text>().text = msg;
        //GameObject.Find("RawImage_MSG").transform.position = GameObject.Find("!_fora_tela").transform.position;
        //GameObject.Find("form").GetComponent<Renderer>().enabled = false;

    }

	public void voltaMenAnterior() {

		//Debug.Log (TrackManager.singletonInstance.btnRankingId);
		
		if (TrackManager.singletonInstance.btnRankingId == 1) {
			NGUITools.SetActive(TrackManager.singletonInstance.menuIntro,true);
			TrackManager.singletonInstance.canvasFormRanking.SetActive(false);
		}
		
		if (TrackManager.singletonInstance.btnRankingId == 2) {
			NGUITools.SetActive(TrackManager.singletonInstance.menuScore,true);
			TrackManager.singletonInstance.canvasFormRanking.SetActive(false);
			
		}
		
		// TrackBuilder.singletonInstance.roadRenderChangerStarter ();
	}

}
