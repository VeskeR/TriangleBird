using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {
	private Vector3 birdMovement = new Vector3(0,1,0);
	private bool clicked = false;
	private Vector3 rotationDot;
	public Vector3 toRotateVector;
	public GameManager manager;
	private Transform normalTransform;
	public Quaternion normalRotation;

	// Use this for initialization
	void Start () {
		manager = manager.GetComponent<GameManager> ();
		normalRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			if (!clicked)
			{
				rigidbody.velocity = new Vector3(0,0,0);
				rigidbody.AddForce(birdMovement * 150);
			}
			clicked = true;
		} else
		{
			clicked = false;
		}

		rotationDot = new Vector3 (1,rigidbody.velocity.y * 2 + 1,0);
		toRotateVector = rotationDot - transform.position;
		Quaternion rotation = Quaternion.LookRotation (toRotateVector);
		Quaternion current = transform.localRotation;
		transform.localRotation = Quaternion.Slerp (current, rotation, Time.deltaTime);

		if (transform.position.y > 2.85f)
		{
			transform.position = new Vector3(0f,2.85f,0f);
		}
	}

	void OnCollisionEnter (Collision o)
	{
		manager.RestartMenu ();
	}
}
