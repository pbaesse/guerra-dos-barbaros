using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System.Linq;

public class Caminho_unidade2: MonoBehaviour {
	
	private Seeker seeker;
	private CharacterController controlador;
	private Path caminho;
	private unidades1 unidade;
	public float velocidade;
	string click = "";
	public float proximo_ponto_do_caminho = 10;
	private int ponto_atual = 0;
	private List < GameObject > recursos;
	private quadrado qua;
	public GameObject centro;
	internal int qt_coletada = 0;
	internal GameObject cons;
	internal GameObject recurso;
	private GameObject proximo_recurso;
	private bool cons_colocada = false;
	private string nome = "";
	private bool construcao_status = false;
	private bool indo_construir = false;
	internal bool indo_coletar = false;
	internal bool indo_deposito = false;
	public GameObject machado;
	internal bool coletando = false;
	internal bool construindo = false;
	Vector3 direcao;
	private float distancia_para_inimigo;
	Vector3 ponto;
	
	
	// Use this for initialization
	void Start() {
		seeker = GetComponent < Seeker > ();
		controlador = GetComponent < CharacterController > ();
		unidade = GetComponent < unidades1 > ();
		centro = GameObject.Find("construcaoCentro_Jandui");
	
		//qua = GetComponent<quadrado>();
	}
	// Update is called once per frame
	void LateUpdate() {
		if(centro == null && transform.name.Substring(10) == "Aldeiao_Jandui")
		{
			centro = GameObject.Find("construcaoCentro_Jandui");
			if(centro == null)
				centro = GameObject.Find("construcaoCentro");
		}
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit bat;
		//		Debug.Log(" qt :"+qt_coletada);
		
		if (Physics.Raycast(r, out bat)) {
			click = bat.collider.tag;
			if (unidade.selecionado) {
				
				{
					if (Input.GetMouseButtonUp(1)) 
					{
						if(transform.name.Substring(10) == "Aldeiao_Jandui")
						machado.SetActive(false);
						
						if (bat.transform.name != "terra") {
							if ((transform.name.Substring(10) != "Aldeiao_Jandui")) {
								ponto = bat.point;
								ponto = new Vector3(Random.Range(ponto.x, ponto.x + 10), 0.08f, Random.Range(ponto.z, ponto.z + 10));
								GetComponent < Animation > ().Play("comecando_correr");
								GetComponent < Animation > ()["correndo"].speed = 1.0f;
								seeker.StartPath(transform.position, ponto, caminho_completo); //determina o caminho
								Debug.Log("correndo");
								coletando = false;
								indo_deposito = false;
								indo_construir = false;
								construindo = false;
								cons = null;
								recurso = null;
							}
							
							if (transform.name.Substring(10) == "Aldeiao_Jandui" && bat.transform.name.Substring(0, 10) == "construcao" ) {
								if ( bat.transform.name == "construcaoCentro"&& qt_coletada >0){
									ponto = bat.point;
									Debug.Log("abes");
									ponto = new Vector3(ponto.x + 15, 0.08f, ponto.z);
									seeker.StartPath(transform.position, ponto, caminho_completo);
									recurso =null;
									machado.SetActive(false);
									cons = null;
									coletando = false;
									indo_coletar = false;
									indo_deposito = true;
								}
								else if( bat.transform.GetComponent < BuildingPlacement > ().vida != bat.transform.GetComponent < BuildingPlacement > ().vida_atual){
									cons = bat.transform.gameObject;
									ponto = bat.point; 
									ponto = new Vector3(ponto.x, 0.08f, Random.Range(ponto.z, ponto.z + 10));
									seeker.StartPath(transform.position, ponto, caminho_completo);
									machado.SetActive(false);
									indo_construir = true;
									indo_deposito = false;
									recurso = null;
									recurso.GetComponent < Recursos > ().diminuir_recurso(false);
								}
								else if( bat.transform.GetComponent < BuildingPlacement > ().vida == bat.transform.GetComponent < BuildingPlacement > ().vida_atual ){
									cons = null;
									ponto = bat.point;
									ponto = new Vector3(Random.Range(ponto.x, ponto.x + 5), 0.08f, Random.Range(ponto.z, ponto.z + 5));
									seeker.StartPath(transform.position, ponto, caminho_completo);
									machado.SetActive(false);
									indo_construir = false;
									indo_deposito = false;
									if(recurso != null)
										recurso.GetComponent < Recursos > ().diminuir_recurso(false);
									recurso = null;
									
								}
								
								
							}
							
							
							if (transform.name.Substring(10) == "Aldeiao_Jandui" && bat.transform.name.Substring(0, 10) == "recurso___") {
								ponto = bat.point;
								Debug.Log("recurso");
								ponto = new Vector3(Random.Range(ponto.x, ponto.x + 10), 0.08f, Random.Range(ponto.z, ponto.z + 10));
								seeker.StartPath(transform.position, ponto, caminho_completo);
								recurso = bat.transform.gameObject;
								cons = null;
								indo_coletar = true;
								indo_deposito = false;
								if((bat.transform.name == "recurso___frutas2" || bat.transform.name == "recurso___frutas")){
								
									if((nome == "arvore"||nome == "arvore3")){
										qt_coletada = 0;
									}
								}
								if(bat.transform.name == "recurso___arvore" || bat.transform.name == "recurso___arvore3")
								{
									if(nome == "frutas2"||nome == "frutas2")
									qt_coletada = 0;
								}
								
							}
						} else {
							ponto = bat.point;
							ponto = new Vector3(Random.Range(ponto.x, ponto.x + 15), 0.08f, Random.Range(ponto.z, ponto.z + 15));
							GetComponent < Animation > ().Play("comecando_correr");
							seeker.StartPath(transform.position, ponto, caminho_completo); //determina o caminho
							coletando = false;
							indo_construir = false;
							construindo = false;
							cons = null;
							indo_deposito = false;
							indo_coletar = false;
							recurso = null;
						}
					}
					
				}
				
			}
		}
		if (cons_colocada) {
			seeker.StartPath(transform.position, cons.transform.position, caminho_completo); //determina o caminho
			cons_colocada = false;
			indo_construir = true;
			recurso.GetComponent < Recursos > ().diminuir_recurso(false);
			
			//	if(Vector3.Distance(transform.position, local_cons) <= 2){
			//		seeker.StartPath(transform.position,transform.position,caminho_completo);//determina o caminho
			//	}
		}
		
		
		if (construindo) {
			GetComponent < Animation > ().Play("machado");
			GetComponent < Animation > ()["machado"].speed = 0.3f;
			machado.SetActive(true);
			
		}
		if (coletando) {
			if (recurso != null) {
			
				
				if (recurso.GetComponent < Recursos > ().quantidade_de_recurso > 0 && qt_coletada < 10) {
					
					if (nome == "arvore" || nome == "arvore3") {
						
						GetComponent < Animation > ().Play("machado");
						GetComponent < Animation > ()["machado"].speed = 0.3f;
						machado.SetActive(true);
					}
					if (nome == "frutas2" || nome == "frutas") {
						
						GetComponent < Animation > ().Play("coletando");
						GetComponent < Animation > ()["coletando"].speed = 0.4f;
						machado.SetActive(false);
					}
					
					
				}
				if( recurso.GetComponent< Recursos >().quantidade_de_recurso> 0)
					recurso.GetComponent < Recursos > ().diminuir_recurso(true);
				if(recurso.GetComponent< Recursos >().quantidade_de_recurso == 0 )
				{
					
					Destroy(recurso);
			
					coletando = false;
					indo_deposito = true;
					ir_deposito();
					
					
				}	
			}
		}
		
	}
	public void ir(Vector3 local) {
		local = new Vector3(Random.Range(local.x, local.x + 10), 0.08f, Random.Range(local.z, local.z + 10));
		seeker.StartPath(transform.position, local, caminho_completo); //determina o caminho
	}
	public void ir_deposito() {
		
		seeker.StartPath(transform.position, centro.transform.position, caminho_completo); //determina o caminho
	}
	
	public void set_status_contrucao_fantasma(bool status) {
		construcao_status = status;
		
	}
	public void caminho_completo(Path c) {
		if (!c.error) // se nao ocorrer nenhum erro
		{
			//reseta o contador de caminho
			Debug.Log("erro no");
			caminho = c;
			ponto_atual = 0;
		}
	}

	public void FixedUpdate() {

		if (caminho == null) return;
		if (ponto_atual >= caminho.vectorPath.Count) {
			if (!construindo && !coletando) {
				GetComponent < Animation > ().Play("parado");
				GetComponent < Animation > ()["parado"].speed = 0.3f;
				
				return;
			}
		}
		if (indo_coletar) {
				
			
			if (gameObject.GetComponent < unidades1 > ().selecionado)
			gameObject.GetComponent < unidades1 > ().set_status_contrucao_fantasma(false);
			
			
			if ( recurso != null && Vector3.Distance(transform.position, recurso.transform.position) < 14 ) {
				
				indo_coletar = false;
				coletando = true;
				nome = recurso.name.Substring(10);
				
			}
		}
		if (indo_deposito) {			
			if (Vector3.Distance(transform.position, centro.transform.position) < 25) {
				
				if (nome == "arvore" || nome == "arvore3") {
					controlador_quantidades.quantidade_madeira += qt_coletada;
				}
				if (nome == "frutas2" || nome == "frutas") {
					controlador_quantidades.quantidade_comida += qt_coletada;
				}
				qt_coletada = 0;
					indo_deposito = false;
				if(recurso != null)
				{
					indo_coletar = true;
					seeker.StartPath(transform.position, new Vector3(Random.Range(recurso.transform.position.x, recurso.transform.position.x + 10), 0.08f, Random.Range(recurso.transform.position.z, recurso.transform.position.z + 10)), caminho_completo); //determina o caminho
				}
			}
		}
		if (indo_construir) {
			
			if (gameObject.GetComponent < unidades1 > ().selecionado) gameObject.GetComponent < unidades1 > ().set_status_contrucao_fantasma(false);
			
			
			if (Vector3.Distance(transform.position, cons.transform.position) < 14) {
				indo_construir = false;
				construindo = true;
			}
		}
		if (caminho.vectorPath.Count != ponto_atual) {
			direcao = caminho.vectorPath[ponto_atual] - transform.position;
			direcao *= velocidade * Time.fixedDeltaTime;
			transform.root.LookAt(caminho.vectorPath[ponto_atual]);
			controlador.SimpleMove(direcao);
			if (indo_deposito &&(nome == "arvore" || nome == "arvore3")) 
			{
				GetComponent < Animation > ().Play("deposito_madeira");
				GetComponent < Animation > ()["deposito_madeira"].speed = 0.6f;
				machado.SetActive(false);
			}
			if (indo_deposito &&(nome == "frutas2" || nome == "frutas"))
			{
				GetComponent < Animation > ().Play("deposito_comida");
				GetComponent < Animation > ()["deposito_comida"].speed = 0.6f;
			}
			if(!indo_deposito)
			{
			GetComponent < Animation > ().Play("correndo");
			GetComponent < Animation > ()["correndo"].speed = 0.6f;
			}
			if (Vector3.Distance(transform.position, caminho.vectorPath[ponto_atual]) < proximo_ponto_do_caminho) {	
				ponto_atual++;
				return;
			}
		}	
	}

	public void ir_construir(GameObject construcao_local) {
		cons = construcao_local;
		cons_colocada = true;
		Debug.Log("cons:" + cons_colocada);
	}
	
}