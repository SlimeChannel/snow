namespace snow.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform SettingsContainer;
        public void TabChange(string animationName)
        {
            _anim.Play(animationName);
            EventSystem.current.SetSelectedGameObject(null);
        }
        public void ExecuteExit()
        {

        }
        public void SelectSettingsCategory(string category)
        {
            for (int i = 0; i < SettingsContainer.childCount; i++)
            {
                Debug.Log(i);
                GameObject child = SettingsContainer.GetChild(i).gameObject;
                child.SetActive(tag == child.GetComponent<SettingsTab>().Tag);
            }
        }
        public void SaveSettings()
        {

        }
        public void DiscardSettings()
        {

        }
    }
}