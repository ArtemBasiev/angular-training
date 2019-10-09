using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace bizapps_test.SL.API_Services.CustomConfigSection
{
    public class WebApiUrlSection : ConfigurationSection
    {

        [ConfigurationProperty("urlpart")]
        public string UrlPart
        {
            get { return ((string)this["urlpart"]); }
        }
    }
}