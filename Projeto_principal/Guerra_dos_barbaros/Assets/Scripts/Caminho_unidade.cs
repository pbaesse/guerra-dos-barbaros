using UnityEngine;
using System.Collections;
using Pathfinding;

public class Caminho_unidade : MonoBehaviour {

	private Seeker seeker;
	private CharacterController controlador;
	private Path caminho;
	private unidades unidade;
	public float velocidade;
	public float proximo_ponto_do_caminho = 10;
	private int ponto_atual =0;
	private quadrado qua;
	private string click;


	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
		controlador = GetComponent<CharacterController>();
		unidade = GetComponent<unidades>();
		//qua = GetComponent<quadrado>();
	}
	
	// Update is called once per frame
	void LateUpdate () {

			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit bat;
			Vector3 ponto = Vector3.zero;
			if(Physics.Raycast(r,out bat)){
				ponto = bat.point;
			click = bat.collider.tag;
		
		if(unidade.selecionado){
			if(Input.GetMouseButton(1))
				seeker.StartPath(transform.position,ponto,caminho_completo);//determina o caminho
			}
		}
		Debug.DrawRay(r.origin,r.direction,Color.blue);
		//Debug.Log (click);

	
	}
	public void caminho_completo(Path c)
	{
		if(!c.error) // se nao ocorrer nenhum erro
		{
			//reseta o contador de caminho
			Debug.Log("erro no");
			caminho = c;
			ponto_atual = 0;
		}
	}
	public void FixedUpdate()
	{
		if(caminho == null)
			return;
		if(ponto_atual >= caminho.vectorPath.Count)
			return;
		Vector3 direcao = caminho.vectorPath[ponto_atual] - transform.position;
		direcao *= velocidade* Time.fixedDeltaTime;
		controlador.SimpleMove(direcao);
		if(Vector3.Distance(transform.position,caminho.vectorPath[ponto_atual]) < proximo_ponto_do_caminho)
		{
			ponto_atual ++;
			return;
		}
	}
}
