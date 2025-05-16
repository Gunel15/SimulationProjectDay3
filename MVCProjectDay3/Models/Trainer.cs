using System.ComponentModel.DataAnnotations;

namespace MVCProjectDay3.Models
{
    public class Trainer:BaseEntity
    {
        [MinLength(5), MaxLength(20)]
        public string Name { get; set; }
        public IEnumerable<Trainer> Trainers { get; set; }
    }
}
