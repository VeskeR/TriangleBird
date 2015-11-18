using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (-1 * Time.deltaTime, 0, 0);
		if (transform.position.x < -10)
		{
			Destroy(gameObject);
		}
	}
	public void DestroyWall()
	{
		Destroy (gameObject);
	}
}
