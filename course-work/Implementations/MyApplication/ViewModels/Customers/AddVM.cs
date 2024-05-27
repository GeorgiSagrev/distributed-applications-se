using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyApplication.ViewModels.Customers
{
    public class AddVM
    {
        [DisplayName("Име: ")]
        [Required(ErrorMessage = "Това поле е задължитено!")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия: ")]
        [Required(ErrorMessage = "Това поле е задължитено!")]
        public string LastName { get; set; }

        [DisplayName("Адрес: ")]
        [Required(ErrorMessage = "Това поле е задължитено!")]
        public string Address { get; set; }

        [DisplayName("Сметка: ")]
        [Required(ErrorMessage = "Това поле е задължитено!")]
        public double AccountBalance { get; set; }

        [DisplayName("Абонамент: ")]
        [Required(ErrorMessage = "Това поле е задължитено!")]
        public bool DeluxeAccount { get; set; }
    }
}
