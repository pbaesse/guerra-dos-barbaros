using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {

	private PlacebleBuilding placeable_building;
	private Transform currentBuilding;
	private bool hasPlaced;
	public LayerMask buildingsMask;
	private PlacebleBuilding placebleBuildingOld;
	
	// Update is called once per frame
	void Update () {
		Vector3 m = Input.mousePosition;
		m = new Vector3 (m.x, m.y, transform.position.y);
		Vector3 p = camera.ScreenToWorldPoint (m);

		if (currentBuilding != null && !hasPlaced) 
		{
			currentBuilding.position = new Vector3 (p.x, 3, p.z);

			if (Input.GetMouseButtonDown (0)) 
			{
				if (isLegalPosition ()) hasPlaced = true;
			}
		} 

		{
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

	
	}
	

	bool isLegalPosition()
	{
		if (placeable_building.colliders.Count > 0) 
		{
			//renderer.material.color = Color.red;
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
}
