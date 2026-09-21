using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class XcodePostProcessor
{
  [PostProcessBuild(999)]
  public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
  {
    if (target != BuildTarget.iOS)
    {
      return;
    }

    string displayAppName = PlayerSettings.productName;
    string buildVersion = PlayerSettings.bundleVersion;
    string buildNumber = PlayerSettings.iOS.buildNumber;


    // Set Info.plist file
    string plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
    PlistDocument plist = new PlistDocument();
    plist.ReadFromFile(plistPath);
    PlistElementDict rootDict = plist.root;
    rootDict.SetString("LSApplicationCategoryType", "public.app-category.games");
    plist.WriteToFile(plistPath);


    // Setting .xcodeproj file
    string pbxProjectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
    Debug.Log($"Path de proyecto XCode: {pbxProjectPath}");
    PBXProject pbxProject = new PBXProject();
    pbxProject.ReadFromFile(pbxProjectPath);
    string targetGuid = pbxProject.GetUnityMainTargetGuid();
    pbxProject.SetBuildProperty(targetGuid, "MARKETING_VERSION", buildVersion);
    pbxProject.SetBuildProperty(targetGuid, "CURRENT_PROJECT_VERSION", buildNumber);
    pbxProject.SetBuildProperty(targetGuid, "INFOPLIST_KEY_CFBundleDisplayName", displayAppName);
    pbxProject.SetBuildProperty(targetGuid, "INFOPLIST_KEY_LSApplicationCategoryType", "public.app-category.games");
    pbxProject.WriteToFile(pbxProjectPath);


    // Adding entitlements: GameCenter
    string entitlementFileName = "Clasicos Latinoamericanos.entitlements";
    var capabilityManager = new ProjectCapabilityManager(pbxProjectPath, entitlementFileName, null, targetGuid);
    capabilityManager.AddGameCenter();
    capabilityManager.WriteToFile();


    Debug.Log("Success on post-processor for XCode Solution.");
  }
}
