using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
	private Plane floor = new Plane(Vector3.up, Vector3.zero);

	private float rotY = 0f;
	private float rotX = 0f;
	private Quaternion foo = Quaternion.identity;

	[SerializeField]
	private RailwayNetwork network;
	[SerializeField]
	private float sensitivity = 1f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

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

		rotY += Input.GetAxis("Mouse X") * sensitivity;
		rotX += Input.GetAxis("Mouse Y") * sensitivity;

		rotX = Mathf.Clamp(rotX, -89.9f, -10f);

		Quaternion bar = Quaternion.Euler(-rotX, rotY, 0);

		foo = Quaternion.RotateTowards(foo, bar, Quaternion.Angle(foo, bar) * 7.5f * Time.deltaTime);

		transform.localEulerAngles = foo.eulerAngles;
	}
}