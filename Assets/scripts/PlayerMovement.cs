using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //these will allow you to change the values in the unity editor
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;

	private float moveSpeed;
	private float turnSpeed;

	private void Start () {
		moveSpeed = 5f;
		turnSpeed = 5f;

    }

	private void Update () {

		Vector3 inputVector = Vector3.zero;
      

        if(Input.GetMouseButtonDown(0))
        {
            inputVector += Vector3.MoveTowards(trans.position, Input.mousePosition, 1000f);
            //Debug.Log("Click");
        }

		// Normalize input vector to standardize movement speed
		inputVector.Normalize ();
		inputVector *= moveSpeed;
		rb.velocity = inputVector;

		// Face player along movement vector
		Quaternion targetRotation = Quaternion.LookRotation (inputVector);

		trans.rotation = Quaternion.Lerp (trans.rotation, targetRotation, turnSpeed * Time.deltaTime);
	}
}
