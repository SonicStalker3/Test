using UnityEngine;

namespace Scriptable.Dialog
{
    ///Data Message for Data Dialog
    [CreateAssetMenu(menuName = "Dialog/Dialog Message")]
    public class DialogText : ScriptableObject
    {
        public byte who_id = 0;
        public string text = "test_text";
    }
}
