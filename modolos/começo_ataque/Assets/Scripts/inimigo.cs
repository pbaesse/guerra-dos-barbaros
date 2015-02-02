using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class inimigo : MonoBehaviour {
	public static List<GameObject> unidades_inimigas;
	public int forca;
	public int vida;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		unidades_inimigas = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
		foreach (GameObject unidade in unidades_inimigas)
		{
			float distancia = Vector3.Distance(unidade.transform.position, this.transform.position);
			Debug.Log (distancia+ unidade.name);
			if (distancia <= 15){
				Debug.Log ("PERTO"+ unidade.name);
				unidade.SendMessage("atacar_unidade", forca, SendMessageOptions.DontRequireReceiver);
			}
			else
						Debug.Log ("LONGE"+ unidade.name);	
		}


	}
	void atacar_inimigo(int vida){}

}
