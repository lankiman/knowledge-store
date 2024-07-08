using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_learning.Enums
{
    public enum LessonCategory
    {
        Chemistry,
        Physics,
        Mathematics,

        [Display(Name = "Pharmaceutical Chemistry")]
        PharmaceuticalChemistry,
        Biochemistry,
        [Display(Name = "Human Anatomy")] HumanAnatomy,
        [Display(Name = "Human Physiology")] HumanPhysiology
    }
}