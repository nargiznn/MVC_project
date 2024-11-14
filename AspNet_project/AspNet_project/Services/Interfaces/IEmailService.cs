using System;
namespace AspNet_project.Services.Interfaces
{
	public interface IEmailService
	{
        void Send(string to, string subject, string html, string from = null);
    }
}

