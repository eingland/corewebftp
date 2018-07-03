using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace corewebftp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public FtpCredentialModel FtpCredential { get; set; }

        public string result = "";
        public void OnGet()
        {
            
            

        }

        public async Task<IActionResult> OnPostAsync()
        {
            string uri = FtpCredential.Uri;
            string user = FtpCredential.Username;
            string pass = FtpCredential.Password;

            await SendRequest(uri, user, pass);

            return Page();
        }

        public Task SendRequest(string uri, string username, string password) {
            return Task.Run(() => {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);

                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Proxy = null;
                request.EnableSsl = true;
                request.Credentials = new NetworkCredential(username, password);
           
                using (FtpWebResponse resp = (FtpWebResponse)request.GetResponse())
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                    result += $"<br>Directory List Complete, status {resp.StatusDescription}";
                }
            });
        }

    }

    public class FtpCredentialModel
        {
            [Required]
            [Display(Name = "URL")]
            public string Uri { get; set; }
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }
            [Required]
            [Display(Name = "Password")]
            public string Password { get; set; }

        }
}
