using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;     

namespace QuanLyKhachSan.Models
{
    public class RoomComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idRoomComment { get; set; }

        public int idRoom { get; set; }

        public string text { get; set; }

        public int idUser { get; set; }

        public int star { get; set; }

        public DateTime createdDate { get; set; }

        public virtual Room Room { get; set; }

        public virtual User User { get; set; }
    }
}