using System.ComponentModel.DataAnnotations;

namespace MVCProjectDay3.ViewModels.Trainers
{
    public class TrainerCreateVM
    {
        [MinLength(5),MaxLength(20)]
        public string Name { get; set; }
    }
}
