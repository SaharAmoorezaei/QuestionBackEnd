

using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain.Models
{
    public class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int RoleId { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role{ get; set; }
    }
}
