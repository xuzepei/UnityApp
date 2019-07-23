using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	public Animator startButtonAnimator;
	public Animator settingsButtonAnimator;
	public Animator settingsDialogAnimator;
	public Animator menuContentAnimator;
	public Animator gearAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		SceneManager.LoadScene("RocketMouse");
	}
		
	public void OpenSettings() {

		if (startButtonAnimator && settingsButtonAnimator && settingsDialogAnimator) {
			startButtonAnimator.SetBool ("isHidden", true);
			settingsButtonAnimator.SetBool ("isHidden", true);
			settingsDialogAnimator.SetBool ("isHidden", false);
		}
	}

	public void CloseSettings() 
	{
		if (startButtonAnimator && settingsButtonAnimator && settingsDialogAnimator) {
			startButtonAnimator.SetBool ("isHidden", false);
			settingsButtonAnimator.SetBool ("isHidden", false);
			settingsDialogAnimator.SetBool ("isHidden", true);
		}
	}

	public void ToggleMenu() 
	{
		if (menuContentAnimator && gearAnimator) {
			bool isHidden = menuContentAnimator.GetBool("isHidden");
			gearAnimator.SetBool ("isHidden", !isHidden);
			menuContentAnimator.SetBool("isHidden", !isHidden);
		}

	}
}
