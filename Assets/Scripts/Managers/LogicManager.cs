using System;

namespace Managers
{
    public class LogicManager
    {
        public static LogicManager instance;
        private Action<bool> onLoadComplete;

        public LogicManager(Action<bool> onLoad)
        {
            if (instance != null)
            {
                return;
            }
            
            instance = this;
            onLoadComplete = onLoad;
            OnLoadSuccess();
        }
        
        public void OnLoadSuccess()
        {
            onLoadComplete?.Invoke(true);
        }
        
        public void OnLoadFail()
        {
            onLoadComplete?.Invoke(false);
        }
    }
}