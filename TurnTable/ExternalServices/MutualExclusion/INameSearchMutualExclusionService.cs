namespace TurnTable.ExternalServices.MutualExclusion {
    public interface INameSearchMutualExclusionService {
        bool IsLocked();
        void Lock();
        void UnLock();
    }
}