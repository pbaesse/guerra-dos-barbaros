using UnityEngine;
using System.Collections;

public class selecao_cons : MonoBehaviour {
	public  bool selecionado = false;	
	private bool construcao_status = false;

	
	

	// Use this for initialization
	void Start () 
	{
		transform.FindChild ("projetor").gameObject.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	public  void OnSelected()
	{
		if (!construcao_status ) {
			
				selecionado = true;
				
				transform.FindChild ("projetor").gameObject.SetActive(true);
				//Debug.Log("selecionado");
			
			if(transform.name == "construcaoQuartel")
				GameObject.Find("GameObject").SendMessage("ativar_menu_quartel",true, SendMessageOptions.DontRequireReceiver);

			if(transform.name == "construcaoCentro")
				GameObject.Find("GameObject").SendMessage("ativar_menu_centro",true, SendMessageOptions.DontRequireReceiver);
				
			GameObject.Find("GameObject").SendMessage("construcao_selecionada",gameObject, SendMessageOptions.DontRequireReceiver);
			
		
		}
		
		//construcao_status = true;
	}
	
	public void OnUnselected()
	{
	
		if (!construcao_status ) 
		{
			if(selecionado)
			{

				if(transform.name == "construcaoQuartel")
					GameObject.Find("GameObject").SendMessage("ativar_menu_quartel",false, SendMessageOptions.DontRequireReceiver);
				if(transform.name == "construcaoCentro")
					GameObject.Find("GameObject").SendMessage("ativar_menu_centro",false, SendMessageOptions.DontRequireReceiver);
				
			}
			selecionado = false;	
			transform.FindChild ("projetor").gameObject.SetActive(false);
		
			//Mouse.unidades_selecionandas.Remove(this.transform.gameObject);
		}
		//construcao_status = true;
	}
	public void set_status_contrucao_fantasma(bool status)
	{
		construcao_status = status;
		
	}
	


}
