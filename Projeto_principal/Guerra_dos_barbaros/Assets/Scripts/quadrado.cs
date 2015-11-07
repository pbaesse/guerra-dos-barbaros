using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class quadrado : MonoBehaviour {
	public Texture marqueeGraphics;
	private Vector2 marqueeOrigin;
	internal  Vector2 marqueeSize;
	public Rect marqueeRect;
	bool unidaddes_diferentes = false; 
	public static List<GameObject> selecionaveis_invariavel;
	public static List<GameObject> selecionaveis;
	public static List<GameObject> Unidades_selecionadas_salvas;
	public static List<GameObject> Unidades_selecionadas;
	internal Rect backupRect;
	public GameObject menu_aldeao;
	private Ray ray;
	private GameObject constru_sele = null;
	private RaycastHit hit;
	internal static bool status = false;
	public Vector3 ponto_do_mouse;
	public  bool fazendo_quadrado = false;
	public 	bool ativar_quartel = false;

	private void Start()
	{
		selecionaveis = new List<GameObject>(GameObject.FindGameObjectsWithTag("selecionaveis"));
		Unidades_selecionadas = new List<GameObject> ();
		int qt =0;
		int qt_lanceiro=0;
		foreach(GameObject u in selecionaveis)
		{
			
			if(u.name.Substring(0,10) == "unidade___")
			{
				qt++;
				if(u.name.Substring(10) == "Aldeiao_Jandui")
					qt_lanceiro+=0;	
				
			}
			
		}
		controlador_quantidades.quantidade_habitantes = qt;
		
	}
	private void OnGUI()
	{
		marqueeRect = new Rect(marqueeOrigin.x, marqueeOrigin.y, marqueeSize.x, marqueeSize.y);
		GUI.color = new Color(0, 0, 0, .3f);
		GUI.DrawTexture(marqueeRect, marqueeGraphics);
	}
	void Update()
	{
		
		Selecao();
		salvar_carregar_unidades();
		ativar_menu_aldeoa ();
		

	}

	
	void Selecao()
	{

		if (!GameObject.Find ("GameObject").GetComponent<UIevents> ().mouse_no_menu () && !status) 
		{
					
				if (Input.GetMouseButtonDown (0)) {
			
								//Poppulate the selectableUnits array with all the selectable units that exist
								fazendo_quadrado = true;
								selecionaveis = new List<GameObject> (GameObject.FindGameObjectsWithTag ("selecionaveis"));
								float _invertedY = Screen.height - Input.mousePosition.y;
								marqueeOrigin = new Vector2 (Input.mousePosition.x, _invertedY);
			
								//Check if the player just wants to select a single unit opposed to drawing a marquee and selecting a range of units
								ray = Camera.main.ScreenPointToRay (Input.mousePosition);
								if (Physics.Raycast (ray, out hit)) 
							{
										
										selecionaveis.Remove (hit.transform.gameObject);
										hit.transform.gameObject.SendMessage ("OnSelected", SendMessageOptions.DontRequireReceiver);
										ponto_do_mouse = hit.point;
							if(hit.transform.name.Length > 10){
					
							foreach(GameObject objeto in selecionaveis)
							{
							if(hit.transform.name.Substring(0,10) == "unidade___" && objeto.name.Substring(0,10) == "construcao")
								objeto.SendMessage("OnUnselected",SendMessageOptions.DontRequireReceiver);
							if(hit.transform.name.Substring(0,10) =="construcao" && objeto.name.Substring(0,10) ==  "unidade___")
							{
								constru_sele = hit.transform.gameObject;
								objeto.SendMessage("OnUnselected",SendMessageOptions.DontRequireReceiver);

							}
							}
							}
						}
			}
						if (Input.GetMouseButtonUp (0)) {
								//Reset the marquee so it no longer appears on the screen.
								marqueeRect.width = 0;
								marqueeRect.height = 0;
								backupRect.width = 0;
								backupRect.height = 0;
								marqueeSize = Vector2.zero;
								fazendo_quadrado = false;
								//Debug.Log("fq");
						}
						if (Input.GetMouseButton (0)) {
								float _invertedY = Screen.height - Input.mousePosition.y;
								marqueeSize = new Vector2 (Input.mousePosition.x - marqueeOrigin.x, (marqueeOrigin.y - _invertedY) * -1);
								//FIX FOR RECT.CONTAINS NOT ACCEPTING NEGATIVE VALUES
								if (marqueeRect.width < 0) {
										backupRect = new Rect (marqueeRect.x - Mathf.Abs (marqueeRect.width), marqueeRect.y, Mathf.Abs (marqueeRect.width), marqueeRect.height);
								} else if (marqueeRect.height < 0) {
										backupRect = new Rect (marqueeRect.x, marqueeRect.y - Mathf.Abs (marqueeRect.height), marqueeRect.width, Mathf.Abs (marqueeRect.height));
								}
								if (marqueeRect.width < 0 && marqueeRect.height < 0) {
										backupRect = new Rect (marqueeRect.x - Mathf.Abs (marqueeRect.width), marqueeRect.y - Mathf.Abs (marqueeRect.height), Mathf.Abs (marqueeRect.width), Mathf.Abs (marqueeRect.height));
								}
								foreach (GameObject objsele in selecionaveis) {
										//Convert the world position of the objsele to a screen position and then to a GUI point
										Vector3 _screenPos = Camera.main.WorldToScreenPoint (objsele.transform.position);
										Vector2 _screenPoint = new Vector2 (_screenPos.x, Screen.height - _screenPos.y);
										//Ensure that any units not within the marquee are currently unselected
										if (!marqueeRect.Contains (_screenPoint) || !backupRect.Contains (_screenPoint)) {
											if(objsele.transform.name.Substring(0,10) == "unidade___")
											{	
											if (!Input.GetKey (KeyCode.LeftControl)  )
														objsele.SendMessage ("OnUnselected", SendMessageOptions.DontRequireReceiver);
											}
											else
											objsele.SendMessage ("OnUnselected", SendMessageOptions.DontRequireReceiver);


					
										}
										if (marqueeRect.Contains (_screenPoint) || backupRect.Contains (_screenPoint)) {	
											if(objsele.transform.name.Substring(0,10) == "unidade___" )
											{
												objsele.SendMessage ("OnSelected", SendMessageOptions.DontRequireReceiver);
													if(constru_sele != null)
													{
														constru_sele.SendMessage ("OnUnselected", SendMessageOptions.DontRequireReceiver);
															constru_sele = null;
													}
												
											}

					
										}
				
				
								}
						}
				} 


	}
	public void limpar_marcador()
	{
		marqueeRect.width = 0;
		marqueeRect.height = 0;
		backupRect.width = 0;
		backupRect.height = 0;
		marqueeSize = Vector2.zero;
	}
	 
	 void ativar_menu_aldeoa()
	{

		int i = 0;

		if (Unidades_selecionadas.Count >=  1) 
		{

			while(Unidades_selecionadas.Count > i) 
			{
				if(Unidades_selecionadas.Last() != Unidades_selecionadas[i])
				{
					Debug.Log("vbbaa");
					if (Unidades_selecionadas[i].name.Substring (10) != Unidades_selecionadas[i+1].name.Substring (10) || Unidades_selecionadas[i].name != "unidade___Aldeiao_Jandui") 
					{
						Debug.Log("sssss");
						unidaddes_diferentes = true;

						break;
					}
				}

				if(Unidades_selecionadas.Last() == Unidades_selecionadas[i])
				{

					unidaddes_diferentes = false;
				}

					i++;
			}

		 if (unidaddes_diferentes) 
			{
				GameObject.Find("GameObject").GetComponent<UIevents>().abrir_menu_construcao(false);
				menu_aldeao.SetActive (false);
			}
		else if(!unidaddes_diferentes)
			{
				menu_aldeao.SetActive (true);
			}
		}
		if (Unidades_selecionadas.Count == 1) 
		{
			if(Unidades_selecionadas[0].name.Substring(10) == "Aldeiao_Jandui")
			{

				menu_aldeao.SetActive (true);
			}
			else
			{
				GameObject.Find("GameObject").GetComponent<UIevents>().abrir_menu_construcao(false);
				menu_aldeao.SetActive (false);
			}
		}
		if (Unidades_selecionadas.Count == 0)
		{
			GameObject.Find("GameObject").GetComponent<UIevents>().abrir_menu_construcao(false);
			menu_aldeao.SetActive (false);
		}

		
	}
	public void add_unidade_selecionada(GameObject unidade)
	{
		if(!Unidades_selecionadas.Contains(unidade))
		Unidades_selecionadas.Add (unidade);


		

	}
	public void remover_unidade_selecionada(GameObject unidade)
	{
		Unidades_selecionadas.Remove (unidade);

	}
	void salvar_carregar_unidades()
	{
		
		if(Input.GetKey(KeyCode.F1))
		{	
			//Unidades_selecionadas_salvas = new List<GameObject>(GameObject.FindGameObjectsWithTag("selecionaveis"));
			foreach (GameObject objsele in Unidades_selecionadas)
			{
				
				//Debug.Log("salvar");
				objsele.SendMessage("salvar",KeyCode.F1,SendMessageOptions.DontRequireReceiver);
			}
		}
		if(Input.GetKey(KeyCode.F2))
		{	
			Unidades_selecionadas_salvas = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
			foreach (GameObject objsele in Unidades_selecionadas_salvas)
			{
				
				//Debug.Log("salvar");
				objsele.SendMessage("salvar",KeyCode.F2,SendMessageOptions.DontRequireReceiver);
			}
		}
		
		if(Input.GetKey(KeyCode.F6))
		{
			foreach (GameObject objsele in selecionaveis)
			{
				Unidades_selecionadas_salvas = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
				//Debug.Log("carregar");
				objsele.SendMessage("carregar",KeyCode.F6, SendMessageOptions.DontRequireReceiver);
			}
		}
		if(Input.GetKey(KeyCode.F7))
		{
			foreach (GameObject objsele in selecionaveis)
			{
				Unidades_selecionadas_salvas = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
				//Debug.Log("carregar");
				objsele.SendMessage("carregar",KeyCode.F7, SendMessageOptions.DontRequireReceiver);
			}
		}
		
	}
	
	
}



