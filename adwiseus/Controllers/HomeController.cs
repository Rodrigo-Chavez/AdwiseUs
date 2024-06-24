using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using adwiseus.Models;
using adwiseus.App_GlobalResources;

using static adwiseus.App_GlobalResources.Resources;
using System.Web.Routing;
using System.Web;

namespace adwiseus.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        
        private EmailSettings _emailSettings;
        private readonly ResourceManager _resourceManager;
        public HomeController()
        {
            
            _emailSettings = new EmailSettings();
            _emailSettings.Subject = ConfigurationManager.AppSettings["EmailSettings:Subject"];
            _emailSettings.FromAddress = ConfigurationManager.AppSettings["EmailSettings:FromAddress"];
            _emailSettings.ToAddress = ConfigurationManager.AppSettings["EmailSettings:ToAddress"];
            _emailSettings.Password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            _resourceManager = new ResourceManager(typeof(adwiseus.App_GlobalResources.Resources));

        }

        public ActionResult Index()
        {
            
            
            return View();
        }

        public bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        [HttpPost]
        public ActionResult SendContactForm(string InputName, string InputEmail, string InputMessage)
        {
            var result = new EmailSendingResult
            {
                InputName = InputName,
                InputEmail = InputEmail,
                InputMessage = InputMessage,
                Success = true
            };

            try
            {
                if (string.IsNullOrWhiteSpace(InputName))
                {
                    throw new Exception("Nombre no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(InputEmail))
                {
                    throw new Exception("Email no puede ser nulo");
                }
                if (!IsValidEmail(InputEmail))
                {
                    throw new Exception("Debe ser un Email valido");
                }
                if (string.IsNullOrWhiteSpace(InputMessage))
                {
                    throw new Exception("Mensaje no puede ser nulo");
                }

                var fromPassword = _emailSettings.Password; // Contraseña de aplicación Gmail
                var fromAddress = new MailAddress(_emailSettings.FromAddress, InputName);
                var toAddress = new MailAddress(_emailSettings.ToAddress, "Destinatario");
                var subject = _emailSettings.Subject;

                string body = $@"
                                <html>
                                <head>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            background-color: #f4f4f4;
                                            padding: 20px;
                                        }}
                                        .container {{
                                            background-color: #ffffff;
                                            padding: 20px;
                                            border-radius: 8px;
                                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                                            max-width: 600px;
                                            margin: 0 auto;
                                        }}
                                        h2 {{
                                            color: #333333;
                                            border-bottom: 2px solid #eeeeee;
                                            padding-bottom: 10px;
                                        }}
                                        p {{
                                            color: #666666;
                                            line-height: 1.6;
                                        }}
                                        .info {{
                                            margin: 10px 0;
                                        }}
                                        .info strong {{
                                            display: inline-block;
                                            width: 100px;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='container'>
                                        <h2>Nuevo Mensaje de Contacto</h2>
                                        <div class='info'><p><strong>Nombre:</strong> {InputName}</p></div>
                                        <div class='info'><p><strong>Email:</strong> {InputEmail}</p></div>
                                        <div class='info'><p><strong>Mensaje:</strong> {InputMessage}</p></div>
                                    </div>
                                </body>
                                </html>
                                ";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }

                TempData["Message"] = "Tu mensaje ha sido enviado con éxito.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Error al enviar el mensaje: {ex.Message}";
                result.Success = false;
                result.Error = ex.Message;
            }

            return View("Contacto", result);
        }
        public JsonResult GetTimelineResource(string year, string url)
        {
            string url2 = url;
            if (url2!=null)
            {
                if (url2.StartsWith("/en-US"))
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                }
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            }
            var culture = Thread.CurrentThread.CurrentUICulture;
            var resource = new
            {
                h2 = Resources.ResourceManager.GetString($"timeline_{year}_h2", culture),
                h3 = Resources.ResourceManager.GetString($"timeline_{year}_h3",culture),
                text = Resources.ResourceManager.GetString($"timeline_{year}_text", culture)
            };

            return Json(resource, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TalentManagement()
        {
            return View();
        }

        public ActionResult ProcessInnovation()
        {
            return View();
        }

        public ActionResult FinanceAudit()
        {
            return View();
        }

        public ActionResult Outsourcing()
        {
            return View();
        }

        public ActionResult Contacto()
        {
            return View();
        }

        public ActionResult IndexArea()
        {
            return View();
        }

        public ActionResult IndexAdwise()
        {
            return View();
        }

        public ActionResult Nosotros()
        {
            return View();
        }

        public ActionResult Perfiles()
        {
            return View();
        }

        public ActionResult GoToPerfiles()
        {
            return RedirectToAction("Perfiles");
        }

        public ActionResult Error()
        {
            return View(new ErrorViewModel());
        }

        [HttpPost]
        public ActionResult ChangeCulture(string returnUrl, string culture)
        {
            string newCulture = CultureHelper.ToggleCulture(culture);
            string newUrl = ChangeUrlCulture(returnUrl, newCulture);
            var cultureInfo = new CultureInfo(newCulture);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return Redirect(newUrl);
        }

        private string ChangeUrlCulture(string url, string newCulture)
        {
             
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }

            
            string[] segments = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length == 0 || !IsCultureSegment(segments[0]))
            {
                // Insertar la cultura al inicio
                url = "/" + newCulture + url;
            }
            else
            {
               
                segments[0] = newCulture;
                url = "/" + string.Join("/", segments);
            }

            return url;
        }

        private bool IsCultureSegment(string segment)
        {
            
            return segment.Length == 5 && segment[2] == '-';
        }
    }
}
