using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBooking.Domain.Entities
{
    public class ResortNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Room Number")]
        public int Resort_Number { get; set; }
        [ForeignKey("Resort")]
        [Display(Name = "Resort Name")]
        public int ResortId { get; set; }
        [ValidateNever]
        public Resort Resort { get; set; }
        public string? SpecialDetails { get; set; }

        
        
    }
}
