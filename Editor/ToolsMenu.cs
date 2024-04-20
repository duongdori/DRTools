#if UNITY_EDITOR
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace DR
{
    public class ToolsMenu : EditorWindow
    {
        private static string _projectName = "PROJECT_NAME";
        private static string _newSceneName = "SCENE_NAME";
        
        [MenuItem("Tools/DR/Create Default Folders")]
        private static void SetUpFolders()
        {
            ToolsMenu window = CreateInstance<ToolsMenu>();
            window.position = new Rect(Screen.width/2, Screen.height/2, 400, 150);
            window.ShowPopup();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Insert the Project name used as the root folder");
            _projectName = EditorGUILayout.TextField("Project Name: ", _projectName);
            
            GUILayout.Space(10);
            _newSceneName = EditorGUILayout.TextField("Scene Name: ", _newSceneName);
            
            this.Repaint();
            
            GUILayout.Space(10);
            if (GUILayout.Button("Generate!")) {
                CreateAllFolders();
                CreateAndSaveNewScene();
                this.Close();
            }
            
            GUILayout.Space(10);
            if (GUILayout.Button("Close")) {
                this.Close(); 
            }
        }
        
        private static void CreateAllFolders()
        {
            string[] folders = {
                "Art",
                "Audio",
                "Animations",
                "Prefabs",
                "Scripts",
                "Scenes",
                "Shaders",
                "UI"
            };
            
            foreach (string folder in folders)
            {
                if (!Exists(Combine(dataPath, _projectName, folder)))
                {
                    CreateDirectory(Combine(dataPath, _projectName, folder));
                }
            }
            
            string[] artFolders = {
                "Sprites",
                "Models",
                "Materials",
                "Textures",
            };
            
            foreach (string subfolder in artFolders)
            {
                if (!Exists(Combine(dataPath, _projectName, "Art", subfolder)))
                {
                    CreateDirectory(Combine(dataPath, _projectName, "Art", subfolder));
                }
            }
            
            string[] audioFolders = {
                "Music",
                "SFX",
            };
            
            foreach (string subfolder in audioFolders)
            {
                if (!Exists(Combine(dataPath, _projectName, "Audio", subfolder)))
                {
                    CreateDirectory(Combine(dataPath, _projectName, "Audio", subfolder));
                }
            }
            
            string[] uiFolders = {
                "Assets",
                "Fonts",
                "Icon"
            };
            
            foreach (string subfolder in uiFolders)
            {
                if (!Exists(Combine(dataPath, _projectName, "UI", subfolder)))
                {
                    CreateDirectory(Combine(dataPath, _projectName, "UI", subfolder));
                }
            }
            
            Refresh();
        }
        
        private static void CreateAndSaveNewScene()
        {
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
    
            string scenePath = $"Assets/{_projectName}/Scenes/{_newSceneName}.unity";
            
            if (!Exists(GetDirectoryName(scenePath)))
            {
                CreateDirectory(GetDirectoryName(scenePath));
            }
    
            EditorSceneManager.SaveScene(newScene, scenePath);
            
            string oldScenesFolderPath = "Assets/Scenes";
            if (IsValidFolder(oldScenesFolderPath))
            {
                DeleteAsset(oldScenesFolderPath);
                Refresh();
            }
    
            EditorUtility.DisplayDialog("Project Created", $"New '{_projectName}' has been created!", "OK");
        }

    }
}
#endif
