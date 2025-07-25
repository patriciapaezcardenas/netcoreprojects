using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Models;

namespace WebProject.ViewModels;

public class AccountCreateVM : Account
{
    public IEnumerable<SelectListItem> AccountTypes { get; set; }
}
