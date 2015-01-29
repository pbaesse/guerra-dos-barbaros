using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {

	private PlacebleBuilding placeable_building;
	private Transform currentBuilding;
	public bool hasPlaced;
	public LayerMask buildingsMask;
	private PlacebleBuilding placebleBuildingOld;

	public GameObject[] buildings;
	private BuildingPlacement buildin_placement;
	private string buildingName;
	

	void Start() {
		buildin_placement = GetComponent<BuildingPlacement>();
	}

	public bool HasPlaced 
	{
		get { return hasPlaced; }
	}
	

	void Update () {
		Vector3 m = Input.mousePosition;
		m = new Vector3 (m.x, m.y, transform.position.y);
		Vector3 p = camera.ScreenToWorldPoint (m);

		if (currentBuilding != null && !hasPlaced) 
		{
			currentBuilding.position = new Vector3 (p.x, 3, p.z);

			if (Input.GetMouseButtonDown (0)) 
			{
				if (isLegalPosition ()) 
				{
					hasPlaced = true;
					placeable_building.placed = true;
					currentBuilding.GetComponent<MeshRenderer>().enabled = true;
					//currentBuilding.GetComponentInChildren<MeshRenderer>().enabled = false;
				}
			}
		} 
		
			if (Input.GetMouseButtonDown (0)) 
			{
				RaycastHit hit = new RaycastHit();
				Ray ray = new Ray(new Vector3(p.x, 11, p.z), Vector3.down);
				if(Physics.Raycast(ray, out hit, Mathf.Infinity, buildingsMask))
				{
					if(placebleBuildingOld != null) placebleBuildingOld.SetSelected(false);

					hit.collider.gameObject.GetComponent<PlacebleBuilding>().SetSelected(true);
					placebleBuildingOld = hit.collider.gameObject.GetComponent<PlacebleBuilding>();
				}

				else 
				{
					if(placebleBuildingOld != null) placebleBuildingOld.SetSelected(false);
				}

			}


	}


	

	bool isLegalPosition()
	{
		if (placeable_building.colliders.Count > 0) 
		{
			return false;

		} 
		return true;
	}

	public void SetItem(GameObject b)
	{
		hasPlaced = false;
		currentBuilding = ((GameObject)Instantiate (b)).transform;
		placeable_building = currentBuilding.GetComponent<PlacebleBuilding> ();
	}

	void OnGUI()
	{
		for (int i = 0; i < buildings.Length; i++) 
		{
			if(GUI.Button(new Rect(Screen.width/20, Screen.height/15+Screen.height/12*i,100, 30), buildings[i].name))
			{
				buildin_placement.SetItem((GameObject)buildings[i]);
				//buildings[i].renderer.material.shader = Shader.Find("Transparent/Diffuse");


			}
		}
	}

	/*GameObject Ghost(GameObject obj)
	{
		GameObject m_ghost = new GameObject();
		m_ghost.transform.rotation = obj.transform.rotation;
		m_ghost.transform.localScale = obj.transform.localScale;
		MeshRenderer ghostRender = m_ghost.AddComponent<MeshRenderer>();
		MeshFilter ghostMesh = m_ghost.AddComponent<MeshFilter>();
		ghostMesh.mesh = obj.GetComponent<MeshFilter>().mesh;
		ghostRender.material = obj.GetComponent<MeshRenderer>().material;
		ghostRender.material.color = new Color(1f, 1f, 1f, 0.5f);
		ghostRender.material.shader = Shader.Find("Transparent/Diffuse");
		return m_ghost;
	}*/
}
