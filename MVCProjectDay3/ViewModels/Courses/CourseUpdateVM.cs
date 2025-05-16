using MVCProjectDay3.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCProjectDay3.ViewModels.Courses
{
    public class CourseUpdateVM
    {
        public int Id { get; set; }
        [MinLength(5), MaxLength(20)]
        public string Title { get; set; }
        [MinLength(5), MaxLength(50)]
        public string Description { get; set; }
        public int TrainerId { get; set; }
       
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
