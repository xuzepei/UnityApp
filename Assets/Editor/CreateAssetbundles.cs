using UnityEditor;
using System.IO;

public class CreateAssetbundles {

	[MenuItem("AssetBundle/Build AssetBundles")]
	static void BuildAllAssetBundles() 
	{
		string dir = "AssetBundles";

		//check if the directory is existing
		if (Directory.Exists(dir) == false) {
			Directory.CreateDirectory (dir);
		}

//		// Call plugin only when running on real device
//		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
//		{
//			#if UNITY_ANDROID
//			#elif UNITY_IOS
//			#endif
//		}

		//build
		#if UNITY_IOS
			BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
		#elif UNITY_ANDROID
			BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
		#else
			BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
		#endif
	}
}
