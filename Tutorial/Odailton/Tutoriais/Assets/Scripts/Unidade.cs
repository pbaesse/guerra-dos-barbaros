using UnityEngine;
using System.Collections;

public class Unidade: MonoBehaviour {
	
	private bool selecionada;
	private bool clicada = false;
	private Vector3 mover = Vector3.zero;
	private float floorOffSet = 1;
	private float speed = 5;
	private float stopDistanceOffSet = 0.5f;

	public virtual void ActionCallback(Vector3 target){ }

	private void Update()
	{
		if ((renderer.isVisible && Input.GetMouseButton (0)) && !clicada)
		{
			Vector3 campos = Camera.main.WorldToScreenPoint(transform.position);
			campos.y = Operator.InvertMouseY(campos.y);
			selecionada = Operator.selection.Contains(campos);
		}

		if (selecionada)
						renderer.material.color = Color.red;
		else 
			renderer.material.color = Color.white;

		if (selecionada && Input.GetMouseButtonUp (1))
		{
			Vector3 destination = Operator.GetDestination();

			if(destination != Vector3.zero)
			{
				//gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);
				mover = destination;
				mover.y += floorOffSet;
			}
		}

		UpdateMove ();

	}

	private void UpdateMove()
	{
		if (mover != Vector3.zero && transform.position != mover) {
						Vector3 direction = (mover - transform.position).normalized;
						direction.y = 0;
						transform.rigidbody.velocity = direction * speed;

						if (Vector3.Distance (transform.position, mover) < stopDistanceOffSet)
								mover = Vector3.zero;
				} else
						transform.rigidbody.velocity = Vector3.zero;
	}

	private void OnMouseDown()
	{
		if (!selecionada) 
		{
			selecionada = true;
			clicada = true;
		} 
		else 
		{
			selecionada = false;
			clicada = false;
		}
	}

	private void OnMouseUp()
	{
		if (clicada)
						selecionada = true;
		clicada = false;
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (0, 40, 400, 40), "Quartel Jandui")) 
		{
			BuildingPlacer alocador = new BuildingPlacer();
			alocador.Create();
		}

	}
	
}
