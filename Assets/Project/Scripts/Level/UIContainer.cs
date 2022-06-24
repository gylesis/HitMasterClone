using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Level
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _backgroundImage; 
        
        public void Show(string str)
        {
            _backgroundImage.enabled = true;
            _text.enabled = true;
            _text.text = str;
        }

        public void Hide()
        {
            _backgroundImage.enabled = false;
            _text.enabled = false;
        }
        
    }
}