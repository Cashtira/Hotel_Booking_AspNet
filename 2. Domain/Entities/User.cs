using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Domain.Entities
{
    public class User : IdentityUser<string>
    {
        [StringLength(100)]
        public string? FullName;
        public DateTime? DateOfBirth { get; set; }

    }
}
