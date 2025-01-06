using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idService { get; set; }

        [StringLength(255)]
        [Required]
        public string name { get; set; }

        public int cost { get; set; }

        public virtual ICollection<BookingService> BookingServices { get; set; }
    }
}