namespace DALs.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class ShortUrl
    {
        [Key]
        [ForeignKey("Url")]
        public int Id { get; set; }
        public string ShortUrlPath { get; set; }
        public Url Url { get; set; }
    }
}
