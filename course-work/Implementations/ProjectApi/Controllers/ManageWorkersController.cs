using ProjectApi.Repositories;
using ProjectApi.CommConstants;
using ProjectApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageWorkersController : ControllerBase
    {
        public ManageWorkersController()
        {
        }

        [HttpPost]
        public IActionResult CreateWorker(CreateWorkerRequest request)
        {
            try
            {
                // Save to database
                WorkersRepository workersRepo = new WorkersRepository();
                Worker worker = new Worker(request.FirstName, request.LastName, request.EmailAddress, request.Salary, request.IsFullTime, request.RegisteredOn);
                workersRepo.Save(worker);

                // Generate response
                var response = GenerateResponse(worker);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetWorker(int id)
        {
            try
            {
                // Retrieve from database
                WorkersRepository repo = new WorkersRepository();
                Worker worker = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);

                if (worker == null)
                {
                    return NotFound();
                }

                // Generate response
                var response = GenerateResponse(worker);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllWorkers()
        {
            try
            {
                WorkersRepository workersRepo = new WorkersRepository();
                OrdersRepository ordersRepo = new OrdersRepository();
                List<Worker> allWorkers = workersRepo.GetAll(i => true);
                List<Order> allOrders = ordersRepo.GetAll(i => true);

                foreach (var worker in allWorkers)
                {
                    int currentCount = 0;
                    foreach (var order in allOrders)
                    {
                        if (order.Worker_ID == worker.Id)
                        {
                            currentCount++;
                        }
                    }

                    worker.TotalOrders = currentCount;
                }

                var response = allWorkers.Select(worker => GenerateResponse(worker)).ToList();
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWorker(int id, UpdateWorkerRequest request)
        {
            try
            {
                WorkersRepository repo = new WorkersRepository();

                // Find the existing worker object
                var existingWorker = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (existingWorker == null)
                {
                    return NotFound();
                }

                // Update the existing worker object
                existingWorker.FirstName = request.FirstName;
                existingWorker.LastName = request.LastName;
                existingWorker.EmailAddress = request.EmailAddress;
                existingWorker.Salary = request.Salary;
                existingWorker.IsFullTime = request.IsFullTime;
                existingWorker.RegisteredOn = request.RegisteredOn;

                // Save changes to the database
                repo.Save(existingWorker);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorker(int id)
        {
            try
            {
                WorkersRepository repo = new WorkersRepository();

                Worker worker = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);

                if (worker == null)
                {
                    return NotFound();
                }

                repo.Delete(worker);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        [HttpGet("search/{searchWord}")]
        public IActionResult SearchWorkersByFirstName(string searchWord)
        {
            try
            {
                WorkersRepository repo = new WorkersRepository();
                List<Worker> workersSearchResult = repo.GetAll(n => n.FirstName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));

                var response = workersSearchResult.Select(worker => GenerateResponse(worker)).ToList();
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Моля опитайте по-късно!", details = ex.Message });
            }
        }

        private WorkerResponse GenerateResponse(Worker worker)
        {
            var response = new WorkerResponse
            {
                Id = worker.Id,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                EmailAddress = worker.EmailAddress,
                Salary = worker.Salary,
                IsFullTime = worker.IsFullTime,
                RegisteredOn = worker.RegisteredOn,
                TotalOrders = worker.TotalOrders
            };

            return response;
        }
    }
}