using System.Threading;

namespace TurnTable.ExternalServices.MutualExclusion {

    public class NameSearchMutualExclusionService : INameSearchMutualExclusionService {
        private static Mutex _key;
        private bool locked;

        public NameSearchMutualExclusionService()
        {
            _key = new Mutex();
        }

        public bool IsLocked()
        {
            return locked;
        }

        public void Lock()
        {
            var waitOne = _key.WaitOne();
            locked = true;
        }

        public void UnLock()
        {
            _key.ReleaseMutex();
            locked = false;
        }
    }
}