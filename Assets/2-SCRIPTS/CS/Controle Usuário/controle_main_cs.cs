using System;
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

public class controle_main_cs : MonoBehaviour 
{
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public struct registro_palavra
	{
		public string verbete;
		public string significado;		
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public List<registro_palavra> dicionario_VL;	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public string path_php_VL = "";
	
	public string path_dicionario_local_VL = ""; 
	public string path_dicionario_internet_VL =""; 
	
	public string nome_dicionario_local_VL = ""; 
	public string nome_dicionario_internet_VL = ""; 		
	
	int dl_VL=1;
	int pontuacao_VL=1;
	
	int nro_verbetes_VL=0;
	int ja_pisquei_VL=0;
	
	public int tecla_01_VL=0;
	public int tecla_15_VL=0;
	public int tecla_16_VL=0;
	
	
	public string id_Usuario_VL = "";
	public string NomeUsuario_VL = "";
	public string Senha_VL = "";	
	public string Ultima_Tecla_Pressionada_VL = "";	
	public int Som_VL=1;
	public int Dica_VL=1;	
	
	
	public string verbetes_unicos_VL="";	
	public string scramble_str_verbete_VL="";	
 
	public int state_tecla_01_VL=0;
	public int state_tecla_02_VL=0;	
	public int state_tecla_03_VL=0;
	public int state_tecla_04_VL=0;		
	public int state_tecla_05_VL=0;
	public int state_tecla_06_VL=0;	
	public int state_tecla_07_VL=0;
	public int state_tecla_08_VL=0;	
	public int state_tecla_09_VL=0;
	public int state_tecla_10_VL=0;	
	public int state_tecla_11_VL=0;
	public int state_tecla_12_VL=0;	
	public int state_tecla_13_VL=0;
	public int state_tecla_14_VL=0;		
	public int state_tecla_15_VL=0;
	public int state_tecla_16_VL=0;	
	public int state_tecla_17_VL=0;
	public int state_tecla_18_VL=0;	
	public int state_tecla_19_VL=0;
	public int state_tecla_20_VL=0;	
	public int state_tecla_21_VL=0;
	public int state_tecla_22_VL=0;	
	public int state_tecla_23_VL=0;
	public int state_tecla_24_VL=0;		
	public int state_tecla_25_VL=0;
	public int state_tecla_26_VL=0;	
	public int state_tecla_27_VL=0;
	public int state_tecla_28_VL=0;	
	public int state_tecla_29_VL=0;
	public int state_tecla_30_VL=0;	
	public int state_tecla_31_VL=0;	
	public int state_tecla_40_VL=0;	
	public int state_tecla_50_VL=0;	
	public int state_tecla_60_VL=0;			
	public int state_tecla_70_VL=0;	
	public int state_tecla_80_VL=0;	
	public int state_tecla_90_VL=0;			
	
	
	public int plataforma_VL=0;			

	public int fechando_VL=0;	
		
	public Material mat_tecla_normal;
	public Material mat_tecla_red;
	
	public int ct_teclas_pressionadas_VL=0;
	
	#if UNITY_WEBPLAYER
	public static string webplayerQuitURL = "http://www.ahoradofilme.com.br";
	#endif	
	
	string str_info_VL = "";
	string largura_VL = "";
	string altura_VL = "";
	string current_resolution_VL = "";		
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	void Start() 
	{
		limpa_playerprefs_uma_vez();
	  str_info_VL = "0";
	  largura_VL = "0";
	  altura_VL = "0";
	  current_resolution_VL = "0";				
		
		dicionario_VL = new List<registro_palavra>(); 
		dl_VL=1; //---- exibe dl
		
		tecla_01_VL=0;
		tecla_15_VL=0;
		tecla_16_VL=0;
		
		fechando_VL=0;
 
		mostra_mensagem("Carregando...",0);
		debug_log("Iniciei em Start();");

		//Camera.main.aspect = 1280f/720f;

	  largura_VL = Screen.width.ToString();
	  altura_VL = Screen.height.ToString();
		current_resolution_VL =  Screen.currentResolution.ToString();
		GameObject.Find("Text_resolucao").GetComponent<Text>().text  = largura_VL +"x"+altura_VL;

		path_php_VL = "http://www.ahoradofilme.com.br/php_enigma_v1003/";
		path_dicionario_local_VL = ""; 
		path_dicionario_internet_VL ="http://www.ahoradofilme.com.br/enigma/dicionarios/"; 
			
		path_dicionario_local_VL = PlayerPrefs.GetString("path_dicionario_local");   			
		path_dicionario_internet_VL ="http://www.ahoradofilme.com.br/enigma/dicionarios/"; 
		nome_dicionario_local_VL =PlayerPrefs.GetString("nome_dicionario_local");   			
		nome_dicionario_internet_VL =	PlayerPrefs.GetString("nome_dicionario_internet");  			
			
		//--- ANDROID
		if (Application.platform == RuntimePlatform.Android) 
		{	
			//----  /data/data/com.freitaglabs.androistester/files
			path_dicionario_local_VL = Application.persistentDataPath + "/"; 
			plataforma_VL=0;
		}
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer) 
		{
			//---- C:/Users/ed/AppData/LocalLow/cia/enigma
			path_dicionario_local_VL = Application.persistentDataPath + "/"; 
			plataforma_VL=1;
		}
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.IPhonePlayer) 
		{
			//--- editor Application.persistentDataPath: /Users/gabrielpalmamoraes/Library/Application
			//--- ipad /var/mobile/containers/data/application/64f34b24-1498-473a-a240-cd7e04bc94b4/Documents
			path_dicionario_local_VL = Application.persistentDataPath + "/"; 
			plataforma_VL=2;
		}		
	
		PlayerPrefs.SetString("version", "1.0.0.3");   
		PlayerPrefs.SetString("release_date", "30/04/2017");   
		PlayerPrefs.Save();				
	
		if(PlayerPrefs.GetString("nome_usuario") == "")
		{
			capta_nome_usuario();
		}
		else		
		{
			StartCoroutine(CARREGA_versao_sistema());
		}	
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void load_info()
	{
		SceneManager.LoadScene("info");		
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void limpa_playerprefs_uma_vez()
	{
    //---- Limpa uma vez o player prefs
		if(PlayerPrefs.GetInt("limpei_playerprefs") == 0)
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
		
		esconde_mensagem("");		
		GameObject.Find("RawImage_formulario").transform.position = GameObject.Find("!_centro_tela").transform.position; 		
		
		if(GameObject.Find("InputField_nome_usuario").GetComponent<InputField>().text == "Digite aqui..." || GameObject.Find("InputField_nome_usuario").GetComponent<InputField>().text == "")
		{
			debug_log("É necessário digitar um nome!");
		}	
		else
		{
			nome_dicionario_local_VL = "dicionario_V001_127_K.dat";
			nome_dicionario_internet_VL = "dicionario_V001_127_K.dat";
		
			RandomString = Gera_Ramdom_String(8);
			id_Usuario_VL = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") +"_id_usuario_"+ RandomString + ".id";
			Senha_VL = Gera_Ramdom_String(8);
			NomeUsuario_VL = GameObject.Find("InputField_nome_usuario").GetComponent<InputField>().text;
			PlayerPrefs.SetString("id_usuario", id_Usuario_VL);   
			PlayerPrefs.SetString("nome_usuario", NomeUsuario_VL);   
			GameObject.Find("Text_usuario").GetComponent<Text>().text  = NomeUsuario_VL;
			PlayerPrefs.SetString("senha", Senha_VL);   			
			PlayerPrefs.SetInt("pontuacao", 1);   			
			PlayerPrefs.SetInt("som", 1);   			
			PlayerPrefs.SetInt("dica", 1);   
			PlayerPrefs.SetString("path_dicionario_local", path_dicionario_local_VL );   			
			PlayerPrefs.SetString("path_dicionario_internet", path_dicionario_internet_VL);   			
			PlayerPrefs.SetString("nome_dicionario_local", nome_dicionario_local_VL);   			
			PlayerPrefs.SetString("nome_dicionario_internet", nome_dicionario_internet_VL);  
			PlayerPrefs.Save();	
			
			GameObject.Find("RawImage_formulario").transform.position = GameObject.Find("!_fora_tela").transform.position; 	
			GameObject.Find("Tex_versao").GetComponent<Text>().text = "Versão: " + PlayerPrefs.GetString("version") + " -> "+ PlayerPrefs.GetString("release_date");
			mostra_mensagem("Carregando...",0);
			StartCoroutine(CARREGA_versao_sistema());
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
			if(PlayerPrefs.GetString("version") != www.text)	
			{	
				mostra_mensagem("A sua versão é a: ["+PlayerPrefs.GetString("version")+"].\n\nEla está desatualizada.\n\nInstale a versão corrente: ["+www.text+ "].\n\nCódigo Erro: 0002", 1);
				PlayerPrefs.DeleteAll();
				PlayerPrefs.Save();	
			}
			else
			{
				GameObject.Find("Tex_versao").GetComponent<Text>().text = "Versão: " + PlayerPrefs.GetString("version") + " - "+ PlayerPrefs.GetString("release_date");
				StartCoroutine(CARREGA_config_sistema());
			}	
		}
		else
		{
			mostra_mensagem("Sem conexão com a internet!\nCódigo Erro: 0001\nRaw Message Error.:" + www.error, 1);
		}		
	} 	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator CARREGA_config_sistema()
	{
		string str_DIC="";
		string str_NOME_DICIONARIO="";
		
		debug_log("Entrei em: CARREGA_config_sistema()");
    string url = path_php_VL + "busca_config_sistema.php";
		WWW www_config = new WWW(url);
		yield return www_config;
		
		if (www_config.error == null)
		{
			byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(www_config.text);
      string str_converted = System.Text.Encoding.UTF8.GetString(utf8Bytes);
			string[] ArrayDeFields = str_converted.Split(new char[] { '\n' , '#' }, System.StringSplitOptions.RemoveEmptyEntries);
			
			//---- Verifica se este é um dicionário válido
			str_DIC = ArrayDeFields[0].Trim();
			str_NOME_DICIONARIO = ArrayDeFields[1].Trim();
			
			debug_log("str_DIC:"+str_DIC);
			debug_log("str_NOME_DICIONARIO"+str_NOME_DICIONARIO);
			
			if(str_DIC.Trim().Equals("DIC"))
			{	
				if(str_NOME_DICIONARIO.Trim().Equals(nome_dicionario_local_VL))
				{
					debug_log("verifica_se_dicionario_existe_localmente()");
					verifica_se_dicionario_existe_localmente();
				}
				else
				{
					nome_dicionario_local_VL = str_NOME_DICIONARIO;
					nome_dicionario_internet_VL = str_NOME_DICIONARIO;
					PlayerPrefs.SetString("nome_dicionario_local", nome_dicionario_local_VL);   			
					PlayerPrefs.SetString("nome_dicionario_internet", nome_dicionario_internet_VL);  
					PlayerPrefs.Save();						
					debug_log("CARREGA_download_dicionario_internet_salva_localmente()");					
					StartCoroutine(CARREGA_download_dicionario_internet_salva_localmente(path_dicionario_local_VL, path_dicionario_internet_VL, nome_dicionario_local_VL, nome_dicionario_local_VL));		
				}	
			}
		}	
		else
		{
			mostra_mensagem("Sem conexão com a internet!\nCódigo Erro: 0009\nRaw Message Error.:" + www_config.error, 1);
		}		
	} 	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void verifica_se_dicionario_existe_localmente()
	{
		string path_e_nome_arquivo = "";

		path_e_nome_arquivo = path_dicionario_local_VL + nome_dicionario_local_VL;
		
		if (System.IO.File.Exists(path_e_nome_arquivo))
		{
			debug_log("Dicionário Existe Localmente.");
			carrega_normal(path_e_nome_arquivo);
		}
		else		
		{
			debug_log("Dicionário NÃO existe localmente, vou fazer o download.");
			StartCoroutine(CARREGA_download_dicionario_internet_salva_localmente(path_dicionario_local_VL, path_dicionario_internet_VL, nome_dicionario_local_VL, nome_dicionario_internet_VL));		
		}				
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator CARREGA_download_dicionario_internet_salva_localmente(string path_dicionario_local, string path_dicionario_internet, string nome_dicionario_local, string nome_dicionario_internet  )
	{
		string url_dicionario_internet = "";
		string path_e_nome_arquivo_local = "";
		string str_ID="";
		string str_MASTER="";
		
		path_e_nome_arquivo_local = path_dicionario_local + nome_dicionario_local;
		url_dicionario_internet = path_dicionario_internet + nome_dicionario_internet;
		WWW www_dicionario = new WWW(url_dicionario_internet);
	
		yield return www_dicionario;
		if (www_dicionario.error == null)
		{
			byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(www_dicionario.text);
      string str_converted = System.Text.Encoding.UTF8.GetString(utf8Bytes);
			string[] ArrayDeFields = str_converted.Split(new char[] { '\n' , '#' }, System.StringSplitOptions.RemoveEmptyEntries);
			
			//---- Verifica se este é um dicionário válido
			str_ID = ArrayDeFields[0];
			str_MASTER = ArrayDeFields[1];
			if(str_ID.Trim().Equals("ID") && str_MASTER.Trim().Equals("MASTER"))
			{
				System.IO.File.WriteAllBytes(path_e_nome_arquivo_local, utf8Bytes);
				carrega_normal(path_e_nome_arquivo_local);
			}	
			else
			{ 
				mostra_mensagem("Erro ao carregar dicionário.\n\n ID INVÁLIDO.\n\nCódigo Erro: 0004\n", 1);
			}
		}
		else
		{
			mostra_mensagem("Erro ao carregar dicionário.\n\nCódigo Erro: 0005\n\nRaw Message Error.:" + www_dicionario.error, 1);
		}  
	}			
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void carrega_normal(string path_e_nome_arquivo)
	{
		id_Usuario_VL = PlayerPrefs.GetString("id_usuario");
		NomeUsuario_VL = PlayerPrefs.GetString("nome_usuario");		
		Senha_VL = PlayerPrefs.GetString("senha");		
		Som_VL =  PlayerPrefs.GetInt("som");		
		Dica_VL =  PlayerPrefs.GetInt("dica");		
		pontuacao_VL =  PlayerPrefs.GetInt("pontuacao");		
		
		GameObject.Find("Text_usuario").GetComponent<Text>().text  = NomeUsuario_VL;
		
		if(Som_VL==1)
		{
			GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn = true;
		}	
		else
		{
			GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn = false;
		}	
		
		if(Dica_VL==1)
		{
			GameObject.Find("Toggle_dica").GetComponent<Toggle>().isOn = true;
		}			
		else
		{
			GameObject.Find("Toggle_dica").GetComponent<Toggle>().isOn = false;
		}	
		
		StartCoroutine(CARREGA_le_arquivo_local(path_e_nome_arquivo));		
		
		GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = "";

		ja_pisquei_VL=0;
		state_tecla_01_VL=0;
		state_tecla_02_VL=0;
		state_tecla_03_VL=0;
		state_tecla_04_VL=0;
		state_tecla_05_VL=0;
		state_tecla_06_VL=0;
		state_tecla_07_VL=0;
		state_tecla_08_VL=0;
		state_tecla_09_VL=0;
		state_tecla_10_VL=0;
		state_tecla_11_VL=0;
		state_tecla_12_VL=0;
		state_tecla_13_VL=0;
		state_tecla_14_VL=0;
		state_tecla_15_VL=0;
		state_tecla_16_VL=0;
		state_tecla_17_VL=0;
		state_tecla_18_VL=0;
		state_tecla_19_VL=0;
		state_tecla_20_VL=0;
		state_tecla_21_VL=0;
		state_tecla_22_VL=0;
		state_tecla_23_VL=0;
		state_tecla_24_VL=0;
		state_tecla_25_VL=0;
		state_tecla_26_VL=0;
		state_tecla_27_VL=0;
		state_tecla_28_VL=0;
		state_tecla_29_VL=0;
		state_tecla_30_VL=0;
		state_tecla_31_VL=0;
		state_tecla_40_VL=0;
		state_tecla_50_VL=0;
		state_tecla_60_VL=0;			
		state_tecla_70_VL=0;
		state_tecla_80_VL=0;
		state_tecla_90_VL=0;		
		montagem_palavra_no_teclado("                               ");
		todas_teclas_cima(1);		
		todas_teclas_pretas(1);	
		esvazia_letras();
		ct_teclas_pressionadas_VL=0;
		GameObject.Find("Tex_pontuacao").GetComponent<Text>().text = "Pontos:" + pontuacao_VL.ToString();
		GameObject.Find("Toggle_dica").GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
		GameObject.Find("Toggle_dica").GetComponent<Toggle>().onValueChanged.AddListener
		(
			delegate 
			{    
				seleciona_toggle_dica(GameObject.Find("Toggle_dica").GetComponent<Toggle>().name, GameObject.Find("Toggle_dica").GetComponent<Toggle>().GetComponent<Toggle>().isOn);
			}
		);	
		GameObject.Find("Toggle_som").GetComponent<Toggle>().onValueChanged.AddListener
		(
			delegate 
			{    
				seleciona_toggle_som(GameObject.Find("Toggle_som").GetComponent<Toggle>().name, GameObject.Find("Toggle_som").GetComponent<Toggle>().GetComponent<Toggle>().isOn);
			}
		);		
		StartCoroutine(CARREGA_autentica_usuario(id_Usuario_VL, NomeUsuario_VL, Senha_VL, plataforma_VL ));
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------		
	IEnumerator CARREGA_le_arquivo_local(string path_e_nome_arquivo)
	{
		WWW www_dicionario = new WWW("file:///"+path_e_nome_arquivo);
		
		yield return www_dicionario;
		
		if (www_dicionario.error == null)
		{
			byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(www_dicionario.text);
      string str_converted = System.Text.Encoding.UTF8.GetString(utf8Bytes);
  		processa_dicionario(str_converted);
		}
		else
		{
			debug_log("Erro ao carregar dicionário localmente.\n\nCódigo Erro: 0006\n\nRaw Message Error.:" + www_dicionario.error);
			mostra_mensagem("Erro ao carregar dicionário localmente.\n\nCódigo Erro: 0006\n\nRaw Message Error.:" + www_dicionario.error, 1);
		}  
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------		
	IEnumerator CARREGA_autentica_usuario(string id_usuario, string nome_usuario, string senha, int plataforma)
	{
		string cod_usuario="";
		
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
			if(www.text == "0")
			{
				debug_log("Usuário não autenticado retorno 0 zero vou cadastrar.");
				StartCoroutine(CARREGA_cadastra_usuario(id_Usuario_VL, NomeUsuario_VL, Senha_VL, plataforma_VL));
			}
			else
			{
				cod_usuario = www.text;
				debug_log("Usuário AUTENTICADO com sucesso. O códido dele é:"+cod_usuario+". Agora vou logar internamente");
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
		WWW www = new WWW(url, form); //---- Quando passa o form no php pega por POST
		yield return www;

		if (www.error == null)
		{	
			debug_log("Usuario Cadastrado com Sucesso");
		}
		else
		{
			mostra_mensagem("Sem conexão com a internet.\n\nCódigo Erro: 0008\n\nRaw Message Error.:" + www.error, 1);
		}		
	} 	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void seleciona_toggle_dica (string name_t, bool toggleStatus) 
	{
		if(Dica_VL==1)
		{	
			PlayerPrefs.SetInt("dica", 0); 
			PlayerPrefs.Save();
			Dica_VL=0;
		}
		else
		{	
			PlayerPrefs.SetInt("dica", 1); 
			PlayerPrefs.Save();
			Dica_VL=1;
		}
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void seleciona_toggle_som (string name_t, bool toggleStatus) 
	{
		if(Som_VL==1)
		{	
			PlayerPrefs.SetInt("som", 0); 
			PlayerPrefs.Save();
			Som_VL=0;
		}
		else
		{	
			PlayerPrefs.SetInt("som", 1); 
			PlayerPrefs.Save();
			Som_VL=1;
		}
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void pisca_verbete() 
	{
		if(ja_pisquei_VL==0)
		{	
			ja_pisquei_VL=1;
      StartCoroutine(pisca_verbete_com_wait(0.175f));
		}
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
  IEnumerator pisca_verbete_com_wait(float delay) 
	{
		int pos_sorteada=0;

		pos_sorteada = Random.Range(0, 6);
		switch(pos_sorteada)
		{
			case 0 : {
								 GameObject.Find("!_verbete").transform.position = GameObject.Find("!_centro_tela").transform.position; 
							 }break;
			case 1 : {
								 GameObject.Find("!_verbete").transform.position = GameObject.Find("!_pos_01").transform.position; 
							 }break;
			case 2 : {
								 GameObject.Find("!_verbete").transform.position = GameObject.Find("!_pos_02").transform.position; 
							 }break;
			case 3 : {
								 GameObject.Find("!_verbete").transform.position = GameObject.Find("!_pos_03").transform.position; 
							 }break;							
			case 4 : {
								 GameObject.Find("!_verbete").transform.position = GameObject.Find("!_pos_04").transform.position; 
							 }break;
			case 5 : {
								 GameObject.Find("!_verbete").transform.position = GameObject.Find("!_pos_05").transform.position; 
							 }break;
		}	
     yield return new WaitForSeconds(delay);
		GameObject.Find("!_verbete").transform.position = GameObject.Find("!_fora_tela").transform.position;		 
  }	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
  IEnumerator mostra_dica_plus_plus(float delay) 
	{
		GameObject.Find("!_verbete_dica_plus_plus").transform.position = GameObject.Find("!_centro_tela_top").transform.position; 
    yield return new WaitForSeconds(delay);
		GameObject.Find("!_verbete_dica_plus_plus").transform.position = GameObject.Find("!_fora_tela").transform.position;		 
  }		
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void processa_dicionario(string str_leitura_file) 
	{
		string scramble_str_verbete = ""; 
		string str_verbete = ""; 
		int z = 0;
		int idx_verbete_sorteado = 0;
		/*
		int tam_00a05 = 0;
		int tam_05a10 = 0;
	  int tam_11a15 = 0;
		int tam_16a20 = 0;
		int tam_21a25 = 0;
		int tam_26a32 = 0;				
		int tam_maior_verbete = 0;
		*/
		string[] ArrayDeFields = str_leitura_file.Split(new char[] { '\n' , '#' }, System.StringSplitOptions.RemoveEmptyEntries);

		//tam_maior_verbete = 0;
		nro_verbetes_VL = 0;
		for(z=0; z < ArrayDeFields.Length-1; z++)
		{
			registro_palavra temp_dicionario = new registro_palavra(); 

			temp_dicionario.verbete = ArrayDeFields[z]; //??? verificar se precisa \n
			temp_dicionario.significado = ArrayDeFields[z+1];//??? verificar se precisa \n
			
			/*
			int k = 0;
			string verbete_lower = ""; 
			
			verbete_lower = ArrayDeFields[z].ToLower();
			for(k=0; k < verbete_lower.Length; k++)
			{
				if(verbetes_unicos_VL.Contains(verbete_lower.Substring(k,1))==false)
				{
					verbetes_unicos_VL = verbetes_unicos_VL + verbete_lower.Substring(k,1);
				}
			}
			debug_log(verbetes_unicos_VL);
			*/

			/*
			if(ArrayDeFields[z].Length > tam_maior_verbete)
			{
				tam_maior_verbete = ArrayDeFields[z].Length;
			}
			if(ArrayDeFields[z].Length <= 5)
			{
				tam_00a05 = tam_00a05 + 1;
			}
			if(ArrayDeFields[z].Length >= 05 && ArrayDeFields[z].Length <=10)
			{
				tam_05a10 = tam_05a10 + 1;
			}
			if(ArrayDeFields[z].Length >= 11 && ArrayDeFields[z].Length <=15)
			{
				tam_11a15 = tam_11a15 + 1;
			}
			if(ArrayDeFields[z].Length >= 16 && ArrayDeFields[z].Length <=20)
			{
				tam_16a20 = tam_16a20 + 1;
			}
			if(ArrayDeFields[z].Length >= 21 && ArrayDeFields[z].Length <=25)
			{
				tam_21a25 = tam_21a25 + 1;
			}
			if(ArrayDeFields[z].Length >= 26 && ArrayDeFields[z].Length <=32)
			{
				tam_26a32 = tam_26a32 + 1;
			}		
			*/			
			dicionario_VL.Add(temp_dicionario);			
			z++;
			nro_verbetes_VL++;
		}
		
		//debug_log("tam_maior_verbete: " + tam_maior_verbete + " tam_00a05:"+tam_00a05+" tam_05a10:"+tam_05a10+" tam_11a15:"+tam_11a15+" tam_16a20:"+tam_16a20+" tam_21a25:"+tam_21a25+" tam_26a32:"+tam_26a32);			 
	 
		idx_verbete_sorteado = Random.Range(1, nro_verbetes_VL);
		
		str_verbete = dicionario_VL[idx_verbete_sorteado].verbete;
		
		//---- scramble
		scramble_str_verbete = func_scramble_verbete(str_verbete);
		
		GameObject.Find("Tex_verbete").GetComponent<Text>().text = str_verbete;
		GameObject.Find("Tex_verbete_sombra").GetComponent<Text>().text = str_verbete;		
		//GameObject.Find("Tex_verbete_scramble").GetComponent<Text>().text = scramble_str_verbete;
		
		GameObject.Find("Tex_verbete_sombra_plus_plus").GetComponent<Text>().text = "A primeira letra é:[" +GameObject.Find("Tex_verbete").GetComponent<Text>().text.Substring(0,1).ToUpper() + "]";
		GameObject.Find("Tex_verbete_plus_plus").GetComponent<Text>().text = "A primeira letra é:[" +GameObject.Find("Tex_verbete").GetComponent<Text>().text.Substring(0,1).ToUpper() + "]";		
		
		if(GameObject.Find("Toggle_dica").GetComponent<Toggle>().isOn == true)
		{
			GameObject.Find("Tex_dica").GetComponent<Text>().text = dicionario_VL[idx_verbete_sorteado].significado;					
		}	
		else		
		{
			GameObject.Find("Tex_dica").GetComponent<Text>().text = "";					
		}	
			
		//GameObject.Find("Tex_nro_verbetes").GetComponent<Text>().text = nro_verbetes_VL.ToString();	
		GameObject.Find("Tex_nro_verbetes").GetComponent<Text>().text ="Palavras Codificadas:"+ string.Format("{0:#,###0}", nro_verbetes_VL);
		
		montagem_palavra_no_teclado("                               ");
		esvazia_letras();
		scramble_str_verbete_VL = scramble_str_verbete;
		montagem_palavra_no_teclado(scramble_str_verbete);
		
		todas_teclas_cima(1);	
		
		mostra_mensagem("",0);
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public string func_scramble_verbete (string str_verbete) 
	{
		List<int> vetor_idx_sorteados = new List<int>();
		
		string scramble_str_verbete="";

		int nro_sorteado=0;		
		int achei_nao_sorteado=0;
		int achei=0;
		int i=0;
		int j=0;

			for(j=0; j < str_verbete.Length; j++)
			{	
				nro_sorteado = Random.Range(0, str_verbete.Length);
				achei_nao_sorteado=0;
				while(achei_nao_sorteado==0)
				{
					if(vetor_idx_sorteados.Count==0)
					{
						vetor_idx_sorteados.Add(nro_sorteado);
						achei_nao_sorteado=1;
					}	
					else
					{
						nro_sorteado = Random.Range(0, str_verbete.Length);
						achei=0;
						for(i=0; i < vetor_idx_sorteados.Count; i++)
						{
							if(vetor_idx_sorteados[i]==nro_sorteado)
							{
								achei=1;
							}
						}			
						if(achei==0)
						{
							achei_nao_sorteado=1;
							vetor_idx_sorteados.Add(nro_sorteado);
						}					
					}
				}	
				scramble_str_verbete = scramble_str_verbete + str_verbete.Substring(nro_sorteado, 1);
				scramble_str_verbete_VL = scramble_str_verbete;
			}
			vetor_idx_sorteados.Clear();
			
		if(str_verbete==scramble_str_verbete)
		{	
			func_scramble_verbete(str_verbete);
		}	
    if(str_verbete!=scramble_str_verbete)
		{
		  return(scramble_str_verbete);
		}	
		return func_scramble_verbete(str_verbete);
	}		
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void montagem_palavra_no_teclado(string palavra)
	{
		string palavra_lower = palavra.ToLower();
		
		for(int i=0;i < palavra_lower.Length;i++)
		{
			switch(i)
			{
				case 0 : GameObject.Find("tm_tecla_01").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;									
				case 1 : GameObject.Find("tm_tecla_02").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 2 : GameObject.Find("tm_tecla_03").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 3 : GameObject.Find("tm_tecla_04").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 4 : GameObject.Find("tm_tecla_05").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 5 : GameObject.Find("tm_tecla_06").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 6 : GameObject.Find("tm_tecla_07").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 7 : GameObject.Find("tm_tecla_08").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;				
				case 8 : GameObject.Find("tm_tecla_09").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 9 : GameObject.Find("tm_tecla_10").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 10: GameObject.Find("tm_tecla_11").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 11: GameObject.Find("tm_tecla_12").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 12: GameObject.Find("tm_tecla_13").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 13: GameObject.Find("tm_tecla_14").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 14: GameObject.Find("tm_tecla_15").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 15: GameObject.Find("tm_tecla_16").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 16: GameObject.Find("tm_tecla_17").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 17: GameObject.Find("tm_tecla_18").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 18: GameObject.Find("tm_tecla_19").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;				
				case 19: GameObject.Find("tm_tecla_20").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 20: GameObject.Find("tm_tecla_21").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 21: GameObject.Find("tm_tecla_22").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 22: GameObject.Find("tm_tecla_23").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 23: GameObject.Find("tm_tecla_24").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 24: GameObject.Find("tm_tecla_25").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 25: GameObject.Find("tm_tecla_26").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 26: GameObject.Find("tm_tecla_27").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 27: GameObject.Find("tm_tecla_28").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 28: GameObject.Find("tm_tecla_29").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
				case 29: GameObject.Find("tm_tecla_30").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;				
				case 30: GameObject.Find("tm_tecla_31").GetComponent<TextMesh>().text = palavra_lower.Substring(i,1); break;
			}	
		}
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{ 
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			if ( Physics.Raycast (ray, out hit, 2000.0f)) 
			{
				//debug_log(hit.transform.name);
				muda_estado_tecla(hit.transform.name.Substring(hit.transform.name.Length-2,2));
				
				verifica_se_acertou();
			}
		}
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void carrega_ranking()
	{
		SceneManager.LoadScene("ranking");		
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void verifica_se_acertou()
	{
		if(GameObject.Find("Tex_verbete").GetComponent<Text>().text.ToLower() == GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.ToLower())
		{
			GameObject.Find("RawImage_acertou_palavra").transform.position = GameObject.Find("!_centro_tela").transform.position;
			
			GameObject.Find("Text_acertou").GetComponent<Text>().text = GameObject.Find("Tex_verbete").GetComponent<Text>().text;
			
			pontuacao_VL = pontuacao_VL + 10;
			
			if(ja_pisquei_VL==1 && GameObject.Find("Toggle_dica").GetComponent<Toggle>().isOn == true)
			{
				pontuacao_VL = pontuacao_VL - 9; //---- ganha 1 ponto
			}	
			else
			{	
				if(ja_pisquei_VL == 1)
				{	
					pontuacao_VL = pontuacao_VL - 7;
				}	
				if(GameObject.Find("Toggle_dica").GetComponent<Toggle>().isOn == true)
				{	
					pontuacao_VL = pontuacao_VL - 5;
				}	
			}
			GameObject.Find("Tex_pontuacao").GetComponent<Text>().text = "Pontos:" + pontuacao_VL.ToString();
			
			PlayerPrefs.SetInt("pontuacao", pontuacao_VL);   			
			PlayerPrefs.Save();	
			if(fechando_VL==0)
			{			
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true)
				{
					AudioSource audio = GameObject.Find("AudioMaster_acertou").GetComponent<AudioSource>();
					audio.Play();
				}							
				StartCoroutine(timer_fechar_acertou(3.0f));
			}	
			fechando_VL=1;
		}
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void fechar_msg_imediatamente()
	{
		GameObject.Find("RawImage_acertou_palavra").transform.position = GameObject.Find("!_fora_tela").transform.position;
		StartCoroutine(func_proximo(1f));
		fechando_VL=2;
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	IEnumerator timer_fechar_acertou(float delay_fechar)
	{
		if(fechando_VL==0)
		{
			yield return new WaitForSeconds(delay_fechar);	
			if(fechando_VL!=2)
			{
				GameObject.Find("RawImage_acertou_palavra").transform.position = GameObject.Find("!_fora_tela").transform.position;
				StartCoroutine(func_proximo(1f));
			}	
			fechando_VL=0;
		}	
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void todas_teclas_vermelhas()
	{
		GameObject.Find("mesh_tecla_01").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_02").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_03").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_04").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_05").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_06").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_07").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_08").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_09").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_10").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_11").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_12").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_13").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_14").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_15").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_16").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_17").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_18").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_19").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_20").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_21").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_22").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_23").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_24").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_25").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_26").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_27").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_28").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_29").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_30").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("mesh_tecla_31").GetComponent<Renderer>().material = mat_tecla_red;		
		GameObject.Find("mesh_tecla_40").GetComponent<Renderer>().material = mat_tecla_red;		
		GameObject.Find("mesh_tecla_50").GetComponent<Renderer>().material = mat_tecla_red;		
		GameObject.Find("mesh_tecla_60").GetComponent<Renderer>().material = mat_tecla_red;				
		GameObject.Find("mesh_tecla_70").GetComponent<Renderer>().material = mat_tecla_red;		
		GameObject.Find("mesh_tecla_80").GetComponent<Renderer>().material = mat_tecla_red;		
		GameObject.Find("mesh_tecla_90").GetComponent<Renderer>().material = mat_tecla_red;				
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void todas_teclas_pretas(int alvo)
	{
		if(alvo==1)
		{
			GameObject.Find("mesh_tecla_70").GetComponent<Renderer>().material = mat_tecla_normal;		
			GameObject.Find("mesh_tecla_80").GetComponent<Renderer>().material = mat_tecla_normal;		
		}
  	GameObject.Find("mesh_tecla_01").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_02").GetComponent<Renderer>().material = mat_tecla_normal;		
		GameObject.Find("mesh_tecla_03").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_04").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_05").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_06").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_07").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_08").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_09").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_10").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_11").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_12").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_13").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_14").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_15").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_16").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_17").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_18").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_19").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_20").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_21").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_22").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_23").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_24").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_25").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_26").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_27").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_28").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_29").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_30").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("mesh_tecla_31").GetComponent<Renderer>().material = mat_tecla_normal;	
		GameObject.Find("mesh_tecla_40").GetComponent<Renderer>().material = mat_tecla_normal;		
		GameObject.Find("mesh_tecla_50").GetComponent<Renderer>().material = mat_tecla_normal;		
		GameObject.Find("mesh_tecla_60").GetComponent<Renderer>().material = mat_tecla_normal;	
		GameObject.Find("mesh_tecla_90").GetComponent<Renderer>().material = mat_tecla_normal;					
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void todas_teclas_cima(int alvo)
	{
		if(alvo==1)
		{
			GameObject.Find("tecla_dupla_01_fbx_70").GetComponent<Animation>().Play("armature_TECLA|anim_up");
			GameObject.Find("tecla_dupla_01_fbx_80").GetComponent<Animation>().Play("armature_TECLA|anim_up");		
		}
		GameObject.Find("tecla_01_fbx_01").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_02").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_03").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_04").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_05").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_06").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_07").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_08").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_09").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_10").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_11").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_12").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_13").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_14").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_15").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_16").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_17").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_18").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_19").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_20").GetComponent<Animation>().Play("armature_TECLA|anim_up");
		GameObject.Find("tecla_01_fbx_21").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_22").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_23").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_24").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_25").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_26").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_27").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_28").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
		GameObject.Find("tecla_01_fbx_29").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_01_fbx_30").GetComponent<Animation>().Play("armature_TECLA|anim_up");
		GameObject.Find("tecla_01_fbx_31").GetComponent<Animation>().Play("armature_TECLA|anim_up");	
		GameObject.Find("tecla_dupla_01_fbx_40").GetComponent<Animation>().Play("armature_TECLA|anim_up");
		GameObject.Find("tecla_dupla_01_fbx_50").GetComponent<Animation>().Play("armature_TECLA|anim_up");		
		GameObject.Find("tecla_dupla_01_fbx_60").GetComponent<Animation>().Play("armature_TECLA|anim_up");
		GameObject.Find("tecla_dupla_01_fbx_90").GetComponent<Animation>().Play("armature_TECLA|anim_up");			
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void todas_teclas_baixo()
	{
		GameObject.Find("tecla_01_fbx_01").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_02").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_03").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_04").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_05").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_06").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_07").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_08").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_09").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_10").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_11").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_12").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_13").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_14").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_15").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_16").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_17").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_18").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_19").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_20").GetComponent<Animation>().Play("armature_TECLA|anim_down");
		GameObject.Find("tecla_01_fbx_21").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_22").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_23").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_24").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_25").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_26").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_27").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_28").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
		GameObject.Find("tecla_01_fbx_29").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_01_fbx_30").GetComponent<Animation>().Play("armature_TECLA|anim_down");
		GameObject.Find("tecla_01_fbx_31").GetComponent<Animation>().Play("armature_TECLA|anim_down");	
		GameObject.Find("tecla_dupla_01_fbx_40").GetComponent<Animation>().Play("armature_TECLA|anim_down");
		GameObject.Find("tecla_dupla_01_fbx_50").GetComponent<Animation>().Play("armature_TECLA|anim_down");		
		GameObject.Find("tecla_dupla_01_fbx_60").GetComponent<Animation>().Play("armature_TECLA|anim_down");		
		GameObject.Find("tecla_dupla_01_fbx_70").GetComponent<Animation>().Play("armature_TECLA|anim_down");
		GameObject.Find("tecla_dupla_01_fbx_80").GetComponent<Animation>().Play("armature_TECLA|anim_down");		
		GameObject.Find("tecla_dupla_01_fbx_90").GetComponent<Animation>().Play("armature_TECLA|anim_down");			
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	void esvazia_letras()
	{
		GameObject.Find("tm_tecla_01").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_02").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_03").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_04").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_05").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_06").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_07").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_08").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_09").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_10").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_11").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_12").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_13").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_14").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_15").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_16").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_17").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_18").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_19").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_20").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_21").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_22").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_23").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_24").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_25").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_26").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_27").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_28").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_29").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_30").GetComponent<TextMesh>().text = "";
		GameObject.Find("tm_tecla_31").GetComponent<TextMesh>().text = "";
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator  func_proximo (float delay) 
	{
		int idx_verbete_sorteado=0;
		string scramble_str_verbete, str_verbete;
		
		idx_verbete_sorteado = Random.Range(1, nro_verbetes_VL);
		
		str_verbete = dicionario_VL[idx_verbete_sorteado].verbete;
		
		GameObject.Find("!_verbete_dica_plus_plus").transform.position = GameObject.Find("!_fora_tela").transform.position;		 
		if(str_verbete.Length <= 16)
		{	
			scramble_str_verbete = func_scramble_verbete(str_verbete);
			scramble_str_verbete_VL = scramble_str_verbete;
			
			GameObject.Find("Tex_verbete").GetComponent<Text>().text = str_verbete;
			GameObject.Find("Tex_verbete_sombra").GetComponent<Text>().text = str_verbete;		
			//GameObject.Find("Tex_verbete_scramble").GetComponent<Text>().text = scramble_str_verbete;
			
			if(GameObject.Find("Toggle_dica").GetComponent<Toggle>().isOn == true)
			{
				GameObject.Find("Tex_dica").GetComponent<Text>().text = dicionario_VL[idx_verbete_sorteado].significado;	
			}	
			else		
			{
				GameObject.Find("Tex_dica").GetComponent<Text>().text = "";					
			}	
			ja_pisquei_VL=0;
			montagem_palavra_no_teclado("                               ");
			esvazia_letras();
			todas_teclas_cima(1);		
			todas_teclas_pretas(1);		
			state_tecla_01_VL=0;
			state_tecla_02_VL=0;
			state_tecla_03_VL=0;
			state_tecla_04_VL=0;
			state_tecla_05_VL=0;
			state_tecla_06_VL=0;
			state_tecla_07_VL=0;
			state_tecla_08_VL=0;
			state_tecla_09_VL=0;
			state_tecla_10_VL=0;
			state_tecla_11_VL=0;
			state_tecla_12_VL=0;
			state_tecla_13_VL=0;
			state_tecla_14_VL=0;
			state_tecla_15_VL=0;
			state_tecla_16_VL=0;
			state_tecla_17_VL=0;
			state_tecla_18_VL=0;
			state_tecla_19_VL=0;
			state_tecla_20_VL=0;
			state_tecla_21_VL=0;
			state_tecla_22_VL=0;
			state_tecla_23_VL=0;
			state_tecla_24_VL=0;
			state_tecla_25_VL=0;
			state_tecla_26_VL=0;
			state_tecla_27_VL=0;
			state_tecla_28_VL=0;
			state_tecla_29_VL=0;
			state_tecla_30_VL=0;
			state_tecla_31_VL=0;	
			state_tecla_40_VL=0;
			state_tecla_50_VL=0;
			state_tecla_60_VL=0;			
			state_tecla_70_VL=0;
			state_tecla_80_VL=0;
			state_tecla_90_VL=0;		
			montagem_palavra_no_teclado(scramble_str_verbete);		
			GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = "";
		
			if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true)
			{
				AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
				audio.Play();
				//				AudioMaster_acertou
			}				
			GameObject.Find("Tex_verbete_sombra_plus_plus").GetComponent<Text>().text = "A primeira letra é:[" +GameObject.Find("Tex_verbete").GetComponent<Text>().text.Substring(0,1).ToUpper() + "]";
			GameObject.Find("Tex_verbete_plus_plus").GetComponent<Text>().text = "A primeira letra é:[" +GameObject.Find("Tex_verbete").GetComponent<Text>().text.Substring(0,1).ToUpper() + "]";		
			yield return new WaitForSeconds(delay);				
		}
		else			
		{
			StartCoroutine(func_proximo(1f));
		}	
		/*
	  int idx_verbete_sorteado=0;
		string scramble_str_verbete, str_verbete;
		
		idx_verbete_sorteado = Random.Range(1, nro_verbetes_VL);
		
		str_verbete = dicionario_VL[idx_verbete_sorteado].verbete;
		scramble_str_verbete = func_scramble_verbete(str_verbete);
		scramble_str_verbete_VL = scramble_str_verbete;
		
		GameObject.Find("Tex_verbete").GetComponent<Text>().text = str_verbete;
		GameObject.Find("Tex_verbete_sombra").GetComponent<Text>().text = str_verbete;		
		//GameObject.Find("Tex_verbete_scramble").GetComponent<Text>().text = scramble_str_verbete;
		
		if(GameObject.Find("Toggle_dica").GetComponent<Toggle>().isOn == true)
		{
			GameObject.Find("Tex_dica").GetComponent<Text>().text = dicionario_VL[idx_verbete_sorteado].significado;	
		}	
		else		
		{
			GameObject.Find("Tex_dica").GetComponent<Text>().text = "";					
		}	
		ja_pisquei_VL=0;
		montagem_palavra_no_teclado("                               ");
		esvazia_letras();
		todas_teclas_cima(1);		
		todas_teclas_pretas(1);		
		state_tecla_01_VL=0;
		state_tecla_02_VL=0;
		state_tecla_03_VL=0;
		state_tecla_04_VL=0;
		state_tecla_05_VL=0;
		state_tecla_06_VL=0;
		state_tecla_07_VL=0;
		state_tecla_08_VL=0;
		state_tecla_09_VL=0;
		state_tecla_10_VL=0;
		state_tecla_11_VL=0;
		state_tecla_12_VL=0;
		state_tecla_13_VL=0;
		state_tecla_14_VL=0;
		state_tecla_15_VL=0;
		state_tecla_16_VL=0;
		state_tecla_17_VL=0;
		state_tecla_18_VL=0;
		state_tecla_19_VL=0;
		state_tecla_20_VL=0;
		state_tecla_21_VL=0;
		state_tecla_22_VL=0;
		state_tecla_23_VL=0;
		state_tecla_24_VL=0;
		state_tecla_25_VL=0;
		state_tecla_26_VL=0;
		state_tecla_27_VL=0;
		state_tecla_28_VL=0;
		state_tecla_29_VL=0;
		state_tecla_30_VL=0;
		state_tecla_31_VL=0;	
		state_tecla_40_VL=0;
		state_tecla_50_VL=0;
		state_tecla_60_VL=0;			
		state_tecla_70_VL=0;
		state_tecla_80_VL=0;
		state_tecla_90_VL=0;		
		montagem_palavra_no_teclado(scramble_str_verbete);		
		GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = "";
	
		if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true)
		{
			AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
			audio.Play();
		}				
		GameObject.Find("Tex_verbete_sombra_plus_plus").GetComponent<Text>().text = "A primeira letra é:[" +GameObject.Find("Tex_verbete").GetComponent<Text>().text.Substring(0,1).ToUpper() + "]";
		GameObject.Find("Tex_verbete_plus_plus").GetComponent<Text>().text = "A primeira letra é:[" +GameObject.Find("Tex_verbete").GetComponent<Text>().text.Substring(0,1).ToUpper() + "]";
    yield return new WaitForSeconds(delay);				
		*/
	}		
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator limpar_teclado(float delay) 
	{
		GameObject.Find("mesh_tecla_60").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("tecla_dupla_01_fbx_60").GetComponent<Animation>().Play("armature_TECLA|anim_down");
		state_tecla_60_VL=1;
		GameObject.Find("mesh_tecla_60").GetComponent<Renderer>().material = mat_tecla_red;
		GameObject.Find("tecla_dupla_01_fbx_60").GetComponent<Animation>().Play("armature_TECLA|anim_down");
		montagem_palavra_no_teclado("                               ");
		esvazia_letras();
		montagem_palavra_no_teclado(scramble_str_verbete_VL);
		todas_teclas_cima(0);	
		todas_teclas_pretas(0);					
		state_tecla_01_VL=0;
		state_tecla_02_VL=0;
		state_tecla_03_VL=0;
		state_tecla_04_VL=0;
		state_tecla_05_VL=0;
		state_tecla_06_VL=0;
		state_tecla_07_VL=0;
		state_tecla_08_VL=0;
		state_tecla_09_VL=0;
		state_tecla_10_VL=0;
		state_tecla_11_VL=0;
		state_tecla_12_VL=0;
		state_tecla_13_VL=0;
		state_tecla_14_VL=0;
		state_tecla_15_VL=0;
		state_tecla_16_VL=0;
		state_tecla_17_VL=0;
		state_tecla_18_VL=0;
		state_tecla_19_VL=0;
		state_tecla_20_VL=0;
		state_tecla_21_VL=0;
		state_tecla_22_VL=0;
		state_tecla_23_VL=0;
		state_tecla_24_VL=0;
		state_tecla_25_VL=0;
		state_tecla_26_VL=0;
		state_tecla_27_VL=0;
		state_tecla_28_VL=0;
		state_tecla_29_VL=0;
		state_tecla_30_VL=0;
		state_tecla_31_VL=0;
		//state_tecla_70_VL=0;
		state_tecla_80_VL=0;
		state_tecla_90_VL=0;		
		GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text="";		 
		if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true)
		{
			AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
			audio.Play();
		}				 
		state_tecla_60_VL=0;		

		GameObject.Find("mesh_tecla_60").GetComponent<Renderer>().material = mat_tecla_normal;
		GameObject.Find("tecla_dupla_01_fbx_60").GetComponent<Animation>().Play("armature_TECLA|anim_up");				

     yield return new WaitForSeconds(delay);				
  }	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	void muda_estado_tecla(string nro_tecla)
	{
		int toca_som=0;
		//---- RANK
		if(nro_tecla=="40")
		{
			if(state_tecla_40_VL==0)
			{
				state_tecla_40_VL=1;
				GameObject.Find("mesh_tecla_40").GetComponent<Renderer>().material = mat_tecla_red;
				GameObject.Find("tecla_dupla_01_fbx_40").GetComponent<Animation>().Play("armature_TECLA|anim_down");
				carrega_ranking();
			}
		}	
		//---- COFRE
		if(nro_tecla=="50")
		{
			if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true)
			{
				AudioSource audio = GameObject.Find("AudioMaster_erro_01").GetComponent<AudioSource>();
				audio.Play();
			}	
			if(tecla_01_VL==1 && tecla_15_VL==1 && tecla_16_VL==1)
			{
				if(state_tecla_50_VL==0)
				{
					state_tecla_50_VL=1;
					tecla_01_VL=0;
					tecla_15_VL=0;
					tecla_16_VL=0;
					GameObject.Find("mesh_tecla_50").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_dupla_01_fbx_50").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					SceneManager.LoadScene("config");		
				}
			}
			else
			{
				if(tecla_01_VL==1 && tecla_16_VL==1)
				{
					tecla_01_VL=0;
					tecla_15_VL=0;
					tecla_16_VL=0;
					GameObject.Find("RawImage_log").transform.position = GameObject.Find("!_centro_tela").transform.position; 		
				}
			}		
		}	
		//---- LIMPAR
		if(nro_tecla=="60")
		{
			tecla_01_VL=0;
			tecla_15_VL=0;
      tecla_16_VL=0;

			if(state_tecla_60_VL==0)
			{
				state_tecla_60_VL=1;
				StartCoroutine(limpar_teclado(1f));
			}
		}	
		//---- DICA++
		if(nro_tecla=="70")
		{
			if(state_tecla_70_VL==0)
			{
				state_tecla_70_VL=1;
				GameObject.Find("mesh_tecla_70").GetComponent<Renderer>().material = mat_tecla_red;
				GameObject.Find("tecla_dupla_01_fbx_70").GetComponent<Animation>().Play("armature_TECLA|anim_down");
				StartCoroutine(mostra_dica_plus_plus(3f));
				
				
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}				
			}
		}	
		//---- PISCA
		if(nro_tecla=="80")
		{
			if(state_tecla_80_VL==0)
			{
				state_tecla_80_VL=1;
				GameObject.Find("mesh_tecla_80").GetComponent<Renderer>().material = mat_tecla_red;
				GameObject.Find("tecla_dupla_01_fbx_80").GetComponent<Animation>().Play("armature_TECLA|anim_down");
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}					
				pisca_verbete();				
			}
		}			
		//---- PRÓXIMO
		if(nro_tecla=="90")
		{
			tecla_01_VL=0;
			tecla_15_VL=0;
      tecla_16_VL=0;
			if(state_tecla_90_VL==0)
			{
				state_tecla_90_VL=1;
				GameObject.Find("mesh_tecla_90").GetComponent<Renderer>().material = mat_tecla_red;
				GameObject.Find("tecla_dupla_01_fbx_90").GetComponent<Animation>().Play("armature_TECLA|anim_down");

				StartCoroutine(func_proximo(1f));
			}
		}			
		
		
		
		
		
		
		//---- início tratamento das teclas
		if(nro_tecla=="01")
		{
			tecla_01_VL=1;
			if(GameObject.Find("tm_tecla_01").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_01_VL==0)
				{
					state_tecla_01_VL=1;
					GameObject.Find("mesh_tecla_01").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_01").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_01").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_01").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_01").GetComponent<TextMesh>().text)
					{
						state_tecla_01_VL=0;
						GameObject.Find("mesh_tecla_01").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_01").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="02")
		{
			if(GameObject.Find("tm_tecla_02").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_02_VL==0)
				{
					state_tecla_02_VL=1;
					GameObject.Find("mesh_tecla_02").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_02").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_02").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_02").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_02").GetComponent<TextMesh>().text)
					{
						state_tecla_02_VL=0;
						GameObject.Find("mesh_tecla_02").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_02").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="03")
		{
			if(GameObject.Find("tm_tecla_03").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_03_VL==0)
				{
					state_tecla_03_VL=1;
					GameObject.Find("mesh_tecla_03").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_03").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_03").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_03").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_03").GetComponent<TextMesh>().text)
					{
						state_tecla_03_VL=0;
						GameObject.Find("mesh_tecla_03").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_03").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="04")
		{
			if(GameObject.Find("tm_tecla_04").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_04_VL==0)
				{
					state_tecla_04_VL=1;
					GameObject.Find("mesh_tecla_04").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_04").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_04").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_04").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_04").GetComponent<TextMesh>().text)
					{
						state_tecla_04_VL=0;
						GameObject.Find("mesh_tecla_04").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_04").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="05")
		{
			if(GameObject.Find("tm_tecla_05").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_05_VL==0)
				{
					state_tecla_05_VL=1;
					GameObject.Find("mesh_tecla_05").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_05").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_05").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_05").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_05").GetComponent<TextMesh>().text)
					{
						state_tecla_05_VL=0;
						GameObject.Find("mesh_tecla_05").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_05").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="06")
		{
			if(GameObject.Find("tm_tecla_06").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_06_VL==0)
				{
					state_tecla_06_VL=1;
					GameObject.Find("mesh_tecla_06").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_06").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_06").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_06").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_06").GetComponent<TextMesh>().text)
					{
						state_tecla_06_VL=0;
						GameObject.Find("mesh_tecla_06").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_06").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="07")
		{
			if(GameObject.Find("tm_tecla_07").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_07_VL==0)
				{
					state_tecla_07_VL=1;
					GameObject.Find("mesh_tecla_07").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_07").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_07").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_07").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_07").GetComponent<TextMesh>().text)
					{
						state_tecla_07_VL=0;
						GameObject.Find("mesh_tecla_07").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_07").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="08")
		{
			if(GameObject.Find("tm_tecla_08").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_08_VL==0)
				{
					state_tecla_08_VL=1;
					GameObject.Find("mesh_tecla_08").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_08").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_08").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_08").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_08").GetComponent<TextMesh>().text)
					{
						state_tecla_08_VL=0;
						GameObject.Find("mesh_tecla_08").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_08").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="09")
		{
			if(GameObject.Find("tm_tecla_09").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_09_VL==0)
				{
					state_tecla_09_VL=1;
					GameObject.Find("mesh_tecla_09").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_09").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_09").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_09").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_09").GetComponent<TextMesh>().text)
					{
						state_tecla_09_VL=0;
						GameObject.Find("mesh_tecla_09").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_09").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="10")
		{
			if(GameObject.Find("tm_tecla_10").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_10_VL==0)
				{
					state_tecla_10_VL=1;
					GameObject.Find("mesh_tecla_10").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_10").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_10").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_10").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_10").GetComponent<TextMesh>().text)
					{
						state_tecla_10_VL=0;
						GameObject.Find("mesh_tecla_10").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_10").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="11")
		{
			if(GameObject.Find("tm_tecla_11").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_11_VL==0)
				{
					state_tecla_11_VL=1;
					GameObject.Find("mesh_tecla_11").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_11").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_11").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_11").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_11").GetComponent<TextMesh>().text)
					{
						state_tecla_11_VL=0;
						GameObject.Find("mesh_tecla_11").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_11").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="12")
		{
			if(GameObject.Find("tm_tecla_12").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_12_VL==0)
				{
					state_tecla_12_VL=1;
					GameObject.Find("mesh_tecla_12").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_12").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_12").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_12").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_12").GetComponent<TextMesh>().text)
					{
						state_tecla_12_VL=0;
						GameObject.Find("mesh_tecla_12").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_12").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="13")
		{
			if(GameObject.Find("tm_tecla_13").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_13_VL==0)
				{
					state_tecla_13_VL=1;
					GameObject.Find("mesh_tecla_13").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_13").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_13").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_13").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_13").GetComponent<TextMesh>().text)
					{
						state_tecla_13_VL=0;
						GameObject.Find("mesh_tecla_13").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_13").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="14")
		{
			if(GameObject.Find("tm_tecla_14").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_14_VL==0)
				{
					state_tecla_14_VL=1;
					GameObject.Find("mesh_tecla_14").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_14").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_14").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_14").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_14").GetComponent<TextMesh>().text)
					{
						state_tecla_14_VL=0;
						GameObject.Find("mesh_tecla_14").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_14").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="15")
		{
			tecla_15_VL=1;
			if(GameObject.Find("tm_tecla_15").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_15_VL==0)
				{
					state_tecla_15_VL=1;
					GameObject.Find("mesh_tecla_15").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_15").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_15").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_15").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_15").GetComponent<TextMesh>().text)
					{
						state_tecla_15_VL=0;
						GameObject.Find("mesh_tecla_15").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_15").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="16")
		{
			tecla_16_VL=1;
			if(GameObject.Find("tm_tecla_16").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_16_VL==0)
				{
					state_tecla_16_VL=1;
					GameObject.Find("mesh_tecla_16").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_16").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_16").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_16").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_16").GetComponent<TextMesh>().text)
					{
						state_tecla_16_VL=0;
						GameObject.Find("mesh_tecla_16").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_16").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="17")
		{
			if(GameObject.Find("tm_tecla_17").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_17_VL==0)
				{
					state_tecla_17_VL=1;
					GameObject.Find("mesh_tecla_17").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_17").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_17").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_17").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_17").GetComponent<TextMesh>().text)
					{
						state_tecla_17_VL=0;
						GameObject.Find("mesh_tecla_17").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_17").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="18")
		{
			if(GameObject.Find("tm_tecla_18").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_18_VL==0)
				{
					state_tecla_18_VL=1;
					GameObject.Find("mesh_tecla_18").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_18").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_18").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_18").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_18").GetComponent<TextMesh>().text)
					{
						state_tecla_18_VL=0;
						GameObject.Find("mesh_tecla_18").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_18").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="19")
		{
			if(GameObject.Find("tm_tecla_19").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_19_VL==0)
				{
					state_tecla_19_VL=1;
					GameObject.Find("mesh_tecla_19").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_19").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_19").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_19").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_19").GetComponent<TextMesh>().text)
					{
						state_tecla_19_VL=0;
						GameObject.Find("mesh_tecla_19").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_19").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="20")
		{
			if(GameObject.Find("tm_tecla_20").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_20_VL==0)
				{
					state_tecla_20_VL=1;
					GameObject.Find("mesh_tecla_20").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_20").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_20").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_20").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_20").GetComponent<TextMesh>().text)
					{
						state_tecla_20_VL=0;
						GameObject.Find("mesh_tecla_20").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_20").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="21")
		{
			if(GameObject.Find("tm_tecla_21").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_21_VL==0)
				{
					state_tecla_21_VL=1;
					GameObject.Find("mesh_tecla_21").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_21").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_21").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_21").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_21").GetComponent<TextMesh>().text)
					{
						state_tecla_21_VL=0;
						GameObject.Find("mesh_tecla_21").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_21").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="22")
		{
			if(GameObject.Find("tm_tecla_22").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_22_VL==0)
				{
					state_tecla_22_VL=1;
					GameObject.Find("mesh_tecla_22").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_22").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_22").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_22").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_22").GetComponent<TextMesh>().text)
					{
						state_tecla_22_VL=0;
						GameObject.Find("mesh_tecla_22").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_22").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="23")
		{
			if(GameObject.Find("tm_tecla_23").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_23_VL==0)
				{
					state_tecla_23_VL=1;
					GameObject.Find("mesh_tecla_23").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_23").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_23").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_23").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_23").GetComponent<TextMesh>().text)
					{
						state_tecla_23_VL=0;
						GameObject.Find("mesh_tecla_23").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_23").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="24")
		{
			if(GameObject.Find("tm_tecla_24").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_24_VL==0)
				{
					state_tecla_24_VL=1;
					GameObject.Find("mesh_tecla_24").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_24").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_24").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_24").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_24").GetComponent<TextMesh>().text)
					{
						state_tecla_24_VL=0;
						GameObject.Find("mesh_tecla_24").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_24").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="25")
		{
			if(GameObject.Find("tm_tecla_25").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_25_VL==0)
				{
					state_tecla_25_VL=1;
					GameObject.Find("mesh_tecla_25").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_25").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_25").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_25").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_25").GetComponent<TextMesh>().text)
					{
						state_tecla_25_VL=0;
						GameObject.Find("mesh_tecla_25").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_25").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="26")
		{
			if(GameObject.Find("tm_tecla_26").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_26_VL==0)
				{
					state_tecla_26_VL=1;
					GameObject.Find("mesh_tecla_26").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_26").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_26").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_26").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_26").GetComponent<TextMesh>().text)
					{
						state_tecla_26_VL=0;
						GameObject.Find("mesh_tecla_26").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_26").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="27")
		{
			if(GameObject.Find("tm_tecla_27").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_27_VL==0)
				{
					state_tecla_27_VL=1;
					GameObject.Find("mesh_tecla_27").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_27").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_27").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_27").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_27").GetComponent<TextMesh>().text)
					{
						state_tecla_27_VL=0;
						GameObject.Find("mesh_tecla_27").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_27").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="28")
		{
			if(GameObject.Find("tm_tecla_28").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_28_VL==0)
				{
					state_tecla_28_VL=1;
					GameObject.Find("mesh_tecla_28").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_28").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_28").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_28").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_28").GetComponent<TextMesh>().text)
					{
						state_tecla_28_VL=0;
						GameObject.Find("mesh_tecla_28").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_28").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="29")
		{
			if(GameObject.Find("tm_tecla_29").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_29_VL==0)
				{
					state_tecla_29_VL=1;
					GameObject.Find("mesh_tecla_29").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_29").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_29").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_29").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_29").GetComponent<TextMesh>().text)
					{
						state_tecla_29_VL=0;
						GameObject.Find("mesh_tecla_29").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_29").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="30")
		{
			if(GameObject.Find("tm_tecla_30").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_30_VL==0)
				{
					state_tecla_30_VL=1;
					GameObject.Find("mesh_tecla_30").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_30").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_30").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_30").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_30").GetComponent<TextMesh>().text)
					{
						state_tecla_30_VL=0;
						GameObject.Find("mesh_tecla_30").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_30").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
		if(nro_tecla=="31")
		{
			if(GameObject.Find("tm_tecla_31").GetComponent<TextMesh>().text != "")
			{
				if(state_tecla_31_VL==0)
				{
					state_tecla_31_VL=1;
					GameObject.Find("mesh_tecla_31").GetComponent<Renderer>().material = mat_tecla_red;
					GameObject.Find("tecla_01_fbx_31").GetComponent<Animation>().Play("armature_TECLA|anim_down");
					GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text + GameObject.Find("tm_tecla_31").GetComponent<TextMesh>().text;
					Ultima_Tecla_Pressionada_VL = GameObject.Find("tm_tecla_31").GetComponent<TextMesh>().text;
					toca_som=1;
				}
				else
				{
					if(Ultima_Tecla_Pressionada_VL == GameObject.Find("tm_tecla_31").GetComponent<TextMesh>().text)
					{
						state_tecla_31_VL=0;
						GameObject.Find("mesh_tecla_31").GetComponent<Renderer>().material = mat_tecla_normal;
						GameObject.Find("tecla_01_fbx_31").GetComponent<Animation>().Play("armature_TECLA|anim_up");
						GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text = GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Substring(0,GameObject.Find("Tex_verbete_decifrado").GetComponent<Text>().text.Length-1);
						toca_som=1;
					}
				}
				if(GameObject.Find("Toggle_som").GetComponent<Toggle>().isOn == true && toca_som == 1)
				{
					AudioSource audio = GameObject.Find("AudioMaster_pressiona_tecla").GetComponent<AudioSource>();
					audio.Play();
				}
			}
		}
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public string Gera_Ramdom_String(int length) 
	{
		string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string returnString = "";
		
		for (int i = 0; i < length; i++) 
		{
			 returnString += chars[ UnityEngine.Random.Range(0, chars.Length) ];
		}
		return returnString;
	}	
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public void fechar_tela_log() 
	{
		GameObject.Find("RawImage_log").transform.position = GameObject.Find("!_fora_tela").transform.position; 		
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
		//--------------------------------------------------------------------------------------------------------------------------------------------------------------------	
	public void debug_log(string mensagem) 
	{
		if(dl_VL==1)
		{	
		  Debug.Log(mensagem);
			GameObject.Find("Text_log").GetComponent<Text>().text = GameObject.Find("Text_log").GetComponent<Text>().text + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ":> "+ mensagem + "\n";
		}	
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------		
	public void mostra_mensagem(string msg, int botao)
	{
		debug_log("mostra_mensagem: "+msg);
		if(botao==1)
		{
			GameObject.Find("Button_sair_msg").GetComponent<Button>().enabled = true;
			GameObject.Find("Button_sair_msg").transform.localScale = new Vector3(1,1,1);
		}	
		else
		{
			GameObject.Find("Button_sair_msg").GetComponent<Button>().enabled = false;
			GameObject.Find("Button_sair_msg").transform.localScale = new Vector3(0,0,0);
		}	
			
		GameObject.Find("Text_mensagem_principal").GetComponent<Text>().text = msg;
		GameObject.Find("RawImage_MSG").transform.position = GameObject.Find("!_centro_tela").transform.position; 		
	}		
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------		
	public void esconde_mensagem(string msg)
	{
		GameObject.Find("Text_mensagem_principal").GetComponent<Text>().text = msg;
		GameObject.Find("RawImage_MSG").transform.position = GameObject.Find("!_fora_tela").transform.position; 		
	}		
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------
	
}
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------