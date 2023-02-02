using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;

/*
 * You can open .txt files by pressing "ctrl + shif + left click" in notepad instead of visual studio
 * you can create new text files by right click in any folder. -> right click -> create/Text File
 * This script file must be located in a folder named "Editor"
 */
class TXTFileOpener : Editor
{
    [InitializeOnLoadMethod]
    static void Initialize()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemOnGUI;
    }
    static string dataPath = Application.dataPath.Replace("Assets", "");
    private static void OnProjectWindowItemOnGUI(string guid, Rect selectionRect)
    {
        Event e = Event.current;
        if (e.control && e.shift && e.type == EventType.MouseDown && e.clickCount == 1 && selectionRect.Contains(e.mousePosition))
        {
            string path = dataPath + AssetDatabase.GUIDToAssetPath(guid);
            if (path.EndsWith(".txt"))
            {
                try
                {
                    var _process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = "/c " + "notepad.exe " + path,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            Verb = "runas",
                            UseShellExecute = true,
                        }
                    };
                    _process.Start();
                    _process.Close();
                }
                catch (System.Exception ex)
                {
                    UnityEngine.Debug.Log(ex);
                }
            }
        }
    }
    [MenuItem("Assets/Create/Text File", priority = 18)]
    public static void CreateTextFile()
    {
        const string NewFileName = "Note";


        // Get the selected path in the Project window
        string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (File.Exists(selectedPath))
        {
            selectedPath = Path.GetDirectoryName(selectedPath);
        }
        // Create a new text file in the selected path
        string newFilePath = Path.Combine(selectedPath, NewFileName);
        int fileNumber = 1;
        while (true)
        {
            if (File.Exists(newFilePath + fileNumber + ".txt")) fileNumber++;
            else
            { 
                newFilePath += fileNumber + ".txt";

                File.WriteAllText(newFilePath, "");

                // Refresh the AssetDatabase
                AssetDatabase.Refresh();
                return;
            }
        }
    }
}