﻿using System.Net.Mail;

namespace CorreoFei.Services.Email
{
    public interface IEmail
    {
        public Task<bool> EnviaCorreoAsync(string tema,
            string para, string cc, string bcc, string cuerpo,
            Attachment adjunto = null);
    }
}
