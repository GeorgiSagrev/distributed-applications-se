using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Entities
{
    public class Customer : BaseEntity
    {
        public Customer(string firstName, string lastName, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountBalance = accountBalance;
            DeluxeAccount = deluxeAccount;
            RegisteredOn = registeredOn;
        }
        public Customer()
        {
            
        }

        [Display(Name = "Име")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(100)]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Сметка")]
        [Required]
        public double AccountBalance { get; set; }

        [Display(Name = "Сметка")]
        public bool DeluxeAccount { get; set; }

        [Display(Name = "Регистриран на ")]
        [Required]
        public DateTime RegisteredOn { get; set; }

        public int TotalOrders { get; set; }
    }
}
