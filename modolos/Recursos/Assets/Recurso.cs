using UnityEngine;
using System.Collections;
public enum tipoRecurso{
	gold,
	food,
	wood
}
public class Recurso : MonoBehaviour {
	public tipoRecurso recurso;
	public int recursoLeft;
	public delegate void ManipularFonteVazia();
	public event ManipularFonteVazia TaVazio;
	public int ColherDaqui(int QuantoQuer){

				if (QuantoQuer <= recursoLeft)
				{
						recursoLeft -= QuantoQuer;
						return QuantoQuer;
				} 
				else 
				{
					recursoLeft = 0;
					if (TaVazio != null)
					{
						TaVazio ();
					}	
					return recursoLeft;
				}
		}
		void Start () {
			if (Input.GetButtonDown ("0")&&(recursoLeft==0)) 
			{
				Destroy(Cube);
			}
		}
	}
