using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ProjectApi.Entities;

namespace MyApplication.ViewModels.Orders
{
    public class AddVM
    {

        public AddVM()
        {
            Customers = new List<Customer>();
            Workers = new List<Worker>();
        }

        [DisplayName("Продукти: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public string Details { get; set; }

        [DisplayName("Количество: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public int Quantity { get; set; }

        [DisplayName("Бакшиш (BGN): ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public double Tip { get; set; }

        [DisplayName("Обща сума (BGN): ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public double Total { get; set; }

        [DisplayName("Eкспресна доставка: ")]
        public bool IsExpress { get; set; }

        [DisplayName("Клиент: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public int Customer_ID { get; set; }

        public List<Customer> Customers { get; set; }

        [DisplayName("Работник: ")]
        [Required(ErrorMessage = "Това поле е задължително!")]
        public int Worker_ID { get; set; }

        public List<Worker> Workers { get; set; }
    }
}
