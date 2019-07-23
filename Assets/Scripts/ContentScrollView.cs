using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ContentScrollView : MonoBehaviour {

	public GameObject contentNode;
	public ScrollViewHeader header;
	public ScrollViewFooter footer;
	public List<GameObject> list;
	private int orderIndex = 0;

	// Use this for initialization
	void Start () {

//		GameObject[] objects = GameObject.FindGameObjectsWithTag("scrollview_cell");//根据tag查找物体，但是必须是激活的
//		foreach(GameObject go in objects) {
//			ScrollViewCell cell = (ScrollViewCell)go.GetComponent (typeof(ScrollViewCell));
//			cell.test ();
//		}

		header.transform.SetSiblingIndex (orderIndex);
		orderIndex++;

		GameObject go = Resources.Load ("Prefabs/Cell") as GameObject;
		if (go) {

			for (int i = 0; i < 20; i++) {
				GameObject cell = Instantiate (go);
				if (cell) {
					list.Add (cell);

					cell.transform.SetParent (contentNode.transform, false); //If param2 is false, perphaps change scale
					cell.transform.SetSiblingIndex (orderIndex);
					//cell.transform.localScale = new Vector3 (1, 1, 1);
					orderIndex++;

					Button button = (Button)cell.GetComponent<Button>();
					if (button) {
						button.onClick.AddListener (delegate {

							Debug.Log("---------->i: " + i);
							OnClickedCell(cell);
						});
					}
					
					ScrollViewCell cellScript = (ScrollViewCell)cell.GetComponent<ScrollViewCell>();
					if (cellScript)
						cellScript.test ();
					
				}
			}
		}

		footer.transform.SetSiblingIndex (orderIndex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClickedCell(GameObject cell) {

		Debug.Log ("Clicked button with index: " + (cell.transform.GetSiblingIndex() - 1));

		RectTransform rt = cell.GetComponent<RectTransform>();
		ScrollViewCell cellScript = (ScrollViewCell)cell.GetComponent<ScrollViewCell>();
		if (cellScript) {

			if (cellScript.isAnimating) {
				return;
			}

			cellScript.isAnimating = true;

			if (cellScript.isExpanded) {

				//rt.sizeDelta = new Vector2 (rt.sizeDelta.x, rt.sizeDelta.y - 100);
				var toValue = new Vector2 (rt.sizeDelta.x, rt.sizeDelta.y - 100);
				DOTween.To (() => rt.sizeDelta, size => rt.sizeDelta = size, toValue, 0.3f).OnComplete(delegate {
					cellScript.isExpanded = false;
					cellScript.isAnimating = false;
				});

			} else {

				//rt.sizeDelta = new Vector2 (rt.sizeDelta.x, rt.sizeDelta.y + 100);
				var toValue = new Vector2 (rt.sizeDelta.x, rt.sizeDelta.y + 100);
				DOTween.To (() => rt.sizeDelta, size => rt.sizeDelta = size, toValue, 0.3f).OnComplete(delegate {
					cellScript.isExpanded = true;
					cellScript.isAnimating = false;
				});;

			}

		}
			
			
//		cell.transform.DoRec (new Vector3 (1, 2, 1), 0.3f).OnComplete(delegate {
//
//			//LayoutRebuilder.ForceRebuildLayoutImmediate(this.contentNode.GetComponent<RectTransform>());
//			
//		});
	
	}
}
