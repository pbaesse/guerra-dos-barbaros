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

	
	void Start () {
		
		//Declare camera limits
		cameraLimits.LeftLimit   = 0 ;
		cameraLimits.RightLimit  = terreno.terrainData.size.x ;
		cameraLimits.TopLimit    = terreno.terrainData.size.z ;
		cameraLimits.BottomLimit = 0;
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
	
		Debug.Log(terreno.terrainData.size.z);		
		bool shift = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
		float speed = m_moveSpeed * Time.deltaTime * (m_height * 3);
		if (shift)
			speed *= 2.0f;
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		// Camera movement
		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))&& (transform.position.x > cameraLimits.LeftLimit - fim || transform.position.x < cameraLimits.RightLimit + fim || transform.position.z  < cameraLimits.TopLimit -30 ||  transform.position.z > cameraLimits.BottomLimit - 15))
			camera.transform.Translate(-speed * .5f, 0.0f, 0.0f, Space.Self);
		if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))&& transform.position.x > cameraLimits.LeftLimit - fim && transform.position.x < cameraLimits.RightLimit + fim && transform.position.z  < cameraLimits.TopLimit -30 &&  transform.position.z > cameraLimits.BottomLimit - 15)
			camera.transform.Translate(speed * .5f, 0.0f, 0.0f, Space.Self);
		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))&& transform.position.x > cameraLimits.LeftLimit - fim && transform.position.x < cameraLimits.RightLimit + fim && transform.position.z  < cameraLimits.TopLimit -30 &&  transform.position.z > cameraLimits.BottomLimit - 15)
			camera.transform.Translate(0.0f, 0.0f, speed, Space.Self);
		if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))&& transform.position.x > cameraLimits.LeftLimit - fim && transform.position.x < cameraLimits.RightLimit + fim && transform.position.z  < cameraLimits.TopLimit -30 &&  transform.position.z > cameraLimits.BottomLimit - 15)
			camera.transform.Translate(0.0f, 0.0f, -speed, Space.Self);
		
		// Rotation
		if (Input.GetKey(KeyCode.Q))
			camera.transform.Rotate(new Vector3(0f, -80f * Time.deltaTime, 0f), Space.World);
		if (Input.GetKey(KeyCode.E))
			camera.transform.Rotate(new Vector3(0f, 80f * Time.deltaTime, 0f), Space.World);
		
		// Tilting
		if (m_scroll > 0.0f)
			m_scroll -= m_scroll / 10;
		else if (m_scroll < 0.0f)
			m_scroll -= m_scroll / 10;
		float scroll = -Input.GetAxis("Mouse ScrollWheel");
		if (Mathf.Abs(scroll) > 0)
		{
			m_scroll = scroll;
			m_zoomed = true;
		}
		if (mouseX < moveMargin && transform.position.x > cameraLimits.LeftLimit - fim )
		{
			Vector3 leftVec = Vector3.Cross(transform.forward, transform.up);
			leftVec.Normalize();
			leftVec *= speed;
			transform.Translate(leftVec, Space.World);
			Debug.Log("ffffff");
		}
		else if (mouseX > Screen.width - moveMargin && transform.position.x <= cameraLimits.RightLimit + fim)
		{
			Vector3 rightVec = -Vector3.Cross(transform.forward, transform.up);
			rightVec.Normalize();
			rightVec *= speed;
			transform.Translate(rightVec, Space.World);
		}
		else if (mouseY > Screen.height - moveMargin)
		{
			Vector3 upVec = new Vector3(transform.up.x, 0, transform.up.z);
			upVec.Normalize();
			upVec *= speed;
			transform.Translate(upVec, Space.World);
		}
		else if (mouseY < moveMargin)
		{
			Vector3 upVec = -(new Vector3(transform.up.x, 0, transform.up.z));
			upVec.Normalize();
			upVec *= speed;
			transform.Translate(upVec, Space.World);
		}
		m_height += m_scroll * 20f;
		m_height = Mathf.Clamp(m_height, 40, 95);
		float tiltDiff = (m_height - 50) * 0.25f;
		m_scale = 15.0f + (m_height * 0.15f);
		
		camera.transform.localEulerAngles = new Vector3(55 + tiltDiff, camera.transform.localEulerAngles.y, camera.transform.localEulerAngles.z);
		camera.transform.position = new Vector3(camera.transform.position.x, m_height, camera.transform.position.z);

	}
	

}
