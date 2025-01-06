using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUser { get; set; }

        [StringLength(255)]
        [Required]
        public string fullName { get; set; }

        [StringLength(255)]
        [Required]
        public string userName { get; set; }

        [StringLength(255)]
        [Required]
        public string email { get; set; }

        [StringLength(255)]
        [Required]
        public string password { get; set; }

        [StringLength(255)]
        public string phoneNumber { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        public string gender { get; set; }

        public int status { get; set; }

        public int idRole { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<RoomComment> RoomComments { get; set; }

    }
}