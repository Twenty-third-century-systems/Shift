namespace TurnTable.ExternalServices {
    public interface INameSearchMutualExclusionService {
        bool IsLocked();
        void Lock();
        void UnLock();
    }
}