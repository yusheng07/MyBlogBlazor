using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HashWorkerBlazor.Models
{
    public class ListItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Account { get; set; }

        public string HashListJson { get; set; }

        public int HashCount { get; set; }

        public string CheckHash { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? LastSendTime { get; set; } 

        public string Base64Img { get; set; }

    }
}
