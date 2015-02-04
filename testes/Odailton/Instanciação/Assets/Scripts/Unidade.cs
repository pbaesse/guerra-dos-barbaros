using UnityEngine;
using System.Collections;

public class Unidade: MonoBehaviour {
	
	private static int ataque = 10;
	private static int armadura = 10;
	private static string tipo = "Capsula";

	private bool selecionada;
	private bool clicada = false;
	private Vector3 mover = Vector3.zero;
	private float floorOffSet = 1;
	private float speed = 5;
	private float stopDistanceOffSet = 0.5f;

	private bool gambiarra = false;
	private float gambMove = 0.0001f;
	private int gambCount = 0;

	public static string Tipo
	{
		get { return tipo; }
		set { tipo = value; }
	}
	public static int Ataque
	{
		get { return ataque; }
		set { ataque = value; }
	}
	public static int Armadura 
	{
		get { return armadura; }
		set { armadura = value; }
	}

	public virtual void ActionCallback(Vector3 target){ }

	public Unidade()
	{

	}

	private void Update()
	{
		if (!gambiarra) 
		{
			transform.Translate (gambMove, 0, gambMove);
			gambMove -= 0.0001f;
			gambCount++;
			if(gambCount == 100) gambiarra = true;	
		}
		if ((renderer.isVisible && Input.GetMouseButton (0)) && !clicada)
		{
			Vector3 campos = Camera.main.WorldToScreenPoint(transform.position);
			campos.y = Operator.InvertMouseY(campos.y);
			selecionada = Operator.selection.Contains(campos);
		}

		if (selecionada)
						renderer.material.color = Color.blue;
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
	
			selecionada = true;
			clicada = true;
	

	}

	private void OnMouseUp()
	{
		if (clicada)
						selecionada = true;
		clicada = false;
	}
	

}
