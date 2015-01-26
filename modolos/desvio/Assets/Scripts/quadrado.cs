using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class quadrado : MonoBehaviour {
	public Texture marqueeGraphics;
	private Vector2 marqueeOrigin;
	private Vector2 marqueeSize;
	public Rect marqueeRect;
	public static List<GameObject> Unidades_selecionaveis;
	public static List<GameObject> Unidades_selecionadas_salvas_f1;
	private Rect backupRect;
	private Ray ray;
	private RaycastHit hit;
	public Vector3 ponto_do_mouse;
	private void OnGUI()
	{
		marqueeRect = new Rect(marqueeOrigin.x, marqueeOrigin.y, marqueeSize.x, marqueeSize.y);
		GUI.color = new Color(0, 0, 0, .3f);
		GUI.DrawTexture(marqueeRect, marqueeGraphics);
	}
	void Update()
	{

		Selecao_quadrado();
		salvar_carregar_unidades();

	}
	void Selecao_quadrado()
	{
		
		if (Input.GetMouseButtonDown(0))
		{
			//Poppulate the selectableUnits array with all the selectable units that exist
			Unidades_selecionaveis = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
			
			float _invertedY = Screen.height - Input.mousePosition.y;
			marqueeOrigin = new Vector2(Input.mousePosition.x, _invertedY);
			
			//Check if the player just wants to select a single unit opposed to drawing a marquee and selecting a range of units
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				Unidades_selecionaveis.Remove(hit.transform.gameObject);
				hit.transform.gameObject.SendMessage("OnSelected",SendMessageOptions.DontRequireReceiver);
					ponto_do_mouse = hit.point;
				Debug.Log("f");
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			//Reset the marquee so it no longer appears on the screen.
			marqueeRect.width = 0;
			marqueeRect.height = 0;
			backupRect.width = 0;
			backupRect.height = 0;
			marqueeSize = Vector2.zero;
			Debug.Log("fq");
		}
		if (Input.GetMouseButton(0))
		{
			float _invertedY = Screen.height - Input.mousePosition.y;
			marqueeSize = new Vector2(Input.mousePosition.x - marqueeOrigin.x, (marqueeOrigin.y - _invertedY) * -1);
			//FIX FOR RECT.CONTAINS NOT ACCEPTING NEGATIVE VALUES
			if (marqueeRect.width < 0)
			{
				backupRect = new Rect(marqueeRect.x - Mathf.Abs(marqueeRect.width), marqueeRect.y, Mathf.Abs(marqueeRect.width), marqueeRect.height);
			}
			else if (marqueeRect.height < 0)
			{
				backupRect = new Rect(marqueeRect.x, marqueeRect.y - Mathf.Abs(marqueeRect.height), marqueeRect.width, Mathf.Abs(marqueeRect.height));
			}
			if (marqueeRect.width < 0 && marqueeRect.height < 0)
			{
				backupRect = new Rect(marqueeRect.x - Mathf.Abs(marqueeRect.width), marqueeRect.y - Mathf.Abs(marqueeRect.height), Mathf.Abs(marqueeRect.width), Mathf.Abs(marqueeRect.height));
			}
			foreach (GameObject unit in Unidades_selecionaveis)
			{
				//Convert the world position of the unit to a screen position and then to a GUI point
				Vector3 _screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
				Vector2 _screenPoint = new Vector2(_screenPos.x, Screen.height - _screenPos.y);
				//Ensure that any units not within the marquee are currently unselected
				if (!marqueeRect.Contains(_screenPoint) || !backupRect.Contains(_screenPoint))
				{

					if(!Input.GetKey(KeyCode.LeftControl))
					unit.SendMessage("OnUnselected", SendMessageOptions.DontRequireReceiver);

				}
				if (marqueeRect.Contains(_screenPoint) || backupRect.Contains(_screenPoint))
				{	
				
					unit.SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver);
				
				}


			}
		}
	}
		void salvar_carregar_unidades()
		{

		if(Input.GetKey(KeyCode.F1))
		{	
			Unidades_selecionadas_salvas_f1 = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
			foreach (GameObject unit in Unidades_selecionadas_salvas_f1)
			{
			
			Debug.Log("salvar");
				unit.SendMessage("salvar",KeyCode.F1,SendMessageOptions.DontRequireReceiver);
			}
		}
		if(Input.GetKey(KeyCode.F2))
		{	
			Unidades_selecionadas_salvas_f1 = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
			foreach (GameObject unit in Unidades_selecionadas_salvas_f1)
			{
				
				Debug.Log("salvar");
				unit.SendMessage("salvar",KeyCode.F2,SendMessageOptions.DontRequireReceiver);
			}
		}
	
		if(Input.GetKey(KeyCode.F6))
		{
			foreach (GameObject unit in Unidades_selecionaveis)
			{
			Unidades_selecionadas_salvas_f1 = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
			Debug.Log("carregar");
				unit.SendMessage("carregar",KeyCode.F6, SendMessageOptions.DontRequireReceiver);
			}
		}
		if(Input.GetKey(KeyCode.F7))
		{
			foreach (GameObject unit in Unidades_selecionaveis)
			{
				Unidades_selecionadas_salvas_f1 = new List<GameObject>(GameObject.FindGameObjectsWithTag("unidade"));
				Debug.Log("carregar");
				unit.SendMessage("carregar",KeyCode.F7, SendMessageOptions.DontRequireReceiver);
			}
		}

	}

}


