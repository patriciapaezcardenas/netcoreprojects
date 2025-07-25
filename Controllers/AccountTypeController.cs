using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using WebProject.Repositories;
using WebProject.Services;

namespace WebProject.Controllers
{
    /// <summary>
    /// Controller for managing account types.
    /// This controller handles the creation and verification of account types.
    /// </summary>
    public class AccountTypeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAccountTypeRepository _iAccountTypeRepository;
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for AccountTypeController.
        /// Initializes the controller with dependencies for logging, configuration, and account type repository.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="iAccountTypeRepository"></param>
        public AccountTypeController(ILogger<HomeController> logger
            , IConfiguration configuration
            , IAccountTypeRepository iAccountTypeRepository
            , IUserService userService)
        {
            _configuration = configuration;
            _logger = logger;
            _iAccountTypeRepository = iAccountTypeRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = await _userService.GetUserId(); // Assuming a default user ID for demonstration purposes
            var accountTypes = await _iAccountTypeRepository.GetAllByUserIdAsync(userId);
            return View(accountTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountType accountType)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is invalid for AccountType creation.");
                return View(accountType);
            }

            accountType.UserId = await _userService.GetUserId(); // Assuming a default user ID for demonstration purposes

            if (await _iAccountTypeRepository.IsExistsAsync(accountType))
            {
                ModelState.AddModelError("Name", "El tipo de cuenta ya existe.");
                _logger.LogWarning($"Account type with name '{accountType.Name}' already exists.");
                return View(accountType);
            }

            await _iAccountTypeRepository.AddAsync(accountType);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = await _userService.GetUserId(); // Assuming a default user ID for demonstration purposes
            var accountType = await _iAccountTypeRepository.GetbyIdAsync(id, userId);

            if (accountType == null)
            {
                _logger.LogError($"Account type with ID {id} not found.");
                return NotFound();
            }

            return View(accountType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountType accountType)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is invalid for AccountType edit.");
                return View(accountType);
            }

            var userId = await _userService.GetUserId(); // Assuming a default user ID for demonstration purposes
            accountType.UserId = userId;

            if (!await _iAccountTypeRepository.IsExistsAsync(accountType))
            {
                ModelState.AddModelError("AccountType", "El tipo de cuenta no existe.");
                _logger.LogWarning($"Account type with id '{accountType.Id}' does not exist.");
                return RedirectToAction("NotFound");
            }

            await _iAccountTypeRepository.ModifyAsync(accountType);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> VerifyAccountTypeIsExist(string name)
        {
            var userId = await _userService.GetUserId(); // Assuming a default user ID for demonstration purposes
            var accountType = new AccountType
            {
                Name = name,
                UserId = userId
            };

            var isExists = await _iAccountTypeRepository.IsExistsAsync(accountType);

            if (isExists)
            {
                return Json($"El tipo de cuenta '{name}' ya existe.");
            }

            return Json(true);
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await _userService.GetUserId(); // Assuming a default user ID for demonstration purposes
            var accountType = await _iAccountTypeRepository.GetbyIdAsync(id, userId);

            if (accountType == null)
            {
                _logger.LogError($"Account type with ID {id} not found.");
                return RedirectToAction(nameof(HomeController.ResourceNotFound));
            }

            return View(accountType);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AccountType accountType)
        {
            var userId = await _userService.GetUserId(); // Assuming a default user ID for demonstration purposes
            var accountTypeResult = await _iAccountTypeRepository.GetbyIdAsync(accountType.Id, userId);
            if (accountTypeResult == null)
            {
                _logger.LogError($"Account type with ID {accountType.Id} not found.");
                return RedirectToAction(nameof(HomeController.ResourceNotFound));
            }

            await _iAccountTypeRepository.DeleteAsync(accountTypeResult.Id);
            _logger.LogInformation($"Account type with ID {accountType.Id} has been deleted.");

            return RedirectToAction(nameof(Index));
        }

        // The ids parameter might be null for several reasons:
        // 1. The request body is not being sent in the correct JSON format
        // 2. The Content-Type header is not set to 'application/json'
        // 3. The request payload does not match the expected int[] format
        // To fix:
        // - Ensure request sends JSON array like: [1,2,3]
        // - Add 'Content-Type: application/json' header
        // - Verify the client-side data matches int[] type
        [HttpPost]
        public async Task<IActionResult> Order([FromBody] int[] ids)
        {
            var userId = await _userService.GetUserId(); // Assuming a default user ID for demonstration purposes
            var accountTypeList = await _iAccountTypeRepository.GetAllByUserIdAsync(userId);
            var extraAccountType = ids.Except(accountTypeList.Select(x=> x.Id)).AsEnumerable();
      
            if (extraAccountType.Count() > 0)
            {
                _logger.LogError($"Account types selected dot not match with user account types.");
                return Forbid();
            }

            var sortedAccountTypes = ids.Select((value, index) =>
             new AccountType { Id = value, Order = index + 1 }).AsEnumerable();

            await _iAccountTypeRepository.UpdateOrder(sortedAccountTypes);

            return Ok("Index");
        }
    }   
}
