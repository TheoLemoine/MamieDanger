using UI.Animations;
using UnityEngine;

namespace UI
{
    public class LevelSelectorOrchestrator : MonoBehaviour
    {
        [SerializeField] private LevelButton levelButton;
        [SerializeField] private LevelSelectorTransition levelSelectorTransition;
        [SerializeField] private LevelRewardDisplay levelRewardDisplay;

        public LevelButton LevelButton => levelButton;
    
        public LevelSelectorTransition LevelSelectorTransition => levelSelectorTransition;

        public void LayoutLevelSelector(bool isLocked, int pickedCoinNumber)
        {
            levelRewardDisplay.LayoutPrefabs(pickedCoinNumber);
            levelButton.locked = isLocked;
            if (isLocked) levelButton.UpdateMaterials();
        }
    }
}
