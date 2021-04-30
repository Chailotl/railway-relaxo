using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
	public enum Direction
	{
		NORTH, SOUTH, EAST, WEST
	}

	private RailwayNetwork.RailNode node;
	private Direction direction;
	private Vector3 toPos;

	void Start()
	{
		node = RailwayNetwork.railNodes[new RailwayNetwork.GridPos(0, 0)];
		direction = Direction.NORTH;
		toPos = new Vector3(0, 0, 0.5f);
	}

	void Update()
	{
		Vector3 pos = transform.position;

		pos = Vector3.MoveTowards(pos, toPos, 2 * Time.deltaTime);

		if (pos == toPos)
		{
			if (direction == Direction.NORTH && node.northNode != null)
			{
				node = node.northNode;
				toPos += new Vector3(0, 0, 0.5f);
			}
			else if (direction == Direction.EAST && node.eastNode != null)
			{
				node = node.eastNode;
				toPos += new Vector3(0.5f, 0, 0);
			}
			else if (direction == Direction.SOUTH && node.southNode != null)
			{
				node = node.southNode;
				toPos += new Vector3(0, 0, -0.5f);
			}
			else if (direction == Direction.WEST && node.westNode != null)
			{
				node = node.westNode;
				toPos += new Vector3(-0.5f, 0, 0);
			}

			if (direction != Direction.SOUTH && node.northNode != null)
			{
				direction = Direction.NORTH;
				toPos += new Vector3(0, 0, 0.5f);
			}
			else if (direction != Direction.WEST && node.eastNode != null)
			{
				direction = Direction.EAST;
				toPos += new Vector3(0.5f, 0, 0);
			}
			else if (direction != Direction.NORTH && node.southNode != null)
			{
				direction = Direction.SOUTH;
				toPos += new Vector3(0, 0, -0.5f);
			}
			else if (direction != Direction.EAST && node.westNode != null)
			{
				direction = Direction.WEST;
				toPos += new Vector3(-0.5f, 0, 0);
			}
		}

		transform.position = pos;
	}
}