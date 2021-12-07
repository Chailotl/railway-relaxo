using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
	private Plane floor = new Plane(Vector3.up, Vector3.zero);

	[SerializeField]
	private RailwayNetwork network;

	void Update()
	{
		Vector3 hitPos = Vector3.zero;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (floor.Raycast(ray, out float enter))
		{
			hitPos = ray.GetPoint(enter);
			hitPos.x = Mathf.Round(hitPos.x);
			hitPos.z = Mathf.Round(hitPos.z);

			Debug.DrawLine(hitPos + new Vector3(0.5f, 0, 0.5f), hitPos - new Vector3(0.5f, 0, 0.5f));
			Debug.DrawLine(hitPos + new Vector3(0.5f, 0, -0.5f), hitPos - new Vector3(0.5f, 0, -0.5f));
		}

		// Camera controller
		Vector3 pos = transform.position;
		Vector3 forward = transform.forward;
		forward.y = 0;
		forward.Normalize();

		if (Input.GetKey(KeyCode.W))
		{
			pos += forward * 2f * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			pos -= forward * 2f * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A))
		{
			pos -= transform.right * 2f * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			pos += transform.right * 2f * Time.deltaTime;
		}

		transform.position = pos;
	}
}