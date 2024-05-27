using MyApplication.Models;
using MyApplication.ViewModels.Workers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using MyApplication.Services;
using ProjectApi.CommConstants;
using ProjectApi.Entities;


namespace MyApplication.Controllers
{
    public class WorkersController : Controller
    {
        private readonly ILogger<WorkersController> _logger;

        public WorkersController(ILogger<WorkersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await WorkerService.Instance.GetAllAsync<List<WorkerResponse>>();

                if (response == null)
                    return BadRequest("Възникна грешка докато зареждахме работниците.Моля опитайте по-късно!");

                IndexVM vm = new IndexVM();
                var allWorker = response.Select(workerResponse => new Worker()
                {
                    Id = workerResponse.Id,
                    FirstName = workerResponse.FirstName,
                    LastName = workerResponse.LastName,
                    EmailAddress = workerResponse.EmailAddress,
                    Salary = workerResponse.Salary,
                    IsFullTime = workerResponse.IsFullTime,
                    RegisteredOn = workerResponse.RegisteredOn,
                    TotalOrders = workerResponse.TotalOrders,
                }).ToList();

                vm.Worker = allWorker;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "Моля опитайте по - късно!", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddVM addVM)
        {
            try
            {
                var response = await WorkerService.Instance.PostAsync<WorkerResponse>(new CreateWorkerRequest(addVM.FirstName, addVM.LastName, addVM.EmailAddress, addVM.Salary, addVM.IsFullTime, DateTime.Now));

                if (response == null)
                    return BadRequest("Възникна грешка при добавянето на новия работник!");    

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
            var response = await WorkerService.Instance.GetAsync<WorkerResponse>(id.ToString());

            Worker worker = new Worker()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                EmailAddress = response.EmailAddress,
                Salary = response.Salary,
                IsFullTime = response.IsFullTime,
                RegisteredOn = response.RegisteredOn
            };
            EditVM vm = new EditVM();
            vm.Worker = worker;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await WorkerService.Instance.PutAsync<OkResult>(vm.Worker.Id, new UpdateWorkerRequest(vm.Worker.Id, vm.Worker.FirstName, vm.Worker.LastName, vm.Worker.EmailAddress, vm.Worker.Salary, vm.Worker.IsFullTime, vm.Worker.RegisteredOn));

                if (response == null)
                    return BadRequest("Възникна грешка при записване на промените! Моля опитайте по-късно!");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = " Моля опитайте по-късно!", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = " Моля опитайте по-късно!", details = ex.Message });
            }
        }


        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await WorkerService.Instance.DeleteAsync<OkResult>(id.ToString());

                if (response == null)
                    return BadRequest("Възникна грешка при изтриването! Моля опитайте по-късно!");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "Моля опитайте по-късно!.", details = httpRequestException.Message });
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
                var responseList = await WorkerService.Instance.GetSearchAsync<List<WorkerResponse>>(firstName);

                if (responseList == null)
                    return BadRequest("Излезна грешка при добавянето на работник!");

                SearchVM vm = new SearchVM();
                var WorkersList = responseList.Select(response => new Worker()
                {
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    EmailAddress = response.EmailAddress,
                    Salary = response.Salary,
                    IsFullTime = response.IsFullTime,
                    RegisteredOn = response.RegisteredOn,
                }).ToList();

                vm.Workers = WorkersList;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "Моля опитайте по-късно", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно", details = ex.Message });
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
    }
}
