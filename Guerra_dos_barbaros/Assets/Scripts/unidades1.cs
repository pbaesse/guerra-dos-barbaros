using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
//
public class unidades1 : MonoBehaviour {
	// para o mouse
	public static List<GameObject> unidades_inimigas ;
	public GameObject machado;
	public  bool selecionado = false;
	private bool salva_f1 = false;
	private bool salva_f2 = false;
	private bool construcao_status = false;
	private bool atacando = false;
	public int vida;
	public int tempo_de_ataque;
	public int forca ;
	Ray raio;
	RaycastHit bat;
	private GameObject alvo;
	Vector3 posi_atual = Vector3.zero;
	bool tem_inimigo = false;


	void Start()
	{
		transform.FindChild ("projetor").gameObject.SetActive(false);
		if(transform.name == "unidade___Aldeiao_Jandui")
		machado.SetActive(false);
	}
	void LateUpdate () 
	{
		verifica_morte ();
		atacar_inimigo();
		atacar_inimigo_perto ();
	
//		animation.Play ("correndo");


	}
	public void set_status_contrucao_fantasma(bool status)
	{
		construcao_status = status;		
	}
	public  void OnSelected()
	{
			if (!construcao_status ) {
						if (!Input.GetKey (KeyCode.LeftControl)) {
								selecionado = true;
		
				transform.FindChild ("projetor").gameObject.SetActive(true);
								//Debug.Log("selecionado");
								Camera.main.SendMessage ("add_unidade_selecionada", gameObject, SendMessageOptions.DontRequireReceiver);
								
		

		
						}
						if (Input.GetKey (KeyCode.LeftControl) && selecionado) {

								Debug.Log ("dessele");
								selecionado = false;
				transform.FindChild ("projetor").gameObject.SetActive(false);
								Camera.main.SendMessage ("remover_unidade_selecionada", gameObject, SendMessageOptions.DontRequireReceiver);
						}
		//Mouse.unidades_selecionandas.Add(this.transform.gameObject);
		else if (Input.GetKey (KeyCode.LeftControl) && !selecionado) {
								selecionado = true;
				transform.FindChild ("projetor").gameObject.SetActive(true);
								Camera.main.SendMessage ("add_unidade_selecionada", gameObject, SendMessageOptions.DontRequireReceiver);
						}
				}

		//construcao_status = true;
	}
	
	public void OnUnselected()
	{
		if (!construcao_status && selecionado) 
		{

						selecionado = false;

			transform.FindChild ("projetor").gameObject.SetActive(false);
						Camera.main.SendMessage ("remover_unidade_selecionada", gameObject, SendMessageOptions.DontRequireReceiver);
						//Mouse.unidades_selecionandas.Remove(this.transform.gameObject);
		}
		//construcao_status = true;
	}
	public void salvar(KeyCode a)
	{
		if(selecionado && a == KeyCode.F1)
		salva_f1 = true;
		if(selecionado && a == KeyCode.F2)
			salva_f2 = true;
	}
	public void  carregar(KeyCode a)
	{
		if(salva_f1 && a == KeyCode.F6)
		{
			selecionado = true;
			transform.FindChild ("projetor").gameObject.SetActive(true);
		}
		if(salva_f2 && a == KeyCode.F7)
		{
			selecionado = true;
			transform.FindChild ("projetor").gameObject.SetActive(true);
		}
	}
	public void atacar_unidade(int forca)
	{
		//Debug.Log ("atacado");
		vida -= forca;
	}
	void verifica_morte ()
	{
		//Debug.Log (vida+transform.name);
		if(vida <= 0)
		Destroy (this.gameObject);
	}
	public void atacar_inimigo()
	{

		if (Input.GetMouseButtonDown (1)) 
		{
			raio = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (raio, out bat)) 
			{
				if(bat.collider.tag == "inimigo" || bat.collider.tag == "construcoes_inimigo")
				{
					//Debug.Log("inmigo");
					posi_atual = bat.collider.transform.position;
					alvo = bat.collider.transform.gameObject;
					transform.gameObject.SendMessage("pegar_alvo",alvo, SendMessageOptions.DontRequireReceiver);
					transform.gameObject.SendMessage("ir_ate_inimigo",posi_atual, SendMessageOptions.DontRequireReceiver);
					tem_inimigo = true;

				}
				else
				{
					alvo = null;
					tem_inimigo = false;
				}
			}

		}
/*		if(posi_atual != bat.collider.transform.position ){
			posi_atual = bat.collider.transform.position;
			transform.gameObject.SendMessage("ir_ate_inimigo",posi_atual, SendMessageOptions.DontRequireReceiver);
			//Debug.Log("mudou"+ bat.collider.name + posi_atual);
			}
*/
	}	
	public void atacar_inimigo_perto()
	{
		
				unidades_inimigas = new List<GameObject> (GameObject.FindGameObjectsWithTag ("inimigo"));
		
				foreach (GameObject inimigos in unidades_inimigas) 
				{
						float distancia = Vector3.Distance (inimigos.transform.position, this.transform.position);
						//Debug.Log (distancia+ unidade.name);
						if (distancia <= 20 && distancia > 5)
						{	
						transform.gameObject.SendMessage("pegar_alvo",alvo, SendMessageOptions.DontRequireReceiver);
						transform.gameObject.SendMessage("ir_ate_inimigo",posi_atual, SendMessageOptions.DontRequireReceiver);
								
						}
						if (distancia <= 5) 
						{
						//Debug.Log ("PERTO"+ unidade.name);
							if (atacando) 
							{
							inimigos.SendMessage ("atacar_inimigo", forca, SendMessageOptions.DontRequireReceiver);
							atacando = false;
							}
						}
						if(posi_atual != inimigos.transform.position && tem_inimigo){
						posi_atual = bat.collider.transform.position;
						transform.gameObject.SendMessage("ir_ate_inimigo",posi_atual, SendMessageOptions.DontRequireReceiver);
						//Debug.Log("mudou"+ bat.collider.name + posi_atual);
						}

				}
		}
		



	}
	
