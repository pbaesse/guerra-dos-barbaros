using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Operator : MonoBehaviour {

	public Texture2D selecionadorHigh;
	public static Rect selection = new Rect(0, 0, 0, 0);
	private Vector3 click_inicial = -Vector3.one;
	private static Vector3 irPara = Vector3.zero;
	private static List<string> passables = new List<string>() { "Floor" };
	
	// Use this for initialization
	void CheckCamera() 
	{
		if (Input.GetMouseButtonDown (0))
						click_inicial = Input.mousePosition;

		else if (Input.GetMouseButtonUp(0))
			click_inicial = -Vector3.one;

		if (Input.GetMouseButton (0))
		{
			selection = new Rect (click_inicial.x, InvertMouseY (click_inicial.y), Input.mousePosition.x - click_inicial.x, InvertMouseY (Input.mousePosition.y) - InvertMouseY (click_inicial.y));

			if (selection.width < 0)
			{
				selection.x += selection.width;
				selection.width = -selection.width;
			}
			if (selection.height < 0)
			{
				selection.y += selection.height;
				selection.height = -selection.height;
			}

		}

	}

	private void CleanUp()
	{
		if (!Input.GetMouseButtonUp (1))
						irPara = Vector3.zero;
	}

	public static Vector3 GetDestination()
	{
		if (irPara == Vector3.zero) 
		{
			RaycastHit hit;
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(r, out hit))
			{
				while(!passables.Contains(hit.transform.gameObject.name))
				{
					if(!Physics.Raycast (hit.transform.position, r.direction, out hit))
						break;
				}

			}

			if(hit.transform !=null)
				irPara = hit.point;
		}

		return irPara;
	}

	private void OnGUI()
	{
		if (click_inicial != -Vector3.one) 
		{
				GUI.color = new Color (1, 1, 1, 0.5f);
				GUI.DrawTexture (selection, selecionadorHigh);
		}

	}
	


	public static float InvertMouseY(float y)
	{
		return Screen.height - y;
	}

	public bool get_nomes(){
		RaycastHit hit;
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		string nomes = "";
		if (Physics.Raycast (r, out hit)) 
		{
			nomes = hit.collider.name ;
		}
		if(nomes == "Floor" && Input.GetMouseButtonDown(1))
		return true;
		return false;
		}

	// Update is called once per frame
	void Update () 
	{
		CheckCamera ();
		CleanUp ();
	
	}
}
	
