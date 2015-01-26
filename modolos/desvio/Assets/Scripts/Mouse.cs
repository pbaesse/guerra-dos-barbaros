using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	private Vector3 ponto_mouse_apertado ;
	private RaycastHit bat;
	private Vector3 posicao_mouseup;
	private Vector3 click_inicial = - Vector3.one;
	private Vector3 posicao_atual_mouse; // no mapa

	//public static GameObject unida_sele_atual;
	public static ArrayList unidades_selecionandas = new ArrayList();
	public static ArrayList unidades_salvas_c  = new ArrayList();
	public static ArrayList unidades_selecionandas_quadrado = new ArrayList();
	public static ArrayList unidades_selecionandas_na_tela = new ArrayList();
	float zona_de_click = 1.3f;
	private static float BoxLeft,BoxTop,boxwidth,boxheight;


}
