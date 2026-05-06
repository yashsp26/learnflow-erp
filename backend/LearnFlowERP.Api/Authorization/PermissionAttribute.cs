using Microsoft.AspNetCore.Authorization;

namespace LearnFlowERP.Api.Authorization
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string permission)
        {
            Policy = $"Permission:{permission}";
        }
    }
}
