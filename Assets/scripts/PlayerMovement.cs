using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //these will allow you to change the values in the unity editor
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
	[SerializeField] private float radOfSat;

	private float moveSpeed;
	private float turnSpeed;
	private Vector3 mousePosition;
	private Vector3 towards = Vector3.zero;

	private void Start () {
		moveSpeed = 7f;
		turnSpeed = 5f;
		mousePosition = Vector3.zero;
		//towards = trans.position;
    }

	private void Update () {
      

        if(Input.GetMouseButtonDown(0))
        {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100f)) {
			
				mousePosition = (hit.point + Vector3.up);

				towards = mousePosition - trans.position;
			
			}
            //Debug.Log("Click");
        }
		// Face player along movement vector
		Quaternion targetRotation = Quaternion.LookRotation(towards);

		trans.rotation = Quaternion.Lerp (trans.rotation, targetRotation, turnSpeed * Time.deltaTime);

			if(Vector3.Distance(trans.position, mousePosition) > radOfSat)
			{
				towards.Normalize();
				towards = towards * (moveSpeed * Time.deltaTime);

				transform.position += towards;
				
			}

		
	}
}
