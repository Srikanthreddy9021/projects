using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBooking.Domain.Entities
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }        
        
        public string Name { get; set; }

        public string? Description { get; set; }

        [ForeignKey("Resort")]
        public int ResortId { get; set; }
        [ValidateNever]
        public Resort? Resort { get; set; }

      
        
    }
}
