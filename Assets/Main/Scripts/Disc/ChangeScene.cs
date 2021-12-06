#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

public class ChangeScene : Editor
{
    [MenuItem("Open Scene/Home #1")]
    public static void OpenHome()
    {
        OpenScene(Const.SCENE_HOME);
    }

    [MenuItem("Open Scene/Game #2")]
    public static void OpenGame()
    {
        OpenScene(Const.SCENE_GAME);
    }
    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Main/Scenes/" + sceneName + ".unity");
        }
    }
}
#endif