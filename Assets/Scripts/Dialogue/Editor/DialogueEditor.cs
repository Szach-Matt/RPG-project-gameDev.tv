using UnityEditor;
using UnityEditor.Callbacks;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "DialogueEditor");
        }

        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instaceID, int line)
        {

            var obj = EditorUtility.InstanceIDToObject(instaceID) as Dialogue;
            if (obj != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }
    }
}