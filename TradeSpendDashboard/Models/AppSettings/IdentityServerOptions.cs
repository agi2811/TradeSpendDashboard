using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Models.AppSettings
{
    public class IdentityServerOptions
    {
        public string Authority { get; set; }
        public string UrlApiUser { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool EnableCaching { get; set; }
        public int CacheDuration { get; set; }
        public string SignedInRedirectUri { get; set; }
        public string Scope { get; set; }
        public string SignedOutRedirectUri { get; set; }
    }
}
