using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	private Vector3 ponto_mouse_apertado ;
	private RaycastHit bat;
	public static GameObject unida_sele_atual;
	// Use this for initialization
	void Awake(){
		ponto_mouse_apertado = Vector3.zero;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		atividades_mouse();
	}
	void atividades_mouse()
	{
		Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(raio, out bat, Mathf.Infinity ))
		{
			if(Input.GetMouseButtonDown(0))
				ponto_mouse_apertado  = bat.point;
			if(bat.collider.name == "terra")//terreno
			{
			if(Input.GetMouseButtonUp(0) && click_esquerdo(ponto_mouse_apertado))
				deseleciona();
			}
			else
			{
				//outros objetos
				if(Input.GetMouseButtonUp(0) && click_esquerdo(ponto_mouse_apertado))
				{
					if(bat.collider.transform.FindChild("selecionado"))
					{
						if(unida_sele_atual != bat.collider.gameObject){ 
						bat.collider.transform.FindChild("selecionado").gameObject.active = true;
							if(unida_sele_atual != null)
							{
								unida_sele_atual.transform.FindChild("selecionado").gameObject.active = false;
							}
							unida_sele_atual = bat.collider.gameObject;
						}
					}
				}
			}

		}
		else{
			//fora do terreno
			if(Input.GetMouseButtonUp(0) && click_esquerdo(ponto_mouse_apertado))
				deseleciona();
		}
		
	}
	#region ajuda

	public bool click_esquerdo(Vector3 ponto_bat)
	{
		float zona_de_click = 0.9f;
		if((ponto_mouse_apertado.x< ponto_bat.x + zona_de_click && ponto_mouse_apertado.x > ponto_bat.x - zona_de_click)&&
		   (ponto_mouse_apertado.y< ponto_bat.y + zona_de_click && ponto_mouse_apertado.y > ponto_bat.y - zona_de_click)&&
		   (ponto_mouse_apertado.z< ponto_bat.z + zona_de_click && ponto_mouse_apertado.z > ponto_bat.z - zona_de_click))
			return true;
				else 
			return false;
	}
	public static void deseleciona()
	{
		if(unida_sele_atual != null)
		{
			unida_sele_atual.transform.FindChild("selecionado").gameObject.SetActive(false);
			unida_sele_atual = null;
		}
	}
	#endregion

}
