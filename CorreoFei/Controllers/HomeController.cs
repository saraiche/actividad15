﻿using CorreoFei.Models;
using CorreoFei.Services.Email;
using CorreoFei.Services.ErrorLog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CorreoFei.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmail _email;
        private readonly IErrorLog _errorLog;

        public HomeController(IErrorLog errorLog, IEmail email)
        {
            _errorLog = errorLog;
            _email = email;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(ContactoViewModel contacto)
        {
            if(ModelState.IsValid) 
            {
                try
                {
                    //envia
                    await _email.EnviaCorreoAsync("Correo electrónico desde FEI", contacto.Correo, null, null, CuerpoCorreo(contacto.Nombre));

                    return RedirectToAction(nameof(Success));
                }
                catch (Exception ex)
                {
                    await _errorLog.ErrorLogAsync(ex.Message);
                }
            }

            ModelState.AddModelError("", "No ha sido posible enviar el correo. Inténtelo nuevamente.");
            return View();
        }

        public string CuerpoCorreo(string nombre)
        {
            string Mensaje = $"<p>Estimado/Estimada usuario, {nombre}</p>";
            Mensaje += "<p>Por este medio le informamos que su particípación ha sido recibida.</p>";
            Mensaje += "<p>Agradecemos su participación.</p>";
            Mensaje += "<br /><br /><br /><div>-----------------------------------------</div>";
            Mensaje += "<div>Mensaje enviado automáticamente. Favor de no responder al remitente de este mensaje.</div>";

            return Mensaje;
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}