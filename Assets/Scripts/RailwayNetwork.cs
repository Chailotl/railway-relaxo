using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailwayNetwork : MonoBehaviour
{
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