
using System.Configuration;

namespace ClientManagementSystemMVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientDbContext _context;
        private readonly IConfiguration _configuration;
        public ClientController(ClientDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientModel == null)
            {
                return NotFound();
            }

            return View(clientModel);
        }

        // GET: Client/AddOrEdit
        // GET: Client/AddOrEdit/5
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)            
                return View(new ClientModel());
            
            else
            {
                var clientModel = await _context.Clients.FindAsync(id);
                if (clientModel == null)
                {
                    return NotFound();
                }
                return View(clientModel);
            }
        }

        // POST: Client/AddOrEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,PhoneNumber,Email,HomeAddress")] ClientModel clientModel)
        {         
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var record = await _context.Clients.FirstOrDefaultAsync(i=>i.Name == clientModel.Name &&
                        i.Email == clientModel.Email &&
                        i.PhoneNumber == clientModel.PhoneNumber &&
                        i.HomeAddress == clientModel.HomeAddress
                    );
                    if (record == null)
                    {
                        _context.Add(clientModel);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Clients.ToList()) });
                    }

                }
                else
                {
                    try
                    {
                        _context.Update(clientModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClientModelExists(clientModel.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                
                return Json( new { isValid = true, html = Helper.RenderRazorViewToString(this,"_ViewAll",_context.Clients.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", clientModel)});
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientModel == null)
            {
                return NotFound();
            }

            return View(clientModel);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientModel = await _context.Clients.FindAsync(id);
            if (clientModel != null)
            {
                _context.Clients.Remove(clientModel);
            }

            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Clients.ToList()) });

        }


        // GET: Client/SendEmail/email
        public async Task<IActionResult> SendEmail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var clientModel = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientModel == null)
            {
                return NotFound();
            }

            return View(clientModel);
        }

        // POST: Client/SendEmail/email
        [HttpPost, ActionName("SendEmail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmailConfirmed(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var clientModel = await _context.Clients.FindAsync(id);

            var emailOptions = new EmailSettingOptions();
            _configuration.GetSection(EmailSettingOptions.EmailSetting).Bind(emailOptions);

            string toEmail = clientModel.Email;

            // Create the email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailOptions.Sender);
            mailMessage.Subject = emailOptions.Subject;
            mailMessage.To.Add(new MailAddress(toEmail));
            mailMessage.Body = emailOptions.Body;
            mailMessage.IsBodyHtml = true;

            // Set up the SMTP client
            SmtpClient smtpClient = new SmtpClient(emailOptions.SmtpServer)
            {
                Port = int.Parse(emailOptions.Port), // Gmail SMTP port
                Credentials = new NetworkCredential(emailOptions.Sender, emailOptions.SmtpPassword),
                EnableSsl = true
            };

            try
            {
                
                smtpClient.Send(mailMessage);
               
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Clients.ToList()) });

        }

        private bool ClientModelExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
