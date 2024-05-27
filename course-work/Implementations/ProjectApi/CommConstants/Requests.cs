namespace ProjectApi.CommConstants
{
    #region Customers Request
    public class CreateCustomerRequest : DataRequest
    {
        public CreateCustomerRequest(string firstName, string lastName, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn) 
            : base(Endpoints.CreateCustomerEndPoint)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountBalance = accountBalance;
            DeluxeAccount = deluxeAccount;
            RegisteredOn = registeredOn;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public double AccountBalance { get; }
        public bool DeluxeAccount { get; }
        public DateTime RegisteredOn { get; }
    }

    public class GetCustomerRequest : DataRequest
    {
        public GetCustomerRequest(int id)
            : base(Endpoints.GetCustomerEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetAllCustomerRequest : NoDataRequest
    {
        public GetAllCustomerRequest()
            : base(Endpoints.GetAllCustomersEndPoint)
        { }
    }

    public class UpdateCustomerRequest : DataRequest
    {
        public UpdateCustomerRequest(int id ,string firstName, string lastName, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn)
            : base(Endpoints.UpdateCustomerEndPoint)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountBalance = accountBalance;
            DeluxeAccount = deluxeAccount;
            RegisteredOn = registeredOn;
        }
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public double AccountBalance { get; }
        public bool DeluxeAccount { get; }
        public DateTime RegisteredOn { get; }
    }

    public class DeleteCustomerRequest : DataRequest
    {
        public DeleteCustomerRequest(int id)
            : base(Endpoints.DeleteCustomerEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class SearchCustomersByFirstNameRequest : DataRequest
    {
        public SearchCustomersByFirstNameRequest(string firstName)
            : base(Endpoints.SearchCustomersByFirstNameEndPoint)
        {
            FirstName = firstName;
        }
        public string FirstName { get; }
    }
    #endregion

    #region Worker Request
    public class CreateWorkerRequest : DataRequest
    {
        public CreateWorkerRequest(string firstName, string lastName, string emailAddress, double salary, bool isFullTime, DateTime registeredOn)
            : base(Endpoints.CreateWorkerEndPoint)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Salary = salary;
            IsFullTime = isFullTime;
            RegisteredOn = registeredOn;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public double Salary { get; }
        public bool IsFullTime { get; }
        public DateTime RegisteredOn { get; }
    }

    public class GetWorkerRequest : DataRequest
    {
        public GetWorkerRequest(int id)
            : base(Endpoints.GetWorkerEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetAllWorkerRequest : NoDataRequest
    {
        public GetAllWorkerRequest()
            : base(Endpoints.GetAllWorkersEndPoint)
        { }
    }

    public class UpdateWorkerRequest : DataRequest
    {
        public UpdateWorkerRequest(int id, string firstName, string lastName, string emailAddress, double salary, bool isFullTime, DateTime registeredOn)
            : base(Endpoints.UpdateWorkerEndPoint)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Salary = salary;
            IsFullTime = isFullTime;
            RegisteredOn = registeredOn;
        }
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public double Salary { get; }
        public bool IsFullTime { get; }
        public DateTime RegisteredOn { get; }
    }

    public class DeleteWorkerRequest : DataRequest
    {
        public DeleteWorkerRequest(int id)
            : base(Endpoints.DeleteWorkerEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class SearchWorkersByFirstNameRequest : DataRequest
    {
        public SearchWorkersByFirstNameRequest(string firstName)
            : base(Endpoints.SearchWorkersByFirstNameEndPoint)
        {
            FirstName = firstName;
        }
        public string FirstName { get; }
    }
    #endregion

    #region Order Request
    public class CreateOrderRequest : DataRequest
    {
        public CreateOrderRequest(string details, int quantity, double tip, double total, bool isExpress, DateTime placedOn, int customer_ID, int worker_ID)
            : base(Endpoints.CreateOrderEndPoint)
        {
            Details = details;
            Quantity = quantity;
            Tip = tip;
            Total = total;
            IsExpress = isExpress;
            PlacedOn = placedOn;
            Customer_ID = customer_ID;
            Worker_ID = worker_ID;
        }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public double Tip { get; set; }
        public double Total { get; set; }
        public bool IsExpress { get; set; }
        public DateTime PlacedOn { get; set; }
        public int Customer_ID { get; set; }
        public int Worker_ID { get; set; }
    }

    public class GetOrderRequest : DataRequest
    {
        public GetOrderRequest(int id)
            : base(Endpoints.GetOrderEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetAllOrdersRequest : NoDataRequest
    {
        public GetAllOrdersRequest()
            : base(Endpoints.GetAllOrdersEndPoint)
        { }
    }

    public class UpdateOrderRequest : DataRequest
    {
        public UpdateOrderRequest(int id, string details, int quantity, double tip, double total, bool isExpress, DateTime placedOn, int customer_ID, int worker_ID)
            : base(Endpoints.UpdateOrderEndPoint)
        {
            Id = id;
            Details = details;
            Quantity = quantity;
            Tip = tip;
            Total = total;
            IsExpress = isExpress;
            PlacedOn = placedOn;
            Customer_ID = customer_ID;
            Worker_ID = worker_ID;
        }

        public int Id { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public double Tip { get; set; }
        public double Total { get; set; }
        public bool IsExpress { get; set; }
        public DateTime PlacedOn { get; set; }
        public int Customer_ID { get; set; }
        public int Worker_ID { get; set; }
    }

    public class DeleteOrderRequest : DataRequest
    {
        public DeleteOrderRequest(int id)
            : base(Endpoints.DeleteOrderEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class SearchOrdersByDetailsRequest : DataRequest
    {
        public SearchOrdersByDetailsRequest(string details)
            : base(Endpoints.SearchOrdersByDetailsEndPoint)
        {
            Details = details;
        }
        public string Details { get; }
    }
    #endregion
}
