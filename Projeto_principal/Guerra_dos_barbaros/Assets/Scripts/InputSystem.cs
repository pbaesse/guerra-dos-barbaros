using UnityEngine;
using System;
using System.Collections.Generic;

public class InputSystem : MonoBehaviour
{
	void Start()
	{
		animation.Play ();

	}
	
	void Update()
	{
		Debug.Log ("aa"+animation.isPlaying);

		animation.Play ("Armature|ArmatureAction.002");
	}
	
}