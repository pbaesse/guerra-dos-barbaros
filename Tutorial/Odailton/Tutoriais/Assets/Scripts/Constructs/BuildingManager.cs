using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	public GameObject[] buildings;
	private BuildingPlacement buildin_placement;

	// Use this for initialization
	void Start () {
		buildin_placement = GetComponent<BuildingPlacement>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		for (int i = 0; i < buildings.Length; i++) 
		{
			if(GUI.Button(new Rect(Screen.width/20, Screen.height/15+Screen.height/12*i,100, 30), buildings[i].name))
			{
				buildin_placement.SetItem((GameObject)buildings[i]);
			}
		}
	}

}
