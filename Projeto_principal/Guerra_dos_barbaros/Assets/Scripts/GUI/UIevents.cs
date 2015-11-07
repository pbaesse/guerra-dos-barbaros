using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEditor;
using System.IO;


public class UIevents : MonoBehaviour {

	bool menu_contrucao_ativo = false;
	bool ativar_pause = false;
	public bool botao_ = false;
	bool ativar_tela_objetivos = false;
	bool novo_save = false;
	GameObject[] items;
	GameObject[] no_jogo;
	GameObject[] botoes;
	GameObject construcao;
	GameObject construcao_selecionado = null;
	GameObject[] toggle_construidos;
	string botao_nome = "";
	private GameObject construcao_sele_ = null;
	public GameObject menu_construcao;
	public GameObject menu_aldeao;
	public GameObject botao_abrir_menu_construcao;
	public GameObject tela_de_escolha;
	public GameObject menu_ingame ;
	public GameObject botao_lanceiro;
	public GameObject botao_aldeao;
	public InputField nome_save;
	public GameObject menu_superior;
	public ToggleGroup lista_saves;
	public ToggleGroup lista_saves_2;
	public GameObject tela_carregar;
	public Toggle saves_prefabs;
	public EventSystem evente;
	public Image obj_ao_iniciar;
	public GameObject obj_persi;
	public GameObject botao_obj;
	public Text texto;
	static bool insuf= false;
	


	// Use this for initialization

	void Start () {
 		items = Resources.LoadAll<GameObject> ("prefabs");
		tela_de_escolha.SetActive (false);
		menu_aldeao.SetActive (false);
		botao_lanceiro.SetActive(false);
		botao_obj.GetComponent< Button > ().interactable = false;
		botao_aldeao.SetActive(false);
		menu_ingame.transform.SetAsLastSibling();
		obj_ao_iniciar.CrossFadeAlpha(0,7,false);
		obj_ao_iniciar.transform.FindChild("Objetivos_ao_iniciar").GetComponent< Text > ().CrossFadeAlpha(0,7,false);
		StartCoroutine(esp());
//		r = GameObject.Find ("Panel1").GetComponent<RectTransform>();
	}
	
	
	void Update()
	{
		
		if(vitoria())
		{
		
			Application.LoadLevel("vitoria");			
		}
		menu_superior.transform.FindChild("habitantes").GetComponent<Text>().text= "Habitantes: " + controlador_quantidades.quantidade_habitantes.ToString()+"/"+controlador_quantidades.quantidade_habitantes_max.ToString();
		menu_superior.transform.FindChild("madeira").GetComponent<Text>().text ="Madeira : " + controlador_quantidades.quantidade_madeira.ToString();
		menu_superior.transform.FindChild("comida").GetComponent<Text>().text ="Comida : " + controlador_quantidades.quantidade_comida.ToString();
		if (mouse_no_menu()&& construcao != null && !construcao.GetComponent<BuildingPlacement> ().escolheu_local)
						Destroy (construcao);
		
		
//		Debug.Log (evente.IsPointerOverGameObject()+"  "+r.position);

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{ 
				abrir_tela_pause();
//				Debug.Log(ativar_pause);
		}
	
		
		if(ativar_pause || ativar_tela_objetivos)
			Time.timeScale  = 0f;
		
		else
			Time.timeScale = 1f;
		Terrain te;
	
		menu_ingame.SetActive (ativar_pause);
		menu_construcao.SetActive (menu_contrucao_ativo);
		botao_abrir_menu_construcao.SetActive (!menu_contrucao_ativo);
		obj_persi.SetActive(ativar_tela_objetivos);

	
		}
	private  bool vitoria()
	{
	Debug.Log("lanceiros: "+ controlador_quantidades.quantidade_lanceiro );
		if(controlador_quantidades.quantidade_comida >=100 && controlador_quantidades.quantidade_habitantes_max >= 15 && controlador_quantidades.quantidade_habitantes >= 10 && controlador_quantidades.quantidade_lanceiro >= 6 && controlador_quantidades.quantidade_madeira >= 100)
			return true;
		else 
			return false;
	}
	
	public void abrir_tela_pause()
	{
		ativar_pause = !ativar_pause;
	}
	public void abrir_objetivos()
	{
		ativar_tela_objetivos = !ativar_tela_objetivos;
	}
	public void menu_principal()
	{
		Application.LoadLevel ("Menu");
	}

	public void abrir_menu_construcao () {
		menu_contrucao_ativo = !menu_contrucao_ativo;
			//	GUIElement.Instantiate(GUILayout.Button ("um", GUILayout.Height (50f), GUILayout.Width (50f)),);
		}
	public void abrir_menu_construcao (bool abr) {
		menu_contrucao_ativo = abr;
		//	GUIElement.Instantiate(GUILayout.Button ("um", GUILayout.Height (50f), GUILayout.Width (50f)),);
	}
	public void construcao_fantasma(string na)
	{
		if(controlador_quantidades.quantidade_madeira>= 40)
		{
		   if (!botao_) {
						foreach (GameObject a in items) {
//								Debug.Log ("FFAF");
								if (a.name == na) {
										botao_ = true;
										botao_nome = a.name;
										//construcao = Instantiate (a, new Vector3 (Camera.main.transform.position.x, 0, Camera.main.transform.position.z), new Quaternion (0, 0, 0, 0)) as GameObject;
										
								
					foreach (GameObject unidades in quadrado.Unidades_selecionadas) 
					{
						
						unidades.GetComponent<unidades1>().set_status_contrucao_fantasma(true);
						unidades.GetComponent<Caminho_unidade2>().set_status_contrucao_fantasma(true);

					}
				
								}
								//	GUIElement.Instantiate(GUILayout.Button ("um", GUILayout.Height (50f), GUILayout.Width (50f)),);
						}
				}


			}
			else
			{
			texto.gameObject.SetActive(true);
			texto.CrossFadeAlpha(1f,0f,false);
			texto.text = "Quantidade de madeira insuficiente";
			texto.CrossFadeAlpha(0f,3f,false);
			insuf = false;
			StartCoroutine(esperar());
			}
			
	}
	public void reconstrucao_fantasma()
	{
		if (botao_) {
		Debug.Log("csoi");
			foreach (GameObject a in items) {
//				Debug.Log ("contrucao_ativoFAF");
				if (a.name == botao_nome) {
			
					construcao = Instantiate (a, new Vector3 (Camera.main.transform.position.x, 0, Camera.main.transform.position.z), new Quaternion (0, 0, 0, 0)) as GameObject;
					
					construcao.transform.FindChild ("Cube").GetComponent<Renderer>().material.shader = Shader.Find ("Transparent/Diffuse");
					construcao.transform.FindChild ("Cube").GetComponent<Renderer>().material.color = new Color (0f, 0.8f, 0, 0.3f);
				}
				//	GUIElement.Instantiate(GUILayout.Button ("um", GUILayout.Height (50f), GUILayout.Width (50f)),);
			}
		}
	}
	public void instanciar_unidades(string nome)
	{
		
		Vector3 pos =  new Vector3(Random.Range(construcao_sele_.transform.position.x+20,construcao_sele_.transform.position.x+40),0.08f,Random.Range(construcao_sele_.transform.position.z + 20,construcao_sele_.transform.position.z +40));
		foreach (GameObject a in items) 
		{
			if(a.name == nome)
			{
				if(a.name == "unidade___Lanceiro Jandui")
				{
					if(controlador_quantidades.quantidade_habitantes < controlador_quantidades.quantidade_habitantes_max && controlador_quantidades.quantidade_comida >=40)
					{
					Instantiate (a,pos,new Quaternion (0, 0, 0, 0));			
					controlador_quantidades.quantidade_habitantes ++;
					controlador_quantidades.quantidade_comida -= 40;
						menu_superior.transform.FindChild("habitantes").GetComponent<Text>().text = "Habitantes : "+controlador_quantidades.quantidade_habitantes.ToString()+"/"+controlador_quantidades.quantidade_habitantes_max.ToString();
						menu_superior.transform.FindChild("comida").GetComponent<Text>().text = "Comida : "+controlador_quantidades.quantidade_comida.ToString();
						controlador_quantidades.quantidade_lanceiro += 1;
						
					}
					else if(controlador_quantidades.quantidade_habitantes == controlador_quantidades.quantidade_habitantes_max){
						texto.gameObject.SetActive(true);
						texto.CrossFadeAlpha(1f,0f,false);
						texto.text = "Quantidade máxima atingida, construa mais casas !!!";
						texto.CrossFadeAlpha(0f,3f,false);
						StartCoroutine(esperar());
						}
					else if(controlador_quantidades.quantidade_comida < 40){
						texto.gameObject.SetActive(true);
						texto.CrossFadeAlpha(1f,0f,false);
						texto.text = "Quantidade de comida insuficiente";
						texto.CrossFadeAlpha(0f,3f,false);
						StartCoroutine(esperar());
						
						}
				}
				if(a.name == "unidade___Aldeiao_Jandui")
				{
					
					if(controlador_quantidades.quantidade_habitantes < controlador_quantidades.quantidade_habitantes_max && controlador_quantidades.quantidade_comida >=20)
					{
						GameObject obj =Instantiate (a,pos,new Quaternion (0, 0, 0, 0)) as GameObject;			
						obj.name = "unidade___Aldeiao_Jandui";
						
					
						controlador_quantidades.quantidade_habitantes ++;
						controlador_quantidades.quantidade_comida -= 20;
						menu_superior.transform.FindChild("habitantes").GetComponent<Text>().text = "Habitantes : "+controlador_quantidades.quantidade_habitantes.ToString()+"/"+controlador_quantidades.quantidade_habitantes_max.ToString();
						menu_superior.transform.FindChild("comida").GetComponent<Text>().text = "Comida : "+controlador_quantidades.quantidade_comida.ToString();
						
					}
					else if(controlador_quantidades.quantidade_habitantes == controlador_quantidades.quantidade_habitantes_max){
						texto.gameObject.SetActive(true);
						texto.CrossFadeAlpha(1f,0f,false);
						texto.text = "Quantidade máxima atingida, construa mais casas !!!";
						texto.CrossFadeAlpha(0f,3f,false);
						StartCoroutine(esperar());
					}
					else if(controlador_quantidades.quantidade_comida < 40){
						texto.gameObject.SetActive(true);
						texto.CrossFadeAlpha(1f,0f,false);
						texto.text = "Quantidade de comida insuficiente";
						texto.CrossFadeAlpha(0f,3f,false);
						StartCoroutine(esperar());
						
					}
				}
			}
		}
		
	}
	public void construcao_selecionada(GameObject c)
	{

		construcao_sele_ = c;
	}
	public void ativar_menu_quartel(bool ativar)
	{
	botao_lanceiro.SetActive(ativar);
	if(!ativar)
	construcao = null;
	}
	public void ativar_menu_centro(bool ativar)
	{
		botao_aldeao.SetActive(ativar);
		if(!ativar)
			construcao = null;
	}
	public bool mouse_no_menu()
	{
		return evente.IsPointerOverGameObject () ;
	}
	public static void verificar_qt_madeira()
	{
		insuf = true;
	}
	IEnumerator esperar()
	{
		yield return new WaitForSeconds(7f);
		texto.gameObject.SetActive(false);
	}
		
	IEnumerator esp()
	{
		yield return new WaitForSeconds(7f);
		obj_ao_iniciar.transform.parent = obj_persi.transform;
		obj_persi.transform.FindChild("Voltar").transform.parent = obj_ao_iniciar.transform;
		obj_persi.gameObject.SetActive(false); 
		botao_obj.GetComponent< Button > ().interactable = true;
		
	}

}


