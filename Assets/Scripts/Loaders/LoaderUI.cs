using UnityEngine;
using UnityEngine.UI;

namespace Loaders
{
    public class LoaderUI : MonoBehaviour
    {
        [SerializeField] private Image loaderFG;

        protected int curProgress;
        private int totalProgress;

        public virtual void Init(int total)
        {
            this.totalProgress = total;
            curProgress = 0;
            UpdateUI();
        }

        public virtual void AddProgress(int progress)
        {
            curProgress += progress;
            UpdateUI();
        }
        
        protected void UpdateUI()
        {
            float loadPercent = (float)curProgress / totalProgress;
            loaderFG.fillAmount = loadPercent;
        }
    }
}