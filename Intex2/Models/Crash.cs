using System;
using System.ComponentModel.DataAnnotations;

namespace Intex2.Models
{
    public class Crash
    {
        [Key]
        [Required]
        public int CrashID { get; set; }
    }
}
