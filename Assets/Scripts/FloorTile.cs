using UnityEngine;
using System.Collections;

/**
 * Build a simple floor tile in alternating red and white checks, 1m a side.
 * x-z is the floor. Origin is (x0, y0) and the tile floor is n squares in x
 * and m squares in z.
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 */

public static class FloorTile  {

	public static void Floor(float x0, float z0, int n, int m) {

		Quaternion pose = Quaternion.Euler (90, 0, 0);

		for (int i=0; i<n; i++) {				
			bool white = (i % 2) == 0;

			for (int j=0; j<m; j++) {
				GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Quad);
				obj.transform.position = new Vector3 (x0 + i, 0, z0 + j);
				obj.transform.rotation = pose;
				if (white)
					obj.GetComponent<Renderer>().material.color = new Color (255, 255, 255);
				else
					obj.GetComponent<Renderer>().material.color = new Color (255, 0, 0);
				white = !white;
			}
		}
	}
	
}
