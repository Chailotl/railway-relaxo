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

		if (hitPos != null && Input.GetMouseButtonDown(0))
		{
			network.addNode(new GridPos((int)hitPos.x, (int)hitPos.z), new RailwayNetwork.RailNode(true, true, true, true, true, true));
		}
	}
}