using UnityEngine;
using System.Collections;

public class controlador_camera : MonoBehaviour {
	//box limits Struct


	public struct BoxLimit {
		public float LeftLimit;
		public float RightLimit;
		public float TopLimit;
		public float BottomLimit;
		
	}
	

	public static BoxLimit cameraLimits       = new BoxLimit();
	public static BoxLimit mouseScrollLimits  = new BoxLimit();

	public float m_moveSpeed;
	private float m_height;
	private float m_scroll;
	private float m_scale;
	private bool m_zoomed;
	public int moveMargin = 5;
	public Terrain terreno;
	public float fim = 5f;
	private quadrado quadrados;

	
	void Start () {
		
		//Declare camera limits
		cameraLimits.LeftLimit   = 0 ;
		cameraLimits.RightLimit  = terreno.terrainData.size.x ;
		cameraLimits.TopLimit    = terreno.terrainData.size.z ;
		cameraLimits.BottomLimit = 0;
		quadrados = GetComponent<quadrado> ();
	}
	
	public float Scale()
	{
		return m_scale;
	}
	
	public bool Zoomed()
	{
		if (m_zoomed)
		{
			m_zoomed = false;
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void SetPos(Vector3 a_pos)
	{
		transform.position = a_pos;
		m_height = a_pos.y;
	}

	// Update is called once per frame
	void Update()
	{

		if(!quadrados.fazendo_quadrado){
			bool shift = (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift));
			float speed = m_moveSpeed * Time.deltaTime * (m_height * 3);
			if (shift)
				speed *= 2.0f;
			float mouseX = Input.mousePosition.x;
			float mouseY = Input.mousePosition.y;
			// Camera movement
			if ((Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) && transform.position.x > cameraLimits.LeftLimit - fim )
				GetComponent<Camera>().transform.Translate (-speed , 0.0f, 0.0f, Space.Self);
			if ((Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) &&transform.position.x < cameraLimits.RightLimit + fim )
				GetComponent<Camera>().transform.Translate (speed , 0.0f, 0.0f, Space.Self);
			if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && transform.position.z < cameraLimits.TopLimit - 30)
				GetComponent<Camera>().transform.Translate (0.0f, 0.0f, speed, Space.Self);
			if ((Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) && transform.position.z > cameraLimits.BottomLimit - 15)
				GetComponent<Camera>().transform.Translate (0.0f, 0.0f, -speed, Space.Self);
			

			
			if (mouseX < moveMargin && transform.position.x > cameraLimits.LeftLimit - fim) {
				Vector3 leftVec = Vector3.Cross (transform.forward, transform.up);
				leftVec.Normalize ();
				leftVec *= speed;
				transform.Translate (leftVec, Space.World);
			
			} else if (mouseX > Screen.width - moveMargin && transform.position.x <= cameraLimits.RightLimit + fim) {
				Vector3 rightVec = -Vector3.Cross (transform.forward, transform.up);
				rightVec.Normalize ();
				rightVec *= speed;
				transform.Translate (rightVec, Space.World);
			} else if (mouseY > Screen.height - moveMargin &&  transform.position.z < cameraLimits.TopLimit - 30) {
				Vector3 upVec = new Vector3 (transform.up.x, 0, transform.up.z);
				upVec.Normalize ();
				upVec *= speed;
				transform.Translate (upVec, Space.World);

			} else if (mouseY < moveMargin &&  transform.position.z > cameraLimits.BottomLimit - 15) {
				Vector3 upVec = -(new Vector3 (transform.up.x, 0, transform.up.z));
				upVec.Normalize ();
				upVec *= speed;
				transform.Translate (upVec, Space.World);
			}
			
		}
		// Tilting
		if (m_scroll > 0.0f)
			m_scroll -= m_scroll / 10;
		else if (m_scroll < 0.0f)
			m_scroll -= m_scroll / 10;
		float scroll = -Input.GetAxis ("Mouse ScrollWheel");
		if (Mathf.Abs (scroll) > 0) {
			m_scroll = scroll;
			m_zoomed = true;
		}
		m_height += m_scroll * 100f;
		m_height = Mathf.Clamp (m_height, 40, 95);
		float tiltDiff = (m_height - 50) * 0.25f;
		m_scale = 15.0f + (m_height * 0.15f);
		
		GetComponent<Camera>().transform.localEulerAngles = new Vector3 (55 + tiltDiff, GetComponent<Camera>().transform.localEulerAngles.y, GetComponent<Camera>().transform.localEulerAngles.z);
		GetComponent<Camera>().transform.position = new Vector3 (GetComponent<Camera>().transform.position.x, m_height, GetComponent<Camera>().transform.position.z);
		
	}


}
