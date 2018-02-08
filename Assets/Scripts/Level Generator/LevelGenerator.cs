using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour{
	private void GenerateTunnel(int turns, int length, ref int[,] map, Vector2I start, int size = 1) {
		int tiles = 0;
		Vector2I position = new Vector2I (start);
		System.Random rand = new System.Random ();
		Vector2I direction = new Vector2I (0, 0);
		if (rand.Next (0, 2) == 0)
			direction.x += 1 - 2 * rand.Next (0, 2);
		else direction.y += 1 - 2 * rand.Next (0, 2);
		Debug.Log ("Start direction: " + direction.ToString ());
		int[] turnPoints = new int[turns];
		int straightLen = Convert.ToInt32(length / (turns+1));
		for (int i = 0; i < turns; i++) {
			turnPoints [i] = straightLen * (i + 1);
			turnPoints [i] += rand.Next (-straightLen + 1, straightLen);
		}
		int turn = 0;
		//Debug.Log ("DIRECTION: " + direction.ToString ());
		while (tiles < length) {
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++) {
					if (position.x + i < map.GetLength (0) && position.y + j < map.GetLength (1) && position.x + i >= 0 && position.y + i >= 0)
						map [position.x + i, position.y + j] = 1;
				}
			position += direction;
			//Debug.Log ("position: " + position.ToString ());
			tiles++;
			if (turn < turns) {
				if (tiles == turnPoints [turn]) {
					turn++;
					int side = 1 - 2 * rand.Next (0, 2);
					if (direction.x == 0) {
						direction.x = side;
						direction.y = 0;
					} else {
						direction.y = side;
						direction.x = 0;
					}
					//Debug.Log ("DIRECTION: " + direction.ToString ());
				}
			}
			//probably there is a simplier way to do the following check, but I'll leave it like that for now
			if (position.x + direction.x > map.GetLength (0) || position.x + direction.x < 0) {
				turn++;
				direction.x = 0;
				direction.y = 1;
				if (position.y + direction.y >= map.GetLength (1))
					direction.y = -1;
				//Debug.Log ("DIRECTION: " + direction.ToString ());
			}
			if (position.y + direction.y > map.GetLength (1) || position.y + direction.y < 0) {
				turn++;
				direction.y = 0;
				direction.x = 1;
				if (position.x + direction.x >= map.GetLength (0))
					direction.x = -1;
				//Debug.Log ("DIRECTION: " + direction.ToString ());
			}
		}
	}

	private void GenerateRoom(int size_x, int size_y, ref int[,] map, Vector2I place) {
		for (int i = 0; i < size_x; i++)
			for (int j = 0; j < size_y; j++) {
				if (place.x + i < map.GetLength (0) && place.y + j < map.GetLength (1) && place.x + i >= 0 && place.y + i >= 0)
					map [place.x + i, place.y + j] = 1;
			}
	}

	public float LOW_PART_TUNNEL_LENG = 1/3f;
	public float HIGH_PART_TUNNEL_LENG = 2f;
	public float TOTAL_ROOM_PART = 1/2f;
	public float ROOM_SIZE_LOW_BOUND = 1/3f;
	public float ROOM_SIZE_UPPER_BOUND = 1.5f;
	public float SIDE_LOW_BOUND_SQRT = 1/2f;
	public float SIDE_UPPER_BOUND_FULL = 0.7f;
	private int[,] GenerateLevel(int size_x, int size_y, int tunnels, int rooms = 0) {
		int[,] bitmap = new Int32[size_x, size_y];
		System.Random rand = new System.Random ();
		for (int i = 0; i < tunnels; i++) {
			int length = rand.Next (Mathf.RoundToInt ((size_x + size_y) * LOW_PART_TUNNEL_LENG), Mathf.RoundToInt ((size_x + size_y) * HIGH_PART_TUNNEL_LENG));
			int turns = rand.Next (0, Convert.ToInt32 (Math.Sqrt (length)));
			Vector2I start = new Vector2I(rand.Next(0,size_x), rand.Next(0,size_y));
			Debug.Log ("Tunnel length: " + length + ", turns: " + turns);
			Debug.Log ("Tunnel start: " + start.ToString ());
			GenerateTunnel (turns, length, ref bitmap, start);
		}

		float s_std = size_x * size_y * TOTAL_ROOM_PART / rooms;
		for (int i = 0; i < rooms; i++) {
			int s_act = rand.Next (Mathf.RoundToInt (s_std * ROOM_SIZE_LOW_BOUND), Mathf.RoundToInt (s_std * ROOM_SIZE_UPPER_BOUND));
			int x_side = rand.Next(Mathf.RoundToInt(Mathf.Sqrt(s_act)*SIDE_LOW_BOUND_SQRT), Mathf.RoundToInt(s_act*SIDE_UPPER_BOUND_FULL));
			int y_side = Mathf.RoundToInt (s_act / x_side);
			Vector2I start = new Vector2I(rand.Next(0,size_x), rand.Next(0,size_y));
			Debug.Log ("Room dimensions: " + x_side + " " + y_side);
			Debug.Log ("Room position: " + start.ToString ());
			GenerateRoom (x_side, y_side, ref bitmap, start);
		}
		return bitmap;
	}

	public int x_length = 10;
	public int y_length = 10;
	public int tunnels_number = 4;
	public int rooms_number = 2;
	public int[,] GetMap() {
		return GenerateLevel (x_length, y_length, tunnels_number, rooms_number);
	}

	void Start() {
		/*int[,] map = GenerateLevel (x_length, y_length, tunnels_number, rooms_number);
		for (int i = 0; i<map.GetLength(0); i++) {
			string str = "";
			for (int j = 0; j < map.GetLength (1); j++)
				str += map [i, j] + " ";
			Debug.Log (str);
		}*/
	}
}
