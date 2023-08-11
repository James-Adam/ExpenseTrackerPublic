// This file is used by Code Analysis to maintain SuppressMessage attributes that are applied to
// this project. Project-level suppressions either have no target or are given a specific target and
// scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out",
        Justification = "<Pending>")]
[assembly:
    SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:ExpenseTracker.Controllers.AccountController.Register(ExpenseTracker.Models.ViewModels.RegisterVM)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly:
    SuppressMessage("Naming", "VSSpell001:Spell Check", Justification = "<Pending>", Scope = "member",
        Target = "~F:ExpenseTracker.Models.Static.UserRoles.Admin")]
[assembly:
    SuppressMessage("Naming", "VSSpell001:Spell Check", Justification = "<Pending>", Scope = "type",
        Target = "~T:ExpenseTracker.Models.AppDbInitializer")]
[assembly:
    SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "type",
        Target = "~T:ExpenseTracker.Migrations.initial")]