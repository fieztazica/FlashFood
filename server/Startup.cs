using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using server.Models;
using System.Configuration;

[assembly: OwinStartup(typeof(server.Startup))]

namespace server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {   
            ConfigureAuth(app);
        }
    }
}
