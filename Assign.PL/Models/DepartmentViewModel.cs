using System.ComponentModel.DataAnnotations;

namespace Assign.PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Code is Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Department Name is Required")]
        [MinLength(3, ErrorMessage = "MinLeangth is 3 Charachters")]
        public string Name { get; set; }
    }
}
