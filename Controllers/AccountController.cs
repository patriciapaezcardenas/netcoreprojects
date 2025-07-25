using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Models;
using WebProject.Repositories;
using WebProject.Services;
using WebProject.ViewModels;

namespace WebProject.Controllers;

public class AccountController : Controller
{
    public readonly IUserService userService;
    public readonly IAccountTypeRepository accountTypeRepository;
    public readonly IAccountRepository accountRepository;


    public AccountController(
        IUserService userService,
        IAccountTypeRepository accountTypeRepository,
        IAccountRepository accountRepository
        )
    {
        this.userService = userService;
        this.accountTypeRepository = accountTypeRepository;
        this.accountRepository = accountRepository;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var userId = await userService.GetUserId();
        var viewModel = new AccountCreateVM
        {
            AccountTypes =  await GetAccountTypes(userId)
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AccountCreateVM accountCreateVM)
    {
        var userId = await userService.GetUserId();
        var accountType = await accountTypeRepository.GetbyIdAsync(accountCreateVM.AccountTypeId, userId);

        if (accountType == null)
        {
            return RedirectToAction("ResourceNotFound");
        }

        if (!ModelState.IsValid)
        {
            accountCreateVM.AccountTypes = await GetAccountTypes(userId);
            return View(accountCreateVM);
        }

        var account = new Account
        {
            Name = accountCreateVM.Name,
            AccountTypeId = accountCreateVM.AccountTypeId,
            Balance = accountCreateVM.Balance,
            Description = accountCreateVM.Description
        };

        await accountRepository.CreateAccountAsync(account);
        return View(account);
    }

    private async Task<IEnumerable<SelectListItem>> GetAccountTypes(int userId)
    {
        var accountTypes = await accountTypeRepository.GetAllByUserIdAsync(userId);

        return accountTypes.Select(x=> new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        });
    }

}
