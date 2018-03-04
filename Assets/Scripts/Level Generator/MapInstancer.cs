using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstancer : MonoBehaviour {
	private LevelGenerator level_gen;
	private int[,] map;
	private int size_x, size_y;
	public GameObject floor;
	void Start () {
		level_gen = this.GetComponent<LevelGenerator> ();
		map = level_gen.GetMap ();
		size_x = map.GetLength (0);
		size_y = map.GetLength (1);
		InstantinateMap ();
	}

	void InstantinateMap() {
		for (int i = 0; i < size_x; i++) {
			for (int j = 0; j < size_y; j++) {
				if (map [i, j] == 1) {
					GameObject inst = Instantiate (floor, new Vector3 (i, 0, j), Quaternion.identity) as GameObject;
				}
			}
		}
	}
}
