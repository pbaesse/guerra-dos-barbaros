using UnityEngine;
using System.Collections;

public class unidade : MonoBehaviour
{

		public bool  selecionou = false;
		public bool  selecionou_click = false;
		public bool  selecionou_ctrl = false;
		public float compensacao_da_terra = 1;
		public float velocidade = 5;
		public float compensacao_distancia_de_parar = 0.5f;
		private Vector3 mov_para_dest = Vector3.zero;
		NavMeshAgent	agent = new	NavMeshAgent();
		public Transform a ;

		void Awake ()
		{
		agent = GetComponent<NavMeshAgent>();
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
				if (selecionou && Input.GetMouseButtonUp (1)) {

						Vector3 destino = Controlador.Get_destino ();
						if (destino != Vector3.zero) {
								
				agent.SetDestination(a.position);
								//mov_para_dest = destino;
								//mov_para_dest.y += compensacao_da_terra;
					
						}
				}
				moveupdate ();
		if(Input.GetKey(KeyCode.LeftControl) && selecionou_click){
			selecionou_ctrl = true;

		}
		else if (!Input.GetKey(KeyCode.LeftControl)){
			selecionou_ctrl = false;
		}
				
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