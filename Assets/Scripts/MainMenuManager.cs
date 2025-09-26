namespace snow
{
    using UnityEngine;
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        public void OpenSettings()
        {
            _anim.Play("SettingsMenuOn");
        }
        public void OpenExit()
        {
            _anim.Play("ExitMenuOn");
        }
        public void OpenJoin()
        {

        }
        public void OpenStart()
        {

        }
        public void CloseExit()
        {
            _anim.Play("ExitMenuOff");
        }
        public void ExecuteExit()
        {

        }
        public void SelectSettingsCategory(string category)
        {

        }
        public void CloseSettings()
        {

        }
        public void SaveSettings()
        {

        }
        public void DiscardSettings()
        {

        }
    }
}