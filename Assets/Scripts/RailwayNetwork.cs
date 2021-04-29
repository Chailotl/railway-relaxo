using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailwayNetwork : MonoBehaviour
{
	public struct GridPos
	{
		public int x;
		public int z;

		public GridPos(int x, int z)
		{
			this.x = x;
			this.z = z;
		}

		public static GridPos north = new GridPos(0, 1);
		public static GridPos south = new GridPos(0, -1);
		public static GridPos east = new GridPos(1, 0);
		public static GridPos west = new GridPos(-1, 0);

		public static GridPos operator +(GridPos lhs, GridPos rhs) => new GridPos(lhs.x + rhs.x, lhs.z + rhs.z);
		public static GridPos operator -(GridPos lhs, GridPos rhs) => new GridPos(lhs.x - rhs.x, lhs.z - rhs.z);
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
}