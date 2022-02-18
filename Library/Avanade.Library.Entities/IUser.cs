

namespace Avanade.Library.Entities
{
    public interface IUser
    {
        int UserId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Role { get; set; }

    }
}
