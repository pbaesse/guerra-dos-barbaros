using UnityEngine;
using System.Collections;

public class BuildingPlacer : MonoBehaviour 
{
	private GameObject _building = null;
	public GameObject model = new GameObject();
	
	public bool IsPlacing 
	{
		get { return _building != null; }
	}
	
	public void Create ()
	{
		DestroyCurrent ();
		InstantiateNewBuilding (string.Empty); // incompleto
	}
	
	public void DestroyCurrent ()
	{
		if (_building != null) 
		{
			Destroy(_building);
			_building = null;
		}
	}
	
	private void InstantiateNewBuilding (string path)
	{
		// buscar a construção disponivel no caminho passado como parametro
		//_building = GameObject.CreatePrimitive (PrimitiveType.Cube); // incompleto
		_building = (GameObject) Instantiate (model);
		//_building.GetComponent<BoxCollider> ().enabled = false;
	}
	
	private void PlaceBuilding ()
	{
		//GameObject newBuilding = GameObject.CreatePrimitive (PrimitiveType.Cube);
		GameObject newBuilding = (GameObject) Instantiate (model);
		newBuilding.transform.position = _building.transform.position;

	}
	
	void Update () 
	{
		if (IsPlacing) 
		{
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(r.origin, r.direction, out hit))
			{
				_building.transform.position = hit.point;
			}
			
			if(Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(1))
			{
				DestroyCurrent();
			}
			
			if(Input.GetButtonDown("Fire1"))
			{
				PlaceBuilding();
				DestroyCurrent();
			}
		}
	}
}
