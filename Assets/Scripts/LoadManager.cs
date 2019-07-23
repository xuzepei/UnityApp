using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;


public class LoadManager : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {

		string path = "AssetBundles/model.ab";
		string url = @"https://color0001.oss-cn-shenzhen.aliyuncs.com/ios/test/model.ab";

		AssetBundle ab = null;

		#region
		//load from local
		//AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync (File.ReadAllBytes (path));
//		AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync (path);
//		yield return request;
//		ab = request.assetBundle;
		#endregion

		//load from server
		UnityWebRequest request = UnityWebRequest.GetAssetBundle(url);
		yield return request.Send();
		ab = DownloadHandlerAssetBundle.GetContent(request);

		if (ab != null) {

			Object temp = ab.LoadAsset("Capsule");
			if (temp) {
				GameObject capsule = Instantiate(temp) as GameObject;
				capsule.gameObject.transform.Translate (new Vector3 (-1, 0, 0));
			}


			Object[] objs = ab.LoadAllAssets<GameObject> ();
			foreach (Object o in objs)
			{
				Instantiate(o);
			}
		}
			
	}


}
