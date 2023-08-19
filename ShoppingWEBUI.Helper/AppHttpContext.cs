using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Helper
{
    public class AppHttpContext
    {
        static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
            set { _serviceProvider = value; }
        }

        public static HttpContext Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor =_serviceProvider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;

                return httpContextAccessor.HttpContext;
            }
        }
    }
}
