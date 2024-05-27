using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Entities
{
    public class Worker: BaseEntity
    {
        public Worker(string firstName, string lastName, string emailAddress, double salary, bool isFullTime, DateTime registeredOn)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Salary = salary;
            IsFullTime = isFullTime;
            RegisteredOn = registeredOn;
        }

        public Worker()
        {
            
        }

        [Display(Name="Име")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Е-майл")]
        [StringLength(100)]
        [Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Заплата")]
        [Required]
        public double Salary { get; set; }

        [Display(Name = "Full-Time")]
        [Required]
        public bool IsFullTime { get; set; }

        [Display(Name = "Регистриран на")]
        [Required]
        public DateTime RegisteredOn { get; set; }

        public int TotalOrders { get; set; }
    }
}
