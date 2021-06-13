
using System.ComponentModel;


namespace WebApi.Application.Enum
{
    public enum Role
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Client")]
        Client = 2
    }
}
