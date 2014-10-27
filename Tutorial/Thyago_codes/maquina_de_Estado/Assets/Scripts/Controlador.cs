using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controlador : MonoBehaviour
{
		public Texture2D multi_seletor = null;
		public static Rect sele = new Rect (0, 0, 0, 0);
		public float zoom_max= 0;
		public float zoom_min = 0;
		public float velocidade_zoom =0.02f;
		public float tempo_zoom = 0.1f;
		private Vector3 destino_zoom = Vector3.zero;
		private Vector3 click_inicial = - Vector3.one;
		private static Vector3 mov_para_destino = Vector3.zero;
		public float distancia_min_borda;
		public float velocidade_mov_camera;
		
		//private static List<string> navegado = new List<string> {"terra"};

		void Update ()
		{
				CheckCamera ();
				limpar ();
				zoom ();
				camera_mov();
		}
	private void camera_mov(){
		float tela_alt = Screen.height;
		float tela_larg = Screen.width;
		Vector2 mouse = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		float borda_X = Screen.height - mouse.x;
		float borda_Y = Screen.width - mouse.y;
		Vector3 movimento = Vector3.zero;
		float x = Input.GetAxis("X")*velocidade_mov_camera*distancia_min_borda;
		float y = Input.GetAxis("Y")*velocidade_mov_camera*distancia_min_borda;
		if(borda_Y < distancia_min_borda)
			movimento = new Vector3(movimento.x,movimento.y,distancia_min_borda - borda_Y);
		else if(mouse.y < distancia_min_borda)
			movimento = new Vector3(movimento.x,movimento.y,-(distancia_min_borda - mouse.y));
		if(borda_X < distancia_min_borda)
			movimento = new Vector3(distancia_min_borda - borda_X ,movimento.y,movimento.z);
		else if(mouse.x < distancia_min_borda)
			movimento = new Vector3(-(distancia_min_borda - mouse.x) ,movimento.y,movimento.z);
		movimento = new Vector3(movimento.x + x,movimento.y,movimento.z + y);
		transform.Translate(movimento*Time.deltaTime*velocidade_mov_camera,Space.World);
		}
	private void zoom(){
		float mov_y = Input.GetAxis("Mouse ScrollWheel");
		if(mov_y != 0)
			destino_zoom = transform.position + (mov_y * transform.forward)* velocidade_zoom;
		if((destino_zoom != Vector3.zero)&&(destino_zoom.y < zoom_max)&&(destino_zoom.y > zoom_min)){
			transform.position = Vector3.Lerp(transform.position,destino_zoom,tempo_zoom);
			if(transform.position == destino_zoom)
				destino_zoom = Vector3.zero;
		}
		if(transform.position.y < zoom_min)
			transform.position = new Vector3(transform.position.x,zoom_min,transform.position.z);
		if(transform.position.y > zoom_max)
			transform.position = new Vector3(transform.position.x,zoom_max,transform.position.z);
	
	}		

		private void CheckCamera ()
		{
				if (Input.GetMouseButtonDown (0))
						click_inicial = Input.mousePosition; //caso o botao esquerdo for precionado a posiçao onde foi clicado e registrado
				else if (Input.GetMouseButtonUp (0)) {
						click_inicial = -Vector3.one;
				}
				if (Input.GetMouseButton (0)) {
						sele = new Rect (click_inicial.x, invertmouseY (click_inicial.y), Input.mousePosition.x - click_inicial.x, invertmouseY (Input.mousePosition.y) - invertmouseY (click_inicial.y));
						if (sele.width < 0) {
								sele.x += sele.width;
								sele.width = -sele.width;
						}
						if (sele.height < 0) {
								sele.y += sele.height;
								sele.height = - sele.height;
						}
				}
		}

		private void OnGUI ()
		{
				if (click_inicial != -Vector3.one) {
						GUI.color = new Color (0, 0, 1, 0.7f);
						GUI.DrawTexture (sele, multi_seletor);
				}
		}

		private void limpar ()
		{
				if (!Input.GetMouseButtonUp (1)) {
						mov_para_destino = Vector3.zero;		
				}
		}

		public static Vector3 Get_destino ()
		{
				if (mov_para_destino == Vector3.zero) {
						RaycastHit bat;
						Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
						if (Physics.Raycast (r, out bat)) {
								while (bat.transform.gameObject.name != "terra") {
										if (!Physics.Raycast (bat.point, r.direction, out bat))
												break;
								}

						}
						if (bat.transform != null)
								mov_para_destino = bat.point;
				}
				return mov_para_destino; 

		}

		public static float invertmouseY (float y)
		{
				return Screen.height - y;
		}
}