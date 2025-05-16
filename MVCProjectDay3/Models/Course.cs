using System.ComponentModel.DataAnnotations;

namespace MVCProjectDay3.Models
{
    public class Course:BaseEntity
    {
        [MinLength(5), MaxLength(20)]
        public string Title {  get; set; }
        [MinLength(5), MaxLength(50)]
        public string Description { get; set; }
        public int TrainerId {  get; set; }
        public Trainer? Trainer { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl {  get; set; }
    }
}
