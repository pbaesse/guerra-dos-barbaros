using UnityEngine;
using System.Collections;

public class Unidades : MonoBehaviour {
	public bool selected = false;
	public bool  selecionou_click = false;


	private void Update ()
	{
				if (renderer.isVisible && Input.GetMouseButton (0)) 
				{
						Vector3 campos = Camera.Camera.main.WorldToScreenPoint (transform.position);
						campos.y = Camera.invertMouseY (campos.y);
						selected = Camera.selecao.Contains (campos);
				}
				if (selected)
					renderer.material.color = Color.red;
				else
					renderer.material.color = Color.white;
		
	}	
} 

