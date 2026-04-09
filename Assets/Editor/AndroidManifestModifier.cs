using System.IO;
using System.Xml;
using UnityEditor;
using UnityEditor.Android;
using UnityEngine;

public class AndroidManifestModifier : IPostGenerateGradleAndroidProject
{
    public int callbackOrder { get { return 1; } }

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        string manifestPath = Path.Combine(path, "src/main/AndroidManifest.xml");

        if (!File.Exists(manifestPath))
        {
            Debug.LogWarning("AndroidManifest.xml not found at: " + manifestPath);
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(manifestPath);

        XmlElement root = xmlDoc.DocumentElement;
        string androidNamespaceURI = "http://schemas.android.com/apk/res/android";

        // Check and add READ_EXTERNAL_STORAGE
        AddPermission(xmlDoc, root, androidNamespaceURI, "android.permission.READ_EXTERNAL_STORAGE");

        // Add maxSdkVersion="32" to READ_EXTERNAL_STORAGE as best practice for Android 13+ if MANAGE_EXTERNAL_STORAGE is used
        XmlNode readPerm = root.SelectSingleNode("uses-permission[@android:name='android.permission.READ_EXTERNAL_STORAGE']", GetNamespaceManager(xmlDoc));
        if (readPerm != null)
        {
            XmlAttribute maxSdkAttr = xmlDoc.CreateAttribute("android", "maxSdkVersion", androidNamespaceURI);
            maxSdkAttr.Value = "32";
            readPerm.Attributes.Append(maxSdkAttr);
        }

        // Check and add MANAGE_EXTERNAL_STORAGE
        AddPermission(xmlDoc, root, androidNamespaceURI, "android.permission.MANAGE_EXTERNAL_STORAGE");

        xmlDoc.Save(manifestPath);
        Debug.Log("Successfully updated AndroidManifest.xml with storage permissions.");
    }

    private void AddPermission(XmlDocument xmlDoc, XmlElement root, string namespaceURI, string permissionName)
    {
        XmlNodeList permissions = root.GetElementsByTagName("uses-permission");
        bool hasPermission = false;

        foreach (XmlNode perm in permissions)
        {
            if (perm.Attributes["android:name"] != null && perm.Attributes["android:name"].Value == permissionName)
            {
                hasPermission = true;
                break;
            }
        }

        if (!hasPermission)
        {
            XmlElement newPermission = xmlDoc.CreateElement("uses-permission");
            newPermission.SetAttribute("name", namespaceURI, permissionName);
            root.AppendChild(newPermission);
        }
    }

    private XmlNamespaceManager GetNamespaceManager(XmlDocument xmlDoc)
    {
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
        nsmgr.AddNamespace("android", "http://schemas.android.com/apk/res/android");
        return nsmgr;
    }
}
