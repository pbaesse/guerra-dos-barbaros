using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	//public float minDistanceToBorder;
	public float movementVelocity;

	void Update()
	{
		float scrHeight = Screen.height;
		float scrWidth = Screen.width;

		Vector2 mouse = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		float yBorder = scrHeight - mouse.y;
		float xBorder = scrWidth - mouse.x;

		Vector3 movimento = Vector3.zero;

		// o 0.5 equivale a distancia minima entre o componente em questao e a borda

		if (yBorder < 5)
		{
			movimento = new Vector3(movimento.x, movimento.y, 5 - yBorder);
		}
		else if (mouse.y < 5)
		{
			movimento = new Vector3(movimento.x, movimento.y, -(5 - mouse.y));
		}

		if (xBorder < 5)
		{
			movimento = new Vector3(5 - xBorder, movimento.y, movimento.z);
		}
		else if (mouse.x < 5)
		{
			movimento = new Vector3(-(5 - mouse.x), movimento.y, movimento.z);
		}

		float foward = Input.GetAxis("Foward") * 18;
		float right = Input.GetAxis("Right") * 18;
		movimento = new Vector3 (movimento.x + right, movimento.y, movimento.z + foward);

		transform.Translate (movimento * Time.deltaTime * movementVelocity, Space.World);
	}
}

