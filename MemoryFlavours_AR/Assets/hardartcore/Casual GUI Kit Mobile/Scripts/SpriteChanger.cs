//This asset was uploaded by https://unityassetcollection.com

using UnityEngine;
using UnityEngine.UI;

namespace hardartcore.CasualGUI
{
    public class SpriteChanger : MonoBehaviour
    {
        public Slider slider;
        public Sprite enabledSprite;
        public Sprite disabledSprite;

        private Image _image;

        public void Awake()
        {
            _image = GetComponent<Image>();
            slider.wholeNumbers = true;
        }

        private void Start()
        {
            // Init based on Slider's value
            ChangeSprite();
        }

        public void ChangeSprite()
        {
            if (slider.value == slider.minValue)
            {
                _image.sprite = disabledSprite;
            }
            else
            {
                _image.sprite = enabledSprite;
            }
        }

    }
}
