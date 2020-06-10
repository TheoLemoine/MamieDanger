using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

namespace UI.Animations
{
    public class LevelSelectorTransition : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image background;
        [SerializeField] private LevelRewardDisplay levelRewardDisplay;
        [SerializeField] private float defaultScale = 1f;
        [SerializeField] private float frontScale = 1.5f;
        [SerializeField] private Color beigeColor = new Color(254, 238, 207);
        [SerializeField] private Color purpleColor = new Color(99, 85, 186);
    
        public void UpdateTransition(float inFrontFactor)
        {
            var scale = defaultScale + (frontScale - defaultScale) * inFrontFactor;
            transform.localScale = Vector3.one * scale;
            var clampedFactor = Mathf.Clamp(inFrontFactor - 0.5f, 0f, 0.5f) * 2f;
            var easeFactor = Easing.InOutSine(clampedFactor);
            var bgColor = Color.Lerp(beigeColor, purpleColor, easeFactor);
            var mainColor = Color.Lerp(purpleColor, beigeColor, easeFactor);
            title.color = mainColor;
            levelRewardDisplay.UpdateCoinsColor(mainColor);
            background.color = bgColor;
        }
    }
}
