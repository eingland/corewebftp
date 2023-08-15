using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace corewebftp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public FtpCredentialModel FtpCredential { get; set; }

        public string result = "";

        public IEnumerable<SftpFile> fileList;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            string uri = FtpCredential.Uri;
            string user = FtpCredential.Username;
            string pass = FtpCredential.Password;

            await GetDirectoryList(uri, user, pass, "/");

            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await Task.Run(() => { FtpCredential = null; });
            return Page();
        }

        public Task GetDirectoryList(string uri, string user, string pass, string path) {
            return Task.Run(() => {
                result = "";
                try 
                {
                    var connectionInfo = new PasswordConnectionInfo(uri,
                                        user,
                                        pass);
                    using (var client = new SftpClient(connectionInfo))
                    {
                        client.Connect();
                    
                        fileList = client.ListDirectory(path);

                        result = "Directory List Complete";
                    }
                }
                catch (Exception e)
                {
                    result = "An exception has been caught " + e.ToString();
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
