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
	private GridPos gridPos;
	private Direction direction;
	private Vector3 toPos;

	void Start()
	{
		node = RailwayNetwork.railNodes[new GridPos(0, 0)];
		gridPos = new GridPos(0, 0);
		direction = Direction.NORTH;
		toPos = new Vector3(0, 0, 0.5f);
	}

	void Update()
	{
		Vector3 pos = transform.position;

		pos = Vector3.MoveTowards(pos, toPos, 2 * Time.deltaTime);

		if (pos == toPos)
		{
			RailwayNetwork.RailNode nextNode = null;

			if (direction == Direction.NORTH && node.northNode != null)
			{
				nextNode = node.northNode;
			}
			else if (direction == Direction.EAST && node.eastNode != null)
			{
				nextNode = node.eastNode;
			}
			else if (direction == Direction.SOUTH && node.southNode != null)
			{
				nextNode = node.southNode;
			}
			else if (direction == Direction.WEST && node.westNode != null)
			{
				nextNode = node.westNode;
			}

			if (nextNode != null)
			{
				toPos = Vector3.Lerp(node.pos, nextNode.pos, 0.5f);
				node = nextNode;
			}

			// Find and pick an available path
			List<Direction> directions = new List<Direction>();

			if (direction == Direction.NORTH)
			{
				if (node.ns) { directions.Add(Direction.NORTH); }
				if (node.se) { directions.Add(Direction.EAST); }
				if (node.sw) { directions.Add(Direction.WEST); }
			}
			else if (direction == Direction.EAST)
			{
				if (node.ew) { directions.Add(Direction.EAST); }
				if (node.nw) { directions.Add(Direction.NORTH); }
				if (node.sw) { directions.Add(Direction.SOUTH); }
			}
			else if (direction == Direction.SOUTH)
			{
				if (node.ns) { directions.Add(Direction.SOUTH); }
				if (node.ne) { directions.Add(Direction.EAST); }
				if (node.nw) { directions.Add(Direction.WEST); }
			}
			else if (direction == Direction.WEST)
			{
				if (node.ew) { directions.Add(Direction.WEST); }
				if (node.ne) { directions.Add(Direction.NORTH); }
				if (node.se) { directions.Add(Direction.SOUTH); }
			}

			if (directions.Count != 0)
			{
				direction = directions[Random.Range(0, directions.Count)];
			}
			else
			{
				// Dead-end, try flipping
				FlipDirection();
			}
		}

		transform.position = pos;
	}

	private void FlipDirection()
	{
		switch (direction)
		{
			case Direction.NORTH:
				direction = Direction.SOUTH;
				break;
			case Direction.SOUTH:
				direction = Direction.NORTH;
				break;
			case Direction.EAST:
				direction = Direction.WEST;
				break;
			case Direction.WEST:
				direction = Direction.EAST;
				break;
		}
	}
}