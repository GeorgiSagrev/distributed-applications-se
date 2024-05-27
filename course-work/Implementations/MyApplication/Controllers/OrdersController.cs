using MyApplication.Models;
using MyApplication.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using MyApplication.Services;
using System.Text;
using ProjectApi.CommConstants;
using ProjectApi.Entities;

namespace MyApplication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await OrderService.Instance.GetAllAsync<List<OrderResponse>>();
                if (response == null)
                    return BadRequest("Възникна грешка докато се опитвахме да заредим поръчките Ви!");

                IndexVM vm = new IndexVM();
                var allOrders = response.Select(orderResponse => GenerateOrder(orderResponse)).ToList();

                vm.Orders = allOrders;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "Опитайте пак по-късно!", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Възникна грешка. Опитайте пак по-късно!", details = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Retreive all customers
            var customersResponse = await CustomerService.Instance.GetAllAsync<List<CustomerResponse>>();

            if (customersResponse == null)
                return BadRequest("Не можахме да заредин клиентите. Няма връзка със сървъра!");

            //Retreive all Workers
            var workersResponse = await WorkerService.Instance.GetAllAsync<List<WorkerResponse>>();

            if (workersResponse == null)
                return BadRequest("Не можахме да заредин работниците. Няма връзка със сървъра!");

            AddVM vm = new AddVM();
            vm.Customers = customersResponse.Select(customerResponse => GenerateCustomer(customerResponse)).ToList();
            vm.Workers = workersResponse.Select(workerResponse => GenerateWorker(workerResponse)).ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddVM addVM)
        {
            try
            {
                var response = await OrderService.Instance.PostAsync<OkResult>(new CreateOrderRequest(addVM.Details, addVM.Quantity, addVM.Tip, addVM.Total, addVM.IsExpress, DateTime.Now, addVM.Customer_ID, addVM.Worker_ID));

                if (response == null)
                    return BadRequest("Не можахме да добажим поръчката Ви. Няма връзка със сървъра!");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "Моля опитайте по-късно!", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var orderResponse = await OrderService.Instance.GetAsync<OrderResponse>(id.ToString());

            EditVM vm = new EditVM();
            vm.Order = GenerateOrder(orderResponse);

            var customersResponse = await CustomerService.Instance.GetAllAsync<List<CustomerResponse>>();

            if (customersResponse == null)
                return BadRequest("Не можахме да заредим клиентите на които искате да редактирате поръчките! Няма връзка със сървъра!");

            var workersResponse = await WorkerService.Instance.GetAllAsync<List<WorkerResponse>>();

            if (workersResponse == null)
                return BadRequest("Не можахме да заредим работниците на които искате да редактирате поръчките! Няма връзка със сървъра!");

            vm.Customers = customersResponse.Select(customerResponse => GenerateCustomer(customerResponse)).ToList();
            vm.Workers = workersResponse.Select(workerResponse => GenerateWorker(workerResponse)).ToList();

            var optionsHtml = new StringBuilder();
            foreach (var worker in vm.Workers)
            {
                var selected = worker.Id == vm.Order.Worker_ID ? "selected=\"selected\"" : "";
                optionsHtml.Append($"<option {selected} value=\"{worker.Id}\">{worker.FirstName} {worker.LastName}</option>");
            }
            ViewBag.WorkerOptions = optionsHtml.ToString();

            optionsHtml = new StringBuilder();
            foreach (var customer in vm.Customers)
            {
                var selected = customer.Id == vm.Order.Customer_ID ? "selected=\"selected\"" : "";
                optionsHtml.Append($"<option {selected} value=\"{customer.Id}\">{customer.FirstName} {customer.LastName}</option>");
            }
            ViewBag.CustomerOptions = optionsHtml.ToString();

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await OrderService.Instance.PutAsync<OkResult>(vm.Order.Id, new UpdateOrderRequest(vm.Order.Id, vm.Order.Details, vm.Order.Quantity, vm.Order.Tip, vm.Order.Total, vm.Order.IsExpress, vm.Order.PlacedOn, vm.Order.Customer_ID, vm.Order.Worker_ID));

                if (response == null)
                    return BadRequest("Не можахме да редактираме поръчката.");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "Моля опитайте по-късно!", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await OrderService.Instance.DeleteAsync<OkResult>(id.ToString());

                if (response == null)
                    return BadRequest("Не можахме да изтрием поръчката");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error =" Моля опитайте по - късно!.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Search(string firstName)
        {
            try
            {
                var responseList = await OrderService.Instance.GetSearchAsync<List<OrderResponse>>(firstName);

                if (responseList == null)
                    return BadRequest("Не можахме да проверим за поръчки. Моля опитайте по-късно!");

                SearchVM vm = new SearchVM();
                var ordersList = responseList.Select(response => GenerateOrder(response)).ToList();

                vm.Orders = ordersList;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "Моля опитайте по-късно!", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Order GenerateOrder(OrderResponse responce)
        {
            return new Order()
            {
                Id = responce.Id,
                Details = responce.Details,
                Tip = responce.Tip,
                Total = responce.Total,
                IsExpress = responce.IsExpress,
                PlacedOn = responce.PlacedOn,
                Quantity = responce.Quantity,

                //Foreign Keys
                Customer = responce.Customer,
                Customer_ID = responce.Customer.Id,
                Worker = responce.Worker,
                Worker_ID = responce.Worker.Id,
            };
        }

        private Customer GenerateCustomer(CustomerResponse customerResponse)
        {
            return new Customer()
            {
                Id = customerResponse.Id,
                FirstName = customerResponse.FirstName,
                LastName = customerResponse.LastName,
                Address = customerResponse.Address,
                AccountBalance = customerResponse.AccountBalance,
                DeluxeAccount = customerResponse.DeluxeAccount,
                RegisteredOn = customerResponse.RegisteredOn,
            };
        }

        private Worker GenerateWorker(WorkerResponse workerResponse)
        {
            return new Worker()
            {
                Id = workerResponse.Id,
                FirstName = workerResponse.FirstName,
                LastName = workerResponse.LastName,
                EmailAddress = workerResponse.EmailAddress,
                Salary = workerResponse.Salary,
                IsFullTime = workerResponse.IsFullTime,
                RegisteredOn = workerResponse.RegisteredOn,
            };
        }


    }
}
