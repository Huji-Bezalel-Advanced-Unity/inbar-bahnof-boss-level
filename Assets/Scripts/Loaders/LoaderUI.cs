using UnityEngine;
using UnityEngine.UI;

namespace Loaders
{
    public class LoaderUI : MonoBehaviour
    {
        [SerializeField] private Image loaderFG;

        private int curProgress;
        private int totalProgress;

        public void Init(int total)
        {
            this.totalProgress = total;
            curProgress = 0;
            UpdateUI();
        }

        public void AddProgress(int progress)
        {
            curProgress += progress;
            UpdateUI();
        }
        
        private void UpdateUI()
        {
            float loadPrecent = curProgress / totalProgress;
            loaderFG.fillAmount = loadPrecent;
        }
    }
}