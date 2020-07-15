using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ResearchTube.Areas.Identity.Pages.Account.Manage
{
    public static class ProfileNavModel
    {
        public static string Home => "Home";

        public static string Profile => "Profile";

        public static string Settings => "Settings";

        public static string VideoLibrary => "VideoLibrary";

        public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Home);

        public static string ProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, Profile);

        public static string SettingsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Settings);

        public static string VideoLibraryNavClass(ViewContext viewContext) => PageNavClass(viewContext, VideoLibrary);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
