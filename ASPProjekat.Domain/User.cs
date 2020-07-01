using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ASPProjekat.Domain
{
    public class User : Entity
    {
        //[Required]
        public string Username { get; set; }
       // [Required]
        public string FirstName { get; set; }
       // [Required]
        public string LastName { get; set; }
        //[Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<UserUseCase> UserUseCases { get; set; }
    }
    //(GETDATE(),null,0,null,1,'user2','Viktorija','Koncar','user2','user2@gmail.com')
}
