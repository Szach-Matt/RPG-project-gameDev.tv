using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueNode", menuName = "My RPG project/DialogueNode", order = 0)]
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] string text;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(0, 0, 200, 100);

        public Rect GetRect()
        {
            return rect;
        }

        public string GetText()
        {
            return text;
        }

        public List<string> GetChildren()
        {
            return children;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Update Dialogue Node position");
            rect.position = newPosition;
        }

        public void SetText(string newText)
        {
            if (text != newText)
            {
                Undo.RecordObject(this, "Changed Dialogue text");
                text = newText;
            }
        }

        public void AddChild(string child)
        {
            Undo.RecordObject(this, "Added child Dialogue Node");
            children.Add(child);
        }

        public void RemoveChild(string child)
        {
            Undo.RecordObject(this, "Deleted child Dialogue Node");
            children.Remove(child);
        }
#endif
    }
}