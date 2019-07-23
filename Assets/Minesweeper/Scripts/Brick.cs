using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrickType {
	Default = -1,
	Empty,
	One,
	Two,
	Three,
	Four,
	Five,
	Six,
	Seven,
	Eight
};


public class Brick : MonoBehaviour {


	// Is this a mine?
	public bool isMine = false;
	public bool isClicked = false;

	// Different Textures
	public Sprite[] emptySprites;
	public Sprite mineSprite;
	private BrickType type = BrickType.Default;
	public int x = -1;
	public int y = -1;

	// Use this for initialization
	void Start () {

		/*
		 * Random.value will always return a new random number between 0 and 1. 
		 * We want a 15% probability that an element is a mine, so we use
		 */
		isMine = Random.value < 0.15;
	}

	// Update is called once per frame
	void Update () {

		if (isMine && isClicked) {
			this.GetComponent<SpriteRenderer> ().sprite = mineSprite;
		}
		
	}

	public void LoadSprite(BrickType type) {

		this.type = type;
		if(this.type != BrickType.Default)
			this.GetComponent<SpriteRenderer> ().sprite = emptySprites [(int)type];

	}

	public bool isCovered() {
		return (this.type == BrickType.Default);
	}

	void OnMouseUpAsButton() {

		isClicked = true;

		// It's a mine
		if (isMine) {
			// TODO: do stuff..
			Debug.LogError("Oops, you lose!");
		} else {

			//Calculate mine number
			int count = GameBoard.adjacentMines(x,y);
			Debug.Log ("Mines' count is " + count);

			// TODO: show adjacent mine number
			LoadSprite ((BrickType)count);

			// TODO: uncover area without mines
			// uncover area without mines
			GameBoard.FFuncover(x, y);

			// TODO: find out if the game was won now
			if (GameBoard.isFinished())
				Debug.Log ("you win");
		}
	}
		
}
