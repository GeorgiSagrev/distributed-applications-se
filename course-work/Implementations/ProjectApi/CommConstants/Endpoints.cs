namespace ProjectApi.CommConstants
{
    public static class Endpoints
    {
        #region Customer endpoints
        public const string CreateCustomerEndPoint = "createcustomer";
        public const string GetCustomerEndPoint = "getcustomer";
        public const string GetAllCustomersEndPoint = "getallcustomers";
        public const string UpdateCustomerEndPoint = "updatecustomer";
        public const string DeleteCustomerEndPoint = "deletecustomer";
        public const string SearchCustomersByFirstNameEndPoint = "searchcustomer";
        #endregion

        #region Worker endpoints
        public const string CreateWorkerEndPoint = "createworker";
        public const string GetWorkerEndPoint = "getworker";
        public const string GetAllWorkersEndPoint = "getallworkers";
        public const string UpdateWorkerEndPoint = "updateworker";
        public const string DeleteWorkerEndPoint = "deleteworker";
        public const string SearchWorkersByFirstNameEndPoint = "searchworker";
        #endregion

        #region Order endpoints
        public const string CreateOrderEndPoint = "createorder";
        public const string GetOrderEndPoint = "getorder";
        public const string GetAllOrdersEndPoint = "getallorders";
        public const string UpdateOrderEndPoint = "updateorder";
        public const string DeleteOrderEndPoint = "deleteorder";
        public const string SearchOrdersByDetailsEndPoint = "searchorder";
        #endregion
    }
}
