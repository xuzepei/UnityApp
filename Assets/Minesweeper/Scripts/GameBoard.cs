using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

	public GameObject brickPrefab;

	public static int row = 13;
	public static int column = 10;
	public static Brick[,] bricks;
	public static bool[,] visited;

	// Use this for initialization
	void Start () {

		Restart ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Restart() {

		bricks = null;
		visited = null;

		bricks = new Brick[column,row];

		if (brickPrefab) {

			Vector3 scale = brickPrefab.transform.localScale;
			int columnNumber = 10;
			int w = 0;
			int h = 0;

			for (int i = 0; i < row * column; i++) {

				h = i / columnNumber;
				w = i % columnNumber;

				Brick brick = Instantiate (brickPrefab, new Vector3 (w * scale.x, h * scale.y, 0), Quaternion.identity).GetComponent<Brick> ();
				brick.x = w;
				brick.y = h;

				bricks[w,h] = brick;


			}
		}

		visited = new bool[column,row];
		for (int i = 0; i < column ; i++) 
			for (int j = 0; j < row; j++){
				visited [i, j] = false;
		}
	}

	public static bool mineAt(int x, int y) {
		// Coordinates in range? Then check for mine.
		if (x >= 0 && y >= 0 && x < column && y < row)
			return bricks[x, y].isMine;
		
		return false;
	}

	public static int adjacentMines(int x, int y) {
		int count = 0;

		if (mineAt(x,   y+1)) ++count; // top
		if (mineAt(x+1, y+1)) ++count; // top-right
		if (mineAt(x+1, y  )) ++count; // right
		if (mineAt(x+1, y-1)) ++count; // bottom-right
		if (mineAt(x,   y-1)) ++count; // bottom
		if (mineAt(x-1, y-1)) ++count; // bottom-left
		if (mineAt(x-1, y  )) ++count; // left
		if (mineAt(x-1, y+1)) ++count; // top-left

		return count;
	}

	public static bool floodFill(int x, int y) {

		Brick brick = findBrick (x, y);
		if (brick == null)
			return false;

		if (brick.isMine)
			return false;
	
		int count = adjacentMines(x,   y);
		brick.LoadSprite ((BrickType)count);

		if (floodFill(x,   y+1)) ++count; // top
		if (floodFill(x+1, y+1)) ++count; // top-right
		if (floodFill(x+1, y  )) ++count; // right
		if (floodFill(x+1, y-1)) ++count; // bottom-right
		if (floodFill(x,   y-1)) ++count; // bottom
		if (floodFill(x-1, y-1)) ++count; // bottom-left
		if (floodFill(x-1, y  )) ++count; // left
		if (floodFill(x-1, y+1)) ++count; // top-left

		if (count == 0) {
			//Debug.Log ("Mines' count is " + count);

			return true;
		}

		return false;
	}

	public static Brick findBrick(int x, int y) {
	
		if (x >= 0 && y >= 0 && x < column && y < row)
			return bricks[x, y];

		return null;
	}

	// Flood Fill empty bricks
	public static void FFuncover(int x, int y) {

		// Coordinates in Range?
		if (x >= 0 && y >= 0 && x < column && y < row) { 
		
			// visited already?
			if (visited[x, y])
				return;

			// uncover element
			int mineCount = adjacentMines(x, y);
			bricks[x, y].LoadSprite((BrickType)mineCount);

			// close to a mine? then no more work needed here
			if (mineCount > 0)
				return;

			// set visited flag
			visited[x, y] = true;

			// recursion
			FFuncover(x-1, y);
			FFuncover(x+1, y);
			FFuncover(x, y-1);
			FFuncover(x, y+1);
		}

	}

	public static bool isFinished() {
		// Try to find a covered element that is no mine
		foreach (Brick brick in bricks)
			if (brick.isCovered() && !brick.isMine)
				return false;
		// There are none => all are mines => game won.
		return true;
	}
}
