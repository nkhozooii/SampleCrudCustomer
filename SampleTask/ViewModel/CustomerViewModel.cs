using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SampleTask.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }        
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        [Required]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> City { get; set; } = default!;
    }
}
