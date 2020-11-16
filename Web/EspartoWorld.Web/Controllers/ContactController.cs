namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Services.Messaging;
    using EspartoWorld.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        private readonly IEmailSender emailSender;
        private readonly IContactFormService contactService;

        public ContactController(IEmailSender emailSender, IContactFormService contactService)
        {
            this.emailSender = emailSender;
            this.contactService = contactService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel model)
        {
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            await this.contactService.AddAsync(model, ip);
            await this.emailSender.SendEmailAsync(model.Email, model.Name, "espartoworld@gmail.com", model.Title, model.Content);
            return this.Redirect("/");
        }
    }
}
