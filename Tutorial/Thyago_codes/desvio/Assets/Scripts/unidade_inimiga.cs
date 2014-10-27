using UnityEngine;
using System.Collections;

public class unidade_inimiga : MonoBehaviour {
	public int vida = 60;
	public int forca = 10;
	public Vector3 alvo;
	private NavMeshAgent agente;
	static string a ;
	// Use this for initialization
	void Start () 
	{
		agente = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		a = Controlador.get_nome();
	}
	void ataque()
	{

	}
}
