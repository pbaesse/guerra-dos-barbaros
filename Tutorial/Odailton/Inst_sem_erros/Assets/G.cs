using UnityEngine;
using System.Collections;

public class G : MonoBehaviour {

	public GameObject g;

	// Use this for initialization
	void Start () {
		GameObject a = (GameObject) Instantiate(g);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (0, 40, 400, 40), "Quartel Jandui")) 
		{

		}
		
	}
}
