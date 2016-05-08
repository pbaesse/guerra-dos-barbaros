using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {
	bool opcao = false;
	public GameObject menu_opcao;
	public GameObject menu_fase;
	public Button botao_fase;
	public Button botao_opcoes;
	bool ativo = true;
	bool ativar_fase = false;
	bool janela = false;
	ColorBlock cb ;
	public Rect windowRect = new Rect(20, 20, 120, 50);
	void Start()
	{

		//slider = GameObject.Find ("slider");
		menu_opcao.transform.FindChild ("Slider").FindChild ("qualidade").GetComponent<Text> ().text = "Graficos: Alto";
	}
	public void carregar_fase(string nome_fase)
	{
		Application.LoadLevel (nome_fase);
	}
	public void sair()
	{
		Application.Quit ();
	}
	public void fases()
	{   ColorBlock cb;

		if (!ativar_fase) 
		{

			ativar_fase = true;
			opcao = false;
	
		 
		}
		else 
		{
			ativar_fase = false;

		
		}
		Debug.Log (ativar_fase);
	}
	public void opcoes()
	{
		if (!opcao) {
						opcao = true;
			ativar_fase = false;
			 
						
				} else {
				
						opcao = false;
						
				}
	
	}

	public void  qualidade(Slider slider)
	{
		if (slider.value >= 0 & slider.value <= 0.375) {
						slider.value = 0;
			QualitySettings.SetQualityLevel(1);
			slider.transform.FindChild("qualidade").GetComponent<Text>().text = "Graficos: Baixo";
			Debug.Log(QualitySettings.GetQualityLevel());
				}
		if (slider.value > 0.375 & slider.value <= 0.75) {
						slider.value = 0.5f;
			QualitySettings.SetQualityLevel(3);
			slider.transform.FindChild("qualidade").GetComponent<Text>().text = "Graficos: Medio";
			Debug.Log(QualitySettings.GetQualityLevel());
				}
		if (slider.value > 0.75 & slider.value <= 1) {
						slider.value = 1;
			QualitySettings.SetQualityLevel(5);
			slider.transform.FindChild("qualidade").GetComponent<Text>().text = "Graficos: Alto";
			Debug.Log(QualitySettings.GetQualityLevel());
				}
	}
	public void Ligar_musica(GameObject musica)
	{
		ativo = !ativo;
		musica.SetActive (ativo);

	}
	void Update()
	{
		menu_opcao.SetActive (opcao);
		menu_fase.SetActive (ativar_fase);
		Debug.Log(opcao);
		Debug.Log(Color.gray);

		if (opcao) {
						Debug.Log ("ff");
						cb = botao_opcoes.colors;
						cb.normalColor = Color.gray;
						botao_opcoes.colors = cb;
				}
		else
		{
			cb = botao_opcoes.colors;
			cb.normalColor = Color.white;
			botao_opcoes.colors = cb;
		}

		 if (ativar_fase) 
		{
				cb = botao_fase.colors;
				cb.normalColor = Color.gray;
				botao_fase.colors = cb;
		}
		else
		{
				cb = botao_fase.colors;
				cb.normalColor = Color.white;
				botao_fase.colors = cb;
		}
	}
}
