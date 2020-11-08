namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Services.Messaging;
    using EspartoWorld.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        private readonly MailKitEmailSender emailSender;

        public ContactController(MailKitEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel model)
        {
            await this.emailSender.SendEmailAsync(model.Email, model.Name, "espartoworld@gmail.com", model.Title, model.Content);
            return this.Redirect("/");
        }
    }
}
