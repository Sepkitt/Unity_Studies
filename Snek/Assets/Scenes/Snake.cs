using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Snake : MonoBehaviour
{
	private Vector2 _direction = Vector2.right;

	private List<Transform> _segments;

	public Transform segmentPrefab;


	private void Start() {
		_segments = new List<Transform>();
		_segments.Add(this.transform);
		
	}

	private void Update() 
	
	{
		//Key Controls

		if(Input.GetKeyDown(KeyCode.W)) {
			_direction = Vector2.up;
		} else if(Input.GetKeyDown(KeyCode.S)) {
			_direction = Vector2.down;
		} else if(Input.GetKeyDown(KeyCode.A)) {
			_direction = Vector2.left;
		} else if(Input.GetKeyDown(KeyCode.D)) {
			_direction = Vector2.right;
		}
		
		
	}
	
	private void FixedUpdate() {

			for (int  i = _segments.Count -1; i > 0; i--) {
				_segments[i].position = _segments[i-1].position;
			}
		

		//  Transform properties should be round so that all numbers are 
		//  whole value so that it can be aligned into a grid
		
		this.transform.position = new Vector3(
			// Vector 3 uses (x,y,z)
			Mathf.Round(this.transform.position.x) + _direction.x,
			Mathf.Round(this.transform.position.y) + _direction.y,
			0.0f

		);
	}

	private void Grow() {
		Transform segment = Instantiate(this.segmentPrefab);
		//Grab last elementin current list of segments
		// Set position of new segment to match tail object
		segment.position = _segments[_segments.Count - 1].position;

		_segments.Add(segment);

	}

	private void ResetState() {
		for (int i = 1; i < _segments.Count; i++) {
			Destroy(_segments[i].gameObject);
		}

		_segments.Clear();
		_segments.Add(this.transform);

		this.transform.position = Vector3.zero;

	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Food") {
			Grow();
		} else if(other.tag == "Obsticle") {
			ResetState();

		}
		
	}
	

}
