using UnityEngine;

namespace Scriptable.Dialog
{
    ///Data Message for Data Dialog
    [CreateAssetMenu(menuName = "Dialog/Dialog Message")]
    public class DialogText : ScriptableObject
    {
        public string who = "test_npc";
        public string text = "test_text";
    }
}
