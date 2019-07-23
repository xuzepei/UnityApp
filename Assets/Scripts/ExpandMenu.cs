using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpandMenu : MonoBehaviour {

	public Button mainButton;
	public Button settingsButton;
	public Button profileButton;
	public bool expanded = false;
	public bool isAnimating = false;

	// Use this for initialization
	void Start () {

		mainButton.onClick.AddListener (delegate {
			OnClickedButton(0);
		});

		settingsButton.onClick.AddListener (delegate {
			OnClickedButton(1);
		});

		profileButton.onClick.AddListener (delegate {
			OnClickedButton(2);
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClickedButton(int tag) {

		switch (tag) {

		case 0:
			{
				Debug.Log ("Clicked main button.");

				if (expanded) {
					foldMenu ();
				
				} else {
					expandMenu ();
				}
					
				break;
			}
		case 1:
			{
				Debug.Log ("Clicked settigns button.");
				foldMenu ();

				break;
			}
		case 2:
			{
				Debug.Log ("Clicked profile button.");
				foldMenu ();

				break;
			}
		default:
			{
				Debug.Log ("Clicked other button.");
				break;
			}
				
		}
	
	}

	void foldMenu() {

		if (isAnimating)
			return;
		isAnimating = true;

		if (expanded) {
			settingsButton.transform.DOBlendableLocalMoveBy (new Vector3 (55, 12, 0), 0.2f).OnComplete (delegate {
				isAnimating = false;
				expanded = false;
			});

			profileButton.transform.DOBlendableLocalMoveBy (new Vector3 (18, 55, 0), 0.2f).OnComplete (delegate {
				isAnimating = false;
				expanded = false;
			});
		}
	
	}

	void expandMenu() {

		if (isAnimating)
			return;
		isAnimating = true;

		if (expanded == false) {
			settingsButton.transform.DOBlendableLocalMoveBy (new Vector3 (-55, -12, 0), 0.3f).SetEase (Ease.OutElastic).OnComplete (delegate {
				isAnimating = false;
				expanded = true;
			});

			profileButton.transform.DOBlendableLocalMoveBy (new Vector3 (-18, -55, 0), 0.3f).SetEase (Ease.OutElastic).OnComplete (delegate {
				isAnimating = false;
				expanded = true;
			});
		}
		
	}
}
