using UnityEngine;
using System.Collections;

public class unidade_aliada : MonoBehaviour
{

		public bool  selecionou = false;
		public bool  selecionou_click = false;
		public bool  selecionou_ctrl = false;
		public float compensacao_da_terra = 1;
		public float velocidade = 5;
		public float compensacao_distancia_de_parar = 0.5f;
		private Vector3 mov_para_dest = Vector3.zero;
		NavMeshAgent agente;
		public int vida = 60;
		public int forca = 10;
		public int time = 14;
		public Vector3 alvo;
		public string estado = "parado";
		WaitForSeconds a ;
	
		void Start ()
		{
		agente = GetComponent<NavMeshAgent>();
		}
		
		// Update is called once per frame
		void Update ()
		{
				if (renderer.isVisible && Input.GetMouseButton (0)) {
					if(!selecionou_click){			
			Vector3 campos = Camera.main.WorldToScreenPoint (transform.position);
						campos.y = Controlador.invertmouseY (campos.y);
				selecionou = Controlador.sele.Contains (campos);}
			if(selecionou_ctrl)
			{
				selecionou = true;
			}

				if (selecionou){
								renderer.material.color = Color.red;
						
			}	
				else
								renderer.material.color = Color.white;
				}
		if(Input.GetMouseButtonUp(1) && selecionou){
			Vector3 destino = Controlador.Get_destino();
			if(destino != Vector3.zero){
				agente.SetDestination(destino);
			}

		}
				moveupdate ();
		if(Input.GetKey(KeyCode.LeftControl) && selecionou_click)
		{
			selecionou_ctrl = true;

		}
		else if (!Input.GetKey(KeyCode.LeftControl))
		{
			selecionou_ctrl = false;
		}
	/*	switch(estado)
		{
		case "andar" : 
			estado = andar();
			break;
		case "parado" : 
			Debug.Log("entrou");
			estado = parado();
			StartCoroutine(MyMethod());
			break;
		}
	*/			
		}
	IEnumerator MyMethod() {
		Debug.Log("Before Waiting 2 seconds");
		yield return new WaitForSeconds(2);
		Debug.Log("After Waiting 2 Seconds");
	}
	private void moveupdate ()
		{
				if (mov_para_dest != Vector3.zero && transform.position != mov_para_dest) {
						Vector3 direcao = (mov_para_dest - transform.position).normalized;
						transform.rigidbody.velocity = direcao * velocidade;
						if (Vector3.Distance (transform.position, mov_para_dest) < compensacao_distancia_de_parar)
								mov_para_dest = Vector3.zero;
						

				} else
						transform.rigidbody.velocity = Vector3.zero;

		}

	/*private string andar(){
		Vector3 destino = Controlador.Get_destino ();
		transform.position= destino ;
		if(destino == Vector3.zero)
			return "parado";
		return "andar";
	}*/
	/*private string parado(){
		Vector3 destino = Controlador.Get_destino ();
		if (selecionou && Input.GetMouseButtonUp (1)&& destino != Vector3.zero)
			return "andar";
		return "parado";
	}*/
	private void OnMouseDown(){
		selecionou_click = true;
		selecionou = true;

	}
	private void OnMouseUp(){
		if(selecionou_click)
			selecionou = true;
			selecionou_click = false;

	}
}