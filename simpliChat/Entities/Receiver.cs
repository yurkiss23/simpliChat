using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpliChat.Entities
{
    [Table("SC_Receivers")]
    public class Receiver
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 75)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 50)]
        public string IPAdress { get; set; }
        [Required, StringLength(maximumLength: 256)]
        public string History { get; set; }
    }
}
