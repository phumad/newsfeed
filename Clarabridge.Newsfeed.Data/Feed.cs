using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarabridge.Newsfeed.Data
{
    public class Feed
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Body { get; set; }
    }
}
