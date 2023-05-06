using UnityEngine;
using UnityEngine.UI;

namespace Yarde.Utils.Buttons
{
    [RequireComponent(typeof(Button))]
    public class QuitButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Application.Quit);
        }
    }
}