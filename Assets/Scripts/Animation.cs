using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animation : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//1
		//transform.DOBlendableMoveBy (new Vector3 (0, 2, 0), 2).SetEase (Ease.OutElastic).SetLoops(-1, LoopType.Yoyo);

		//2
		//transform.DOLocalMove(new Vector3(20, 0, 0), 2);
		//transform.DOLocalMoveX(20,2).SetEase(Ease.OutElastic);

		//3
		//transform.DOLocalRotate(new Vector3(0, 0, -180.0f), 0.3f).SetLoops(-1, LoopType.Restart);

		//4
		//transform.DOScale(new Vector3(2,2,0), 2.0f).SetLoops(-1,LoopType.Yoyo);

		//5
		transform.DOPunchPosition(new Vector3(0, 10, 0), 2, 2, 0.1f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
