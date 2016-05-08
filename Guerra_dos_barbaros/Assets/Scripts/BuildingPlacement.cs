using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class BuildingPlacement : MonoBehaviour {

	public bool escolheu_local = false;
	private bool selecionado = true;
	private bool pode_contruir;
	private bool em_construcao = false;
	public  bool construido = false;
	public 	bool construcao_interrompida= false;
	private Ray ray;
	private RaycastHit hit;
	public GameObject[] buildings;
	public int vida_atual;
	public int vida ;
	public int aumento_de_vida;
	public int delay;
	int num_de_construtores ;
	int num_de_construtores_atual;
	int i ;
	private static List<GameObject> selecionaveis;
	//private BuildingPlacement buildin_placement;
	private string buildingName;
	string a = "";
	internal List<GameObject> aldeioes_selecionados;
	public Text texto;
	

	void Start() {
		//buildin_placement = GetComponent<BuildingPlacement>();
		//construcao = null;
		StartCoroutine (incrementar_construir());
		transform.name = transform.name.Substring(0,transform.name.Length -7);
		transform.FindChild("efeito").gameObject.SetActive(false);
		selecionaveis = new List<GameObject> (GameObject.FindGameObjectsWithTag ("selecionaveis"));
	}
	
	
	void Update () { 
		
	

		if(!construido )
		{	

				if (Input.GetMouseButtonUp (1)  && !escolheu_local ) 
				{
					foreach (GameObject unidades in quadrado.Unidades_selecionadas) 
					{
						
						unidades.GetComponent<unidades1>().set_status_contrucao_fantasma(false);
						unidades.GetComponent<Caminho_unidade2>().set_status_contrucao_fantasma(false);
						quadrado.status = false;				
					}
					
					GameObject.Find("GameObject").GetComponent<UIevents>().botao_ = false;
					Destroy(gameObject);
				}
				num_de_construtores = 0;
				i = 0;
				//	Vector3 m = Input.mousePosition;
				//	m = new Vector3 (m.x, m.y, transform.position.z);
				//	Vector3 p = Camera.main.ScreenToWorldPoint (m);
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) 
				{
					if (!escolheu_local)
					{
						transform.position = new Vector3 (hit.point.x, transform.position.y, hit.point.z);
						quadrado.status = true;
					}
					if (Input.GetMouseButtonDown (0) && pode_contruir && !escolheu_local )
					{
					selecionaveis = new List<GameObject> (GameObject.FindGameObjectsWithTag ("selecionaveis"));
					GameObject.Find("GameObject").GetComponent<UIevents>().botao_ = false;
						escolheu_local = true;  
						controlador_quantidades.quantidade_madeira -= 40;
						quadrado.status = true;		
						Camera.main.GetComponent<quadrado>().limpar_marcador();
					foreach (GameObject aldeoes in quadrado.Unidades_selecionadas) 
					{
							aldeoes.GetComponent<Caminho_unidade2>().SendMessage ("ir_construir",this.gameObject, SendMessageOptions.RequireReceiver);					
							aldeoes.GetComponent<unidades1>().set_status_contrucao_fantasma(true);
					}
					quadrado.status = false;		
					
					
				}
				
				}
			if(vida_atual < vida && num_de_construtores ==0)
			{
				foreach (GameObject unidades in selecionaveis) 
				{
					if(unidades.name.Substring(10) == "Aldeiao_Jandui")
					{			
						if( unidades.GetComponent<Caminho_unidade2>().construindo)
						{
						//unidades.GetComponent<unidades1>().get_status_contrucao_fantasma(true);
							if(unidades.GetComponent<Caminho_unidade2>().cons == gameObject  )
							{
							
								num_de_construtores +=1;
							}
						
						}
						if(num_de_construtores != num_de_construtores_atual)
							num_de_construtores_atual = num_de_construtores;
						
				
					}
				}
		    }
		}
	
	
	}
		
		

	void OnTriggerStay(Collider c) 
	{
		if (!escolheu_local&& c.name != "terra") {
			pode_contruir = false;
			transform.FindChild ("Cube").GetComponent<Renderer>().material.color = new Color (0.8f, 0f, 0, 0.3f);
		} 

	}
	void OnTriggerEnter(Collider c)
	{
		a = c.name;
		if (!escolheu_local && c.name != "terra") {
						pode_contruir = false;
						transform.FindChild ("Cube").GetComponent<Renderer>().material.color = new Color (0.8f, 0f, 0, 0.3f);
				} 
		if (!escolheu_local && c.name == "terra") {
						pode_contruir = true;
			transform.FindChild ("Cube").GetComponent<Renderer>().material.color = new Color (0f, 0.8f, 0, 0.3f);
				}


		
	}
	void OnTriggerExit(Collider c)
	{
		a = c.name;
	if (!escolheu_local) {
						pode_contruir = true;
						transform.FindChild ("Cube").GetComponent<Renderer>().material.color = new Color (0f, 0.8f, 0, 0.3f);
				}
	}


	IEnumerator incrementar_construir()
{
			

			if (!construido && vida_atual + (aumento_de_vida * num_de_construtores) < vida) 
			{
			if(num_de_construtores_atual == 0)
			{
				Debug.Log("entroou");
				
				transform.FindChild("efeito").gameObject.SetActive(false);
			}
			else
			{
			Debug.Log ("aumento:"+aumento_de_vida + "num:" +num_de_construtores_atual);
			vida_atual += aumento_de_vida * num_de_construtores_atual;
				transform.FindChild("efeito").gameObject.SetActive(true);
			Debug.Log ("vida atual:"+vida_atual);
			}
			}
		if(!construido && vida_atual + (aumento_de_vida * num_de_construtores) >= vida)
			{
			vida_atual = vida;
			construido = true;	
			em_construcao = false;
			transform.FindChild("efeito").gameObject.SetActive(false);
			transform.FindChild ("Cube").GetComponent<Renderer> ().material.shader = Shader.Find ("Diffuse");
			transform.FindChild ("Cube").GetComponent<Renderer> ().material.color = Color.white;
			if(transform.name == "construcaoCasa_Jandui")
				controlador_quantidades.quantidade_habitantes_max += 10;
			foreach(GameObject unidade in selecionaveis)
			{
				if(unidade.name.Substring(10) == "Aldeiao_Jandui"){
					if(unidade.GetComponent<Caminho_unidade2>().cons == gameObject)
					{
						unidade.GetComponent<Caminho_unidade2>().construindo = false;
						
					}
					
				}
			}
			}
		

		yield return new WaitForSeconds (delay);
		StartCoroutine (incrementar_construir());

		}
}
