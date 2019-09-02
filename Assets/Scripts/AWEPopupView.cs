using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AWEPopupView : MonoBehaviour {

	public GameObject panel = null;
	public Button closeButton = null;
	public Image maskImage = null;

	private Transform closeButtonTransform = null;
	private Transform maskTransform = null;
	private Transform panelTransform = null;

	void Awake() {
	
		if (closeButton) {
			closeButtonTransform = closeButton.transform;
		} else {
			closeButtonTransform = transform.Find ("CloseButton");
		}

		if (maskImage) {
			maskTransform = maskImage.transform;
		} else {
			maskTransform = transform.Find ("Mask");
		}

		if (panel) {
			panelTransform = panel.transform;
		} else {
			panelTransform = transform.Find ("Panel");
		}
	
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Popup() {

		transform.SetAsLastSibling();

		if (closeButtonTransform) {
			closeButtonTransform.gameObject.SetActive (true);

			closeButton = closeButtonTransform.gameObject.GetComponent<Button> ();
			if (closeButton) {
				closeButton.onClick.AddListener (delegate {
					Close();
				});
			}
		}
			
		if (maskTransform) {
			maskTransform.gameObject.SetActive (true);
			//maskTransform.DOScale(new Vector3(1,1,0), 0.3f);

			maskImage = maskTransform.gameObject.GetComponent<Image> ();
			if (maskImage) {
				maskImage.color = Color.clear;
				maskImage.DOColor (new Color (0.0f, 0.0f, 0.0f, 0.7f), 0.3f);
			}
		}
			

		if (panelTransform) {

			panelTransform.localPosition = new Vector3(0, 0, panelTransform.localPosition.z);
			panelTransform.localScale = new Vector3 (0, 0, 0);
			panelTransform.gameObject.SetActive (true);

			panelTransform.SetAsLastSibling();
			//panelTransform.DOScale(new Vector3(1,1,0), 0.2f).SetEase(Ease.InOutBounce);
			panelTransform.DOScale(new Vector3(1,1,0), 0.3f);
		}
	}


	public void Close() {

		if (closeButtonTransform) {
			closeButtonTransform.gameObject.SetActive (false);
		}

		if (maskTransform) {
			maskTransform.gameObject.SetActive (true);

			maskImage = maskTransform.gameObject.GetComponent<Image> ();
			if (maskImage) {
				maskImage.color = Color.clear;
				maskImage.DOColor (new Color (0.0f, 0.0f, 0.0f, 0.0f), 0.3f).OnComplete (delegate {
					maskTransform.gameObject.SetActive (false);
				});
			}
		}
	

		if (panelTransform) {

			panelTransform.DOScale(new Vector3(0,0,0), 0.3f).OnComplete(delegate {
				GameObject.Destroy(gameObject);
			});
		}
	
	}

	public void ClickedTrialButton() {

		Debug.Log ("------ClickedTrialButton--------");

	}

	public void ClickedWatchAdButton() {

		Debug.Log ("------ClickedWatchAdButton--------");

	}

	public void ClickedRestoreButton() {

		Debug.Log ("------ClickedRestoreButton--------");

	}

	public void ClickedPolicyButton() {

		Debug.Log ("------ClickedPolicyButton--------");
	
	}

	public void ClickedTermsButton() {
		
		Debug.Log ("------ClickedTermsButton--------");
	}
}
