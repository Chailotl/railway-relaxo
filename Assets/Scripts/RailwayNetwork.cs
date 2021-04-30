using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailwayNetwork : MonoBehaviour
{
	public static Dictionary<GridPos, RailNode> railNodes = new Dictionary<GridPos, RailNode>();

	[SerializeField]
	private GameObject straightTrack;
	[SerializeField]
	private GameObject angleTrack;

	public struct GridPos : IEquatable<GridPos>
	{
		public readonly int x;
		public readonly int z;

		public GridPos(int x, int z)
		{
			this.x = x;
			this.z = z;
		}

		public static GridPos north = new GridPos(0, 1);
		public static GridPos south = new GridPos(0, -1);
		public static GridPos east = new GridPos(1, 0);
		public static GridPos west = new GridPos(-1, 0);

		public override bool Equals(object other) => other is GridPos && Equals(other);
		public bool Equals(GridPos other) => this == other;
		public override int GetHashCode()
		{
			int hash = 17;
			hash = hash * 23 + x.GetHashCode();
			hash = hash * 23 + z.GetHashCode();
			return hash;
		}

		public static GridPos operator +(GridPos lhs, GridPos rhs) => new GridPos(lhs.x + rhs.x, lhs.z + rhs.z);
		public static GridPos operator -(GridPos lhs, GridPos rhs) => new GridPos(lhs.x - rhs.x, lhs.z - rhs.z);
		public static bool operator ==(GridPos lhs, GridPos rhs) => lhs.x == rhs.x && lhs.z == rhs.z;
		public static bool operator !=(GridPos lhs, GridPos rhs) => !(lhs == rhs);
		public static implicit operator Vector3(GridPos pos) => new Vector3(pos.x, 0, pos.z);
	}

	public class RailNode
	{
		public RailNode northNode;
		public RailNode southNode;
		public RailNode eastNode;
		public RailNode westNode;

		public bool ns;
		public bool ew;

		public bool ne;
		public bool nw;
		public bool se;
		public bool sw;

		public RailNode(bool ns, bool ew, bool ne, bool nw, bool se, bool sw)
		{
			this.ns = ns;
			this.ew = ew;
			this.ne = ne;
			this.nw = nw;
			this.se = se;
			this.sw = sw;
		}
	}

	void Awake()
	{
		addNode(new GridPos(0, 0), new RailNode(true, false, false, false, false, false));
		addNode(new GridPos(0, 1), new RailNode(false, false, false, false, true, false));
		addNode(new GridPos(1, 1), new RailNode(false, true, false, false, false, false));
		addNode(new GridPos(2, 1), new RailNode(false, false, false, false, false, true));
		addNode(new GridPos(2, 0), new RailNode(true, false, false, false, false, false));
		addNode(new GridPos(2, -1), new RailNode(false, false, false, true, false, false));
		addNode(new GridPos(1, -1), new RailNode(false, true, false, false, false, false));
		addNode(new GridPos(0, -1), new RailNode(false, false, true, false, false, false));
	}

	public bool addNode(GridPos pos, RailNode node)
	{
		if (railNodes.ContainsKey(pos)) { return false; }

		railNodes.Add(pos, node);

		// Add game objects and connect nodes
		if (node.ns)
		{
			joinWithNorthNode(pos, node);
			joinWithSouthNode(pos, node);
			Instantiate(straightTrack, pos, Quaternion.identity);
		}
		if (node.ew)
		{
			joinWithEastNode(pos, node);
			joinWithWestNode(pos, node);
			Instantiate(straightTrack, pos, Quaternion.Euler(0, 90, 0));
		}
		if (node.ne)
		{
			joinWithNorthNode(pos, node);
			joinWithEastNode(pos, node);
			Instantiate(angleTrack, pos, Quaternion.Euler(0, 180, 0));
		}
		if (node.nw)
		{
			joinWithNorthNode(pos, node);
			joinWithWestNode(pos, node);
			Instantiate(angleTrack, pos, Quaternion.Euler(0, 90, 0));
		}
		if (node.se)
		{
			joinWithSouthNode(pos, node);
			joinWithEastNode(pos, node);
			Instantiate(angleTrack, pos, Quaternion.Euler(0, 270, 0));
		}
		if (node.sw)
		{
			joinWithSouthNode(pos, node);
			joinWithWestNode(pos, node);
			Instantiate(angleTrack, pos, Quaternion.identity);
		}

		return true;
	}

	public void joinWithNorthNode(GridPos pos, RailNode node)
	{
		if (railNodes.TryGetValue(pos + GridPos.north, out RailNode otherNode))
		{
			node.northNode = otherNode;
			otherNode.southNode = node;
		}
	}

	public void joinWithSouthNode(GridPos pos, RailNode node)
	{
		if (railNodes.TryGetValue(pos + GridPos.south, out RailNode otherNode))
		{
			node.southNode = otherNode;
			otherNode.northNode = node;
		}
	}

	public void joinWithEastNode(GridPos pos, RailNode node)
	{
		if (railNodes.TryGetValue(pos + GridPos.east, out RailNode otherNode))
		{
			node.eastNode = otherNode;
			otherNode.westNode = node;
		}
	}

	public void joinWithWestNode(GridPos pos, RailNode node)
	{
		if (railNodes.TryGetValue(pos + GridPos.west, out RailNode otherNode))
		{
			node.westNode = otherNode;
			otherNode.eastNode = node;
		}
	}
}