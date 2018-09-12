using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //these will allow you to change the values in the unity editor
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
	[SerializeField] private float radOfSat; //radius of satisfaction

	private float moveSpeed; //move speed
	private float turnSpeed; //turn speed
	private Vector3 mousePosition; //mouse position
	private Vector3 towards = Vector3.zero; //vector for the direction the player needs to go
    /*
     * So I kept running into a problem where the player either sunk into the ground or floated above it.
     * On line 43 I had it say mousePosition = (hit.point + Vector3.up) which made the player float or mousePosition = (hit.point + Vector3.forward) which made the player sink.
     * So I decided to make a custom vector with a y-axis value of my choice to try and get it to not sink/float.
     * Since Vector3.up is (0,1,0) and Vector3.forward is (0,0,1), 0.5f is the halfway point which means the character should rest right on the plane
     */
    private Vector3 dontSinkOrFloat; //custom vector to prevent floating or sinking

    private void Start () {
		moveSpeed = 7f;
		turnSpeed = 5f;
		mousePosition = Vector3.zero;
    }

	private void Update () {
      
        if(Input.GetMouseButtonDown(0)) //if the mouse is clicked
        {
            //set up the variable that will get the position of the mouse through the camera
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100f)) { //this checks if the ray intersected with any objects i.e. the ray (mouse position), what it hit, and the maximum distance


                dontSinkOrFloat[1] = 0.5f; // basically makes the vector equal this --> [0, 0.5f, 0]

				mousePosition = (hit.point + dontSinkOrFloat); //Don't sink or float!!!

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