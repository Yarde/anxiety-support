using System.Collections.Generic;
using UnityEngine;

namespace Yarde.Quests
{
    [CreateAssetMenu(fileName = "Questline", menuName = "Quests/Questline", order = 0)]
    public class Questline : ScriptableObject
    {
        [SerializeField] private List<Quest> _quests;
        
        public bool IsLastQuest(int index)
        {
            return index == _quests.Count - 1;
        }
        
        public string GetQuest(int index)
        {
            return _quests[index].name;
        }
    }
}