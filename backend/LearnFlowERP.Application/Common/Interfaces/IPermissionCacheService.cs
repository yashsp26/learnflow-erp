namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface IPermissionCacheService
    {
        Task<List<string>> GetPermissionsAsync(long roleId);

        void RemoveRolePermissions(long roleId);
    }
}