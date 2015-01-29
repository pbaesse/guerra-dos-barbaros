using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacebleBuilding : MonoBehaviour {
	
	public List<Collider> colliders = new List<Collider>();
	public  bool isSelected = false;
	private bool clicada = false;
	public GameObject[] unidades; 
	bool chao;
	private bool construindo = false;
	public bool placed = false;
	

	public Material inRed;

	public Vector3 GetConstructPosition
	{
		get { return new Vector3(transform.position.x, transform.position.y, transform.position.z); }
	}
	
	public bool Construindo
	{
		get { return construindo; }
	}


	[HideInInspector]
	

	void Update()
	{
		Operator a = new Operator();
		chao = a.get_nomes ();
		if (chao)
		{
			isSelected = false;
		}
		if (isSelected) 
		{
					    renderer.material.color = Color.green;
						//Debug.Log ("sf");
		}
		else 
		{
						renderer.material.color = Color.white;
			//Debug.Log ("fddd");
			isSelected = false;
		}

		if (Input.GetMouseButtonDown (1) && !placed)
		{
			Destroy(gameObject);
			
		}

	}

	/*public static void GetPosition(Vector3 pos)
	{
		unit_x = pos.x ;
		unit_y = pos.y ;
		unit_z = pos.z ;
	}*/

	void OnGUI()
	{
		if (isSelected) 
		{
			for (int i = 0; i < unidades.Length; i++) 
			{
				if(GUI.Button(new Rect(Screen.width/10, Screen.height/5+Screen.height/2*i,100, 30), unidades[i].name))
				{
					Instantiate((GameObject)unidades[i], new Vector3(transform.position.x-8, 1, transform.position.z+8), new Quaternion(-90,0,0,0));
				}
			}

			if(GUI.Button(new Rect(Screen.width/10, Screen.height/5+Screen.height/10,100, 30), "Atk"))
			{
				if(Unidade.Tipo == "Capsula")
				{
					Debug.Log(Unidade.Ataque);
					Unidade.Ataque++;
					Debug.Log(Unidade.Ataque);
				}
			}

		
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Building" || c.tag == "Unidade") 
		{
						colliders.Add (c);
						GetComponentInChildren<MeshCollider> ().renderer.material.color = Color.red;
		}

	}

	void OnTriggerExit(Collider c)
	{
		if (c.tag == "Building" || c.tag == "Unidade")
			colliders.Remove (c);
		if (colliders.Count == 0) {
			GetComponentInChildren<MeshCollider>().renderer.material.color = Color.white;
				}

	}

	public void SetSelected(bool s)
	{
		isSelected = s;
	}

	private void OnMouseDown()
	{
		    isSelected = true;
			clicada = true;

	}



	private void OnMouseUp()
	{
		if (clicada)
			isSelected = true;
		clicada = false;
	}

}
