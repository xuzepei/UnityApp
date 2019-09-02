using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AWEButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler {

	public Color textNormalColor = Color.clear;
	public Color textHighlightedColor = Color.clear;
	public Color textPressedColor = Color.clear;
	public Color textDisabledColor = Color.clear;

	Text text = null;

	void Awake()
	{
		Debug.Log (">>>>>>>>>>>>>>>>" + GetType() + ":Awake--------");

		//text
		if (text == null) {

			Transform trans = transform.Find ("Text");
			if (trans) {
				text = trans.gameObject.GetComponent<Text> ();
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		Debug.Log(">>>>>>>>>>>>>>>>" + GetType() + ":OnPointerDown------------");
		if (text && textPressedColor != Color.clear)
			text.color = textPressedColor;
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		Debug.Log(">>>>>>>>>>>>>>>>" + GetType() + ":OnPointerExit------------");
		if (text && textNormalColor != Color.clear)
			text.color = textNormalColor;
	}
}
