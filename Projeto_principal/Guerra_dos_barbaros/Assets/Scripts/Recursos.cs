using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Recursos: MonoBehaviour {
	
	public int quantidade_de_recurso;
	public bool selecionado = false;
	private bool coletada = false;
	private static List < GameObject > selecionaveis;
	private static List < GameObject > recursos;
	
	
	
	// Use this for initialization
	void Start() {
		StartCoroutine(delay());
		
	}
	
	// Update is called once per frame
	void Update() {
		
		//Debug.Log("quantidade :" + quantidade_de_recurso + " re" + gameObject.GetHashCode());
	}
	public void diminuir_recurso(bool estado) {
		coletada = estado;
	}
	public void OnSelected() {
		//if (!construcao_status ) {
		selecionado = true;
		
		transform.FindChild("projetor").gameObject.SetActive(true);
		Debug.Log(transform.name);
		//Debug.Log("selecionado");
		
	}
	public void OnUnselected() {
		
		//if (!construcao_status ) 
		//{
		selecionado = false;
		transform.FindChild("projetor").gameObject.SetActive(false);
		
		//Mouse.unidades_selecionandas.Remove(this.transform.gameObject);
		//}
		//construcao_status = true;
		//construcao_status = true;
	}
	bool Eultima(string s) {
		int i = 0;
		foreach(GameObject a in selecionaveis) {
			if (a.name == s) i++;
		}
		if (i == 0 || i == 1) return true;
		else return false;
		
	}
	
	IEnumerator delay() {
		selecionaveis = new List < GameObject > (GameObject.FindGameObjectsWithTag("selecionaveis"));
		
		
		if (coletada) {
			foreach(GameObject objeto in selecionaveis) {
				if (quantidade_de_recurso > 0 && objeto.name == "unidade___Aldeiao_Jandui" && objeto.GetComponent < Caminho_unidade2 > ().qt_coletada < 10 && objeto.GetComponent < Caminho_unidade2 > ().recurso == gameObject) {
					quantidade_de_recurso -= 1;
					objeto.GetComponent < Caminho_unidade2 > ().qt_coletada += 1;
					if (objeto.GetComponent < Caminho_unidade2 > ().qt_coletada >= 10) {
						objeto.GetComponent < Caminho_unidade2 > ().ir_deposito();
						objeto.GetComponent < Caminho_unidade2 > ().coletando = false;
						objeto.GetComponent < Caminho_unidade2 > ().indo_deposito = true;
						
					}
				}
				}
			
			
			}
		
		yield return new WaitForSeconds(2);
		StartCoroutine(delay());
		}
	void OnDestroy ()
	{
		recursos = new List<GameObject>(GameObject.FindGameObjectsWithTag("selecionaveis"));
		GameObject outro = null;
		float distancia1 = 0;
		float distancia2 = 0;
		if(transform.name == "recurso___frutas2" || transform.name == "recurso___frutas")
		{
			foreach(GameObject objeto in recursos){
				
				if ((objeto.name == "recurso___frutas" ||objeto.name == "recurso___frutas2") && objeto != gameObject) {
					
					if(outro  == null)
						outro  = objeto;
					
					distancia1 = Vector3.Distance(transform.position,objeto.transform.position);
					distancia2 = Vector3.Distance(transform.position,outro.transform.position);
					if(distancia1 < distancia2){
						Debug.Log("mais proximo");
						outro = objeto;
					}
					
				}
			}
		}
		if(transform.name == "recurso___arvore" || transform.name == "recurso___arvore3"){
			Debug.Log("destruido o coitado");
			
			foreach(GameObject objeto in recursos){
				
				if ((objeto.name == "recurso___arvore" ||objeto.name == "recurso___arvore3") && objeto != gameObject) {
					
					if(outro  == null)
						outro  = objeto;
					
					distancia1 = Vector3.Distance(transform.position,objeto.transform.position);
					distancia2 = Vector3.Distance(transform.position,outro.transform.position);
					if(distancia1 < distancia2){
						outro = objeto;
					}
					
				}
			}
		}
		
		
		selecionaveis = new List < GameObject > (GameObject.FindGameObjectsWithTag("selecionaveis"));
		foreach(GameObject objeto in selecionaveis)
		{
			
			if(objeto.name == "unidade___Aldeiao_Jandui"){
			if(objeto.GetComponent< Caminho_unidade2> ().recurso == gameObject ){
				objeto.GetComponent< Caminho_unidade2> ().recurso = outro;
				Debug.Log("entrou arvore");
				}
		}
		
	}

}
	}
	
		
	

