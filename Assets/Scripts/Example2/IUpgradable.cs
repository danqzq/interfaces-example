public interface IUpgradable
{
    UpgradableInfo GetInfo();
    
    void OnUpgrade();
}