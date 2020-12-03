namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Services.Messaging;
    using EspartoWorld.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class ContactController : BaseController
    {
        private readonly IEmailSender emailSender;
        private readonly IContactFormService contactService;
        private readonly IConfiguration configuration;

        public ContactController(IEmailSender emailSender, IContactFormService contactService, IConfiguration configuration)
        {
            this.emailSender = emailSender;
            this.contactService = contactService;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(ContactFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var appEmail = this.configuration.GetSection("EmailSender:Email").Value;
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            await this.contactService.AddAsync(model, ip);
            await this.emailSender.SendEmailAsync(model.Email, model.Name, appEmail, model.Title, model.Email + System.Environment.NewLine + model.Content);
            await this.emailSender.SendEmailAsync(appEmail, GlobalConstants.SystemName, model.Email, "Your message is important for us", "Thank you for your message");
            return this.Redirect("/");
        }
    }
}
