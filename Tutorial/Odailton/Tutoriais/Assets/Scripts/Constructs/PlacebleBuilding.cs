using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacebleBuilding : MonoBehaviour {
	
	public List<Collider> colliders = new List<Collider>();
	private bool isSelected;
	private bool clicada = false;
	public GameObject[] unidades;

	[HideInInspector]

	void Update()
	{
		if (isSelected)
			renderer.material.color = Color.blue;
		else
			renderer.material.color = Color.white;

	}

	void OnGUI()
	{
		if (isSelected) 
		{
			for (int i = 0; i < unidades.Length; i++) 
			{
				if(GUI.Button(new Rect(Screen.width/10, Screen.height/5+Screen.height/2*i,100, 30), unidades[i].name))
				{
					Instantiate((GameObject)unidades[i]);
				}
			}
		
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Building")
						colliders.Add (c);

	}

	void OnTriggerExit(Collider c)
	{
		if (c.tag == "Building")
			colliders.Remove (c);
		
	}

	public void SetSelected(bool s)
	{
		isSelected = s;
	}

	private void OnMouseDown()
	{
		if (!isSelected) 
		{
			isSelected = true;
			clicada = true;
		} 
		else 
		{
			isSelected = false;
			clicada = false;
		}
	}
	
	private void OnMouseUp()
	{
		if (clicada)
			isSelected = true;
		clicada = false;
	}

}
