﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BBNet.Web.Startup))]

namespace BBNet.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}