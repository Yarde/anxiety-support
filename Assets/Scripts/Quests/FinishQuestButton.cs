using UnityEngine;
using UnityEngine.UI;

namespace Yarde.Quests
{
    [RequireComponent(typeof(Button))]
    public class FinishQuestButton : MonoBehaviour
    {
        public bool IsClicked { get; private set; }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(FinishQuest);
        }

        private void FinishQuest()
        {
            IsClicked = true;
        }
    }
}