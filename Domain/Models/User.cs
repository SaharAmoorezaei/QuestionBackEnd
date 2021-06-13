

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Domain.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }        
        public string FullName { get; set; }        
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }


    }
}
