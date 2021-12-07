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
	private RectTransform cursor;
	[SerializeField]
	private float sensitivity = 1f;
	[SerializeField]
	private float cornerEpsilon = 0.4f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		ray = new Ray(transform.position, Quaternion.Euler(-rotX, rotY, 0) * Vector3.forward);

		if (floor.Raycast(ray, out float enter))
		{
			Vector3 hitPos = ray.GetPoint(enter);
			Vector3 pos2 = new Vector3(Mathf.Round(hitPos.x), 0, Mathf.Round(hitPos.z));
			Vector3 diff = (hitPos - pos2) * 2f;

			// Draw square
			Debug.DrawLine(pos2 + new Vector3(0.5f, 0, 0.5f), pos2 + new Vector3(0.5f, 0, -0.5f));
			Debug.DrawLine(pos2 + new Vector3(0.5f, 0, 0.5f), pos2 + new Vector3(-0.5f, 0, 0.5f));
			Debug.DrawLine(pos2 + new Vector3(-0.5f, 0, -0.5f), pos2 + new Vector3(0.5f, 0, -0.5f));
			Debug.DrawLine(pos2 + new Vector3(-0.5f, 0, -0.5f), pos2 + new Vector3(-0.5f, 0, 0.5f));

			/// Figure out where in the grid is the mouse
			int x = diff.x > cornerEpsilon ? 1 : diff.x < -cornerEpsilon ? -1 : 0;
			int z = diff.z > cornerEpsilon ? 1 : diff.z < -cornerEpsilon ? -1 : 0;

			bool ns = x == 0 && z != 0;
			bool ew = x != 0 && z == 0;
			bool ne = x == 1 && z == 1;
			bool nw = x == -1 && z == 1;
			bool se = x == 1 && z == -1;
			bool sw = x == -1 && z == -1;

			// Draw line
			Debug.DrawLine(pos2, hitPos, x != 0 && z != 0 ? Color.red : Color.gray);

			// Place rails
			if (Input.GetMouseButtonDown(0))
			{
				network.addNode(new GridPos((int)pos2.x, (int)pos2.z), new RailwayNetwork.RailNode(ns, ew, ne, nw, se, sw));
			}

			// Move cursor
			cursor.position = Camera.main.WorldToScreenPoint(hitPos);
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