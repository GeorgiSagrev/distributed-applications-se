using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyApplication.ViewModels.Workers
{
    public class AddVM
    {

        [DisplayName("Име: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public string LastName { get; set; }

        [DisplayName("Е-маил: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public string EmailAddress { get; set; }

        [DisplayName("Заплата: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public double Salary { get; set; }

        [DisplayName("Пълно време: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public bool IsFullTime { get; set; }

        [Display(Name = "Регистриран на")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public DateTime RegisteredOn { get; set; }
    }
}
