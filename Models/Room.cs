using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idRoom { get; set; }

        [StringLength(255)]
        [Required]
        public string name { get; set; }

        [StringLength(255)]
        [Required]
        public string image { get; set; }

        public string description { get; set; }

        public int discount { get; set; }

        public int cost { get; set; }

        public int view { get; set; }

        public int numberChildren { get; set; }

        public int numberAdult { get; set; }

        public int idType { get; set; }

        public virtual Type Type { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<RoomComment> RoomComments { get; set; }


    }
}