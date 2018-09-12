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
    }

	private void Update () {
      

        if(Input.GetMouseButtonDown(0))
        {
            //set up the variable that will get the position of the mouse through the camera
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100f)) { //this checks if the ray intersected with any objects i.e. the ray (mouse position), what it hit, and the maximum distance
			
				mousePosition = (hit.point + Vector3.forward);

				towards = mousePosition - trans.position; //the vector in which the player needs to travel
			
			}
            //Debug.Log("Click"); this was just to check if the game would detect a click. 
        }
		// This will make the player face the direction that it is traveling
		Quaternion targetRotation = Quaternion.LookRotation(towards);

		trans.rotation = Quaternion.Lerp (trans.rotation, targetRotation, turnSpeed * Time.deltaTime);

			if(Vector3.Distance(trans.position, mousePosition) > radOfSat)
			{
                // this will normalize the vector to get the direction
				towards.Normalize();
				towards = towards * (moveSpeed * Time.deltaTime);
                
                // this will move the player
				trans.position += towards;
			}		
	}
}
