namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface IPermissionCacheService
    {
        Task<List<string>> GetPermissionsAsync(long userId, CancellationToken cancellationToken = default);

        void RemoveUserPermissions(long userId);
        

        // To remove permissions immeadiately after someone revokes them because they may be in cache
        Task RemoveDesignationUsersPermissionsAsync(long designationId);

        Task RemoveRoleUsersPermissionsAsync(long roleId);
    }
}