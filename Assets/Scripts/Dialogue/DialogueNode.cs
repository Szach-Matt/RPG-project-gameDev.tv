using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueNode", menuName = "My RPG project/DialogueNode", order = 0)]
    public class DialogueNode : ScriptableObject
    {
        public string text;
        public List<string> children = new List<string>();
        public Rect rect = new Rect(0, 0, 200, 100);
    }
}