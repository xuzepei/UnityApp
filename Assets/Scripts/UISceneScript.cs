using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneScript : MonoBehaviour {

	private GameObject bombPopViewPrefab = null;
	public GameObject canvas = null;

	// Use this for initialization
	void Start () {
		bombPopViewPrefab = (GameObject)Resources.Load("Prefabs/BombPopupView");

		AWEAudioManager.Instacne.PlayMusicByName ("bg_0");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenPopupView() {

		if (bombPopViewPrefab) {
			GameObject obj = Instantiate(bombPopViewPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			if (obj) {

				obj.transform.parent = canvas.transform;

				//重置组件的位置和大小，因为动态加到Canvas上以为，原有的位置和大小会随着Canvas变化
				obj.GetComponent<RectTransform> ().sizeDelta = Vector3.zero; //根据拉伸关系，设置UI组件的大小

				obj.transform.localPosition = Vector3.zero;
				obj.transform.localScale = Vector3.one;

				AWEPopupView popupView = obj.gameObject.GetComponent<AWEPopupView> ();
				if (popupView) {

					AWEAudioManager.Instacne.PlaySoundByName ("click");
					popupView.Popup ();
				}
			}
		}
		
	}
}
