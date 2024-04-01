using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResortBooking.Domain.Entities;

namespace ResortBooking.Web.ViewModels
{
    public class ResortNumberVM
    {
        public ResortNumber? ResortNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? ResortList { get; set; }
    }
}
