using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using solution.IoC;

namespace solution.App_Start
{
    public class IocConfig
    {
        public static void RegisterCastleWindsor()
        {
            Container.Install();
        }
    }
}