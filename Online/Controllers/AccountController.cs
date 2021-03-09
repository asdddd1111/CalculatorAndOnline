using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online.DbStaff.Model;
using Online.DbStaff.Repository;
using Online.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Online.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository userRepository;
        private IMapper mapper;
        public AccountController(UserRepository userRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        [Authorize]
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            var viewModel = new UserViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel viewModel)
        {
            var user = userRepository.GetUserByNameAndPassword(viewModel.Login, viewModel.Password);
            if (user == null)
            {
                return View(viewModel);
            }
            var recordId = new Claim("Id", user.Id.ToString());
            var recordName = new Claim(ClaimTypes.Name, user.Login);
            var recordAuthMethod = new Claim(ClaimTypes.AuthenticationMethod, Startup.AuthMethod);
            var page = new List<Claim>() { recordId, recordName, recordAuthMethod };
            var claimsIdentity = new ClaimsIdentity(page, Startup.AuthMethod);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            if (string.IsNullOrEmpty(viewModel.ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(viewModel.ReturnUrl);
            }
        }
        [HttpGet]
        public IActionResult Registration()
        {
            var viewModel = new RegistrationViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = mapper.Map<User>(viewModel);
            userRepository.Save(user);
            return RedirectToAction("Index", "Account");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult CheckLogin(string login)
        {
            var answer = userRepository.IsUnique(login);
            if (answer == false)
            {
                return Json("Этот логин занят");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
