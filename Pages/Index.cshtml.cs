using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Models;
using Roster.Utilities;

namespace Roster.Pages
{
    public class IndexModel : PageModel
    {
        private static CloudTableHelper _tableHelper;

        public IndexModel()
        {
            if (_tableHelper == null)
            {
                _tableHelper = new CloudTableHelper();
            }
        }
        public List<Member> Members;

        public void OnGet()
        {
            Member member = _tableHelper.GetMember("Ingram", "mingram@vt.edu");
            Members = new List<Member>();
            Members.Add(member);
        }
    }
}