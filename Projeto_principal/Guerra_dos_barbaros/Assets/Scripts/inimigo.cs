using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class inimigo : MonoBehaviour {
	public static List<GameObject> unidades_inimigas;
	public int forca;
	public int vida;
	public bool atacando = false;
	// Use this for initialization
	void Start () {
		StartCoroutine("esperar");

	}
	
	// Update is called once per frame
	void Update () {

		atacar_unidade ();
	}
	void atacar_unidade()
	{
		unidades_inimigas = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
		
		foreach (GameObject unidade in unidades_inimigas)
		{
			float  distancia = Vector3.Distance(unidade.transform.position, this.transform.position);
			Debug.Log (distancia+ unidade.name);
			if (distancia <= 15){
				Debug.Log ("PERTO"+ unidade.name);
				if(atacando){
					unidade.SendMessage("atacar_unidade", forca, SendMessageOptions.DontRequireReceiver);
					atacando = false;
				}
				
			}
			else
				Debug.Log ("LONGE"+ unidade.name);	
		}
		Debug.Log(atacando+" upadate");

	}

	IEnumerator esperar ()
	{
	

				yield return new WaitForSeconds (5);
				Debug.Log ("esperei 5 secs");
				atacando = true;
				Debug.Log(atacando+" ifesperar");
				StartCoroutine ("esperar");

	}

	
}
