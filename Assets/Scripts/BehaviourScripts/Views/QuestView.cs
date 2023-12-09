using System.Collections;
using System.Collections.Generic;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class QuestView : CustomUI
    {
        [SerializeField] private Slider questProgressSlider;
        [SerializeField] private Image starImage;
        [SerializeField] private QuestItemView[] questItemViews;
        
        private Dictionary<string, QuestItemView> _questItemDictionary;

        protected override void OnEnable()
        {
            base.OnEnable();
            _questItemDictionary = new Dictionary<string, QuestItemView>();
            foreach (var quest in questItemViews) 
                _questItemDictionary.Add(quest.Key, quest);
        }
        
        public void SetupQuest(string id)
        {
            foreach (var questItem in questItemViews) 
                questItem.SetActive(false);

            _questItemDictionary[id].SetActive(true);
            
            starImage.gameObject.SetActive(false);
            questProgressSlider.value = 0;
        }
        
        public void CompleteQuest() => StartCoroutine(SmoothFillSlider(1f));

        public IEnumerator SmoothFillSlider(float duration)
        {
            float elapsedTime = 0;
            float startValue = questProgressSlider.value;

            while (elapsedTime < duration)
            {
                questProgressSlider.value = Mathf.Lerp(startValue, 1, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            starImage.gameObject.SetActive(true);
            questProgressSlider.value = 1;
        }
    }
}