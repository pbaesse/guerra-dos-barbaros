using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public Texture2D quadradoSelecao = null;
	public static Rect selecao = new Rect(0,0,0,0);
	private Vector3 startClick = -Vector3.one;
	void Update () 
	{
		checarCamera ();
	
	}
	private void checarCamera()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			startClick=Input.mousePosition;
		}
		else if (Input.GetMouseButton(0))
		{
			if(selecao.width < 0)
			{
				selecao.x +=selecao.width;
				selecao.width = -selecao.width;
			}
			if(selecao.height < 0)
			{
				selecao.y += selecao.height;
				selecao.height = - selecao.height;
			}
			startClick = -Vector3.one;
		}
		if(Input.GetMouseButton(0)){
			selecao = new Rect (startClick.x,invertMouseY(startClick.y),Input.mousePosition.x - startClick.x,invertMouseY(Input.mousePosition.y)- invertMouseY(startClick.y));
			}

		private void OnGui()
		{
			if(startClick != -Vector3.one)
			{
				GUI.Color = new Color (1, 1, 1, 0.5f);
				GUI.DrawTexture(selecao,quadradoSelecao);

			}
		}
			               
	}
	public static float invertMouseY(float y)
	{
		return Screen.height - y;
	}
}
