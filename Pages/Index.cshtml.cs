using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace corewebftp.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            
            

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Logon();

            return Page();
        }

        public string Logon() {
            
            var status = "";

            return status;
        }

    }
}
