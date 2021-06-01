using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioWebsiteAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsiteAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;

        public HomeController(EmailAddress _fromAddress, IEmailService _emailService)
        {
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Projects()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                EmailMessage msgToSend = new EmailMessage
                {
                    FromAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    ToAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    Content = $"Here is your message: Name: {model.Name}, " +
                    $"Email: {model.Email}, Message: {model.Message}",
                    Subject = "Contact Form - Personal Website"

                };

                EmailService.Send(msgToSend);
                return RedirectToAction("Success");
            }

            else
            {
                return View();
            }
        }
    }
}
