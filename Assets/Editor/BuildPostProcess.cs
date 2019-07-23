/**
 * creatge by zhangke 2017.5.10
 * 发布后自动替换ios和android项目中对应的文件夹
 * (ios项目替换Data和Native文件夹，Android Studio项目替换assets文件夹)
 */
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Xml;
using System.IO;
using UnityEditor.iOS.Xcode;

[InitializeOnLoad]
public static class BuildPostProcess
{
	static BuildPostProcess() {
		EditorUserBuildSettings.activeBuildTargetChanged += OnChangePlatform;
	}

	public static void DeleteFolder(string dir)
	{
		DirectoryInfo dirs = new DirectoryInfo(dir);
		DeleteFolder (dirs);
	}

	public static void DeleteFolder(DirectoryInfo dirs)
	{
		if(dirs==null||(!dirs.Exists))
		{
			return ;
		}

		DirectoryInfo[] subDir=dirs.GetDirectories();
		if(subDir!=null)
		{
			for(int i=0;i<subDir.Length;i++)
			{
				if(subDir[i]!=null)
				{
					DeleteFolder(subDir[i]);
				}
			}
			subDir=null;
		}

		FileInfo[] files=dirs.GetFiles();
		if(files!=null)
		{
			for(int i=0;i<files.Length;i++)
			{
				if(files[i]!=null)
				{
					files[i].Delete();
					files[i]=null;
				}
			}
			files=null;
		}

		dirs.Delete();
	}


	static void OnChangePlatform() {
		if(EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
		{
			EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
			#if UNITY_5_6_OR_NEWER
			EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle; 
			#endif
		}
	}

	[PostProcessBuild (100)]
	public static void OnPostProcessBuild (BuildTarget target, string pathToBuiltProject)
	{
		if (target == BuildTarget.Android)
		{
			string buildOutPutPath = pathToBuiltProject+"/"+Application.productName+"/assets";
			if(!Directory.Exists(buildOutPutPath)){
				buildOutPutPath = pathToBuiltProject+"/"+Application.productName+"/src/main/assets";
			}
			string androidStudioProjectPath = Application.dataPath + "/../proj.android-studio/app/src/main/assets";
			if(Directory.Exists(buildOutPutPath) && Directory.Exists(androidStudioProjectPath)){
				DeleteFolder (androidStudioProjectPath);
				Directory.Move (buildOutPutPath, androidStudioProjectPath);
				Debug.Log ("BuildPostProcess>>>>>>>>>>Replaced 'assets'");
			}

			string buildOutArmeabiPath = pathToBuiltProject+"/"+Application.productName+"/libs/armeabi-v7a";
			if(!Directory.Exists(buildOutArmeabiPath)){
				buildOutArmeabiPath = pathToBuiltProject+"/"+Application.productName+"/src/main/jniLibs/armeabi-v7a";
			}
			string androidStuidoArmeabiPath = Application.dataPath + "/../proj.android-studio/app/src/main/jniLibs/armeabi-v7a";
			if(Directory.Exists(buildOutArmeabiPath) && Directory.Exists(androidStuidoArmeabiPath)){
				DeleteFolder (androidStuidoArmeabiPath);
				Directory.Move (buildOutArmeabiPath, androidStuidoArmeabiPath);
				Debug.Log ("BuildPostProcess>>>>>>>>>>Replaced 'armeabi-v7a'");
			}

			string buildOutX86Path = pathToBuiltProject+"/"+Application.productName+"/libs/x86";
			if(!Directory.Exists(buildOutX86Path)){
				buildOutX86Path = pathToBuiltProject+"/"+Application.productName+"/src/main/jniLibs/x86";
			}
			string androidStuidoX86Path = Application.dataPath + "/../proj.android-studio/app/src/main/jniLibs/x86";
			if(Directory.Exists(buildOutX86Path) && Directory.Exists(androidStuidoX86Path)){
				DeleteFolder (androidStuidoX86Path);
				Directory.Move (buildOutX86Path, androidStuidoX86Path);
				Debug.Log ("BuildPostProcess>>>>>>>>>>Replaced 'x86'");
			}

			string buildOutJarPath = pathToBuiltProject+"/"+Application.productName+"/libs/unity-classes.jar";
			string androidStuidoJarPath = Application.dataPath + "/../proj.android-studio/app/libs/unity-classes.jar";
			if(File.Exists(buildOutJarPath) && File.Exists(androidStuidoJarPath)){
				File.Delete(androidStuidoJarPath);
				File.Move (buildOutJarPath, androidStuidoJarPath);
				Debug.Log ("BuildPostProcess>>>>>>>>>>Replaced 'unity-classes.jar'");
			}
		} 
		else if(target == BuildTarget.iOS)
		{
			UpdateProject(target, pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj");

			string buildOutPutPath1 = pathToBuiltProject+"/Data";
			string iOSProjectPath1 = Application.dataPath + "/../proj.ios_mac/Data";
			if(Directory.Exists(iOSProjectPath1)){
				DeleteFolder (iOSProjectPath1);
				Directory.Move (buildOutPutPath1, iOSProjectPath1);
				Debug.Log ("BuildPostProcess>>>>>>>>>>Replaced 'Data'");
			}

			string buildOutPutPath2 = pathToBuiltProject+"/Classes/Native";
			string iOSProjectPath2 = Application.dataPath + "/../proj.ios_mac/Classes/Native";
			if(Directory.Exists(iOSProjectPath2)){
				DeleteFolder (iOSProjectPath2);
				Directory.Move (buildOutPutPath2, iOSProjectPath2);
				Debug.Log ("BuildPostProcess>>>>>>>>>>Replaced 'Native'");
			}

			string buildOutPutPath3 = pathToBuiltProject+"/Libraries/Plugins";
			string iOSProjectPath3 = Application.dataPath + "/../proj.ios_mac/Libraries/Plugins";
			if(Directory.Exists(iOSProjectPath3) && Directory.Exists(buildOutPutPath3)){
				DeleteFolder (iOSProjectPath3);
				Directory.Move (buildOutPutPath3, iOSProjectPath3);
				Debug.Log ("BuildPostProcess>>>>>>>>>>Replaced 'Plugins'");
			}

			string buildOutPutPath4 = pathToBuiltProject+"/Libraries/libil2cpp";
			string iOSProjectPath4 = Application.dataPath + "/../proj.ios_mac/Libraries/libil2cpp";
			if(Directory.Exists(iOSProjectPath4) && Directory.Exists(buildOutPutPath4)){
				DeleteFolder (iOSProjectPath4);
				Directory.Move (buildOutPutPath4, iOSProjectPath4);
				Debug.Log ("BuildPostProcess>>>>>>>>>>Replaced 'libil2cpp'");
			}
		}
		Debug.Log("BuildPostProcess>>>>>>>>>>=========================build complete===============================");
	}


	private static void UpdateProject(BuildTarget buildTarget, string projectPath) {
		PBXProject project = new PBXProject();
		project.ReadFromString(File.ReadAllText(projectPath));

		string targetId = project.TargetGuidByName(PBXProject.GetUnityTargetName());

		// Required Frameworks
		project.AddFrameworkToProject(targetId, "CoreTelephony.framework", false);
		project.AddFrameworkToProject(targetId, "EventKit.framework", false);
		project.AddFrameworkToProject(targetId, "EventKitUI.framework", false);
		project.AddFrameworkToProject(targetId, "MediaPlayer.framework", false);
		project.AddFrameworkToProject(targetId, "MessageUI.framework", false);
		project.AddFrameworkToProject(targetId, "QuartzCore.framework", false);
		project.AddFrameworkToProject(targetId, "SystemConfiguration.framework", false);
		project.AddFrameworkToProject(targetId, "Security.framework", false);
		project.AddFrameworkToProject(targetId, "MobileCoreServices.framework", false);
		project.AddFrameworkToProject(targetId, "PassKit.framework", false);
		project.AddFrameworkToProject(targetId, "Social.framework", false);
		project.AddFrameworkToProject(targetId, "CoreData.framework", false);
		project.AddFrameworkToProject(targetId, "AdSupport.framework", false);
		project.AddFrameworkToProject(targetId, "StoreKit.framework", false);
		if(project.HasFramework("iAd.framework")){
			project.RemoveFrameworkFromProject(targetId,"iAd.framework");
		}

		project.AddFileToBuild(targetId, project.AddFile("usr/lib/libz.1.2.5.dylib", "Frameworks/libz.1.2.5.dylib", PBXSourceTree.Sdk));
		project.AddFileToBuild(targetId, project.AddFile("usr/lib/libz.dylib", "Frameworks/libz.dylib", PBXSourceTree.Sdk));
		project.AddFileToBuild(targetId, project.AddFile("usr/lib/libsqlite3.dylib", "Frameworks/libsqlite3.dylib", PBXSourceTree.Sdk));
		project.AddFileToBuild(targetId, project.AddFile("usr/lib/libsqlite3.0.dylib", "Frameworks/libsqlite3.0.dylib", PBXSourceTree.Sdk));
		project.AddFileToBuild(targetId, project.AddFile("usr/lib/libxml2.dylib", "Frameworks/libxml2.dylib", PBXSourceTree.Sdk));

		// Optional Frameworks
		project.AddFrameworkToProject(targetId, "Webkit.framework", true);
		project.AddFrameworkToProject(targetId, "JavaScriptCore.framework", true);
		project.AddFrameworkToProject(targetId, "WatchConnectivity.framework", true);

		// For 3.0 MP classes
		project.AddBuildProperty(targetId, "OTHER_LDFLAGS", "-ObjC");
		project.AddBuildProperty(targetId, "OTHER_LDFLAGS", "-lxml2");
		project.SetBuildProperty(targetId, "ENABLE_BITCODE", "NO");

		File.WriteAllText(projectPath, project.WriteToString());
	}
}