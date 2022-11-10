using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TmtsChecker.Models
{
    public class TmHost
    {
        [Key]
        [Required]
        public string Hostname { get; set; }
        
        public string Ip { get; set; }
    }
}
