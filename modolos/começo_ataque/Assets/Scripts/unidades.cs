using UnityEngine;
using System.Collections;
//
public class unidades : MonoBehaviour {
	// para o mouse

	public  bool selecionado = false;
	private bool salva_f1 = false;
	private bool salva_f2 = false;
	public int vida;


	void Start()
	{
		StartCoroutine(esperar ());
	}
	void LateUpdate () 
	{
		verifica_morte ();
	}
	public  void OnSelected()
	{
		Debug.Log ("sele");
		if (!Input.GetKey (KeyCode.LeftControl)) 
		{
						selecionado = true;
						renderer.material.color = Color.red;
		}
		if(Input.GetKey (KeyCode.LeftControl) && selecionado)
		{
			Debug.Log ("dessele");
			selecionado = false;
			renderer.material.color = Color.white;
		}
		//Mouse.unidades_selecionandas.Add(this.transform.gameObject);
		else if(Input.GetKey (KeyCode.LeftControl) && !selecionado)
		{
			Debug.Log ("dessele");
			selecionado = true;
			renderer.material.color = Color.red;
		}

	}
	
	public void OnUnselected()
	{
		selecionado = false;
		renderer.material.color = Color.white;
	
		//Mouse.unidades_selecionandas.Remove(this.transform.gameObject);
	}
	public void salvar(KeyCode a)
	{
		if(selecionado && a == KeyCode.F1)
		salva_f1 = true;
		if(selecionado && a == KeyCode.F2)
			salva_f2 = true;

	
	}
	public void  carregar(KeyCode a)
	{
		if(salva_f1 && a == KeyCode.F6)
		{
			selecionado = true;
			renderer.material.color = Color.red;
		}
		if(salva_f2 && a == KeyCode.F7)
		{
			selecionado = true;
			renderer.material.color = Color.red;
		}
	}
	public void atacar_unidade(int forca)
	{
		Debug.Log ("atacado");
		vida -= forca;
		esperar ();
	}
	void verifica_morte ()
	{
		if(vida == 0)
		Destroy (this.gameObject);
	}
	IEnumerator esperar()
	{
		yield return new WaitForSeconds (10);
	}
}

