using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public static Animator anim;
	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	
	void Start()
	{
		anim = GetComponent<Animator>();
	}
	
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (CrossPlatformInputManager.GetButtonDown("Jump"))
		{
				jump = true;
		}
		if (CrossPlatformInputManager.GetButtonDown("Crouch"))
		{
			//PlayerMovement.anim.Play("crowl");
			crouch = true;
		}
	}	
	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
		}
	
	
}
