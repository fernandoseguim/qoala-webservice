using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QoalaWS.Extensions
{
    public static class LogExtensions
    {
        public static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ILog Logger(this Object obj)
        {
            return log4net.LogManager.GetLogger(obj.GetType());
            //return log;
        }
    }

    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class LogTestClass
    {
        [TestMethod]
        public void LogTest()
        {
            this.Logger().Debug("Debug test");
            this.Logger().Error("Error test");
            this.Logger().Fatal("Fatal test");
            this.Logger().Warn("WARN test");
            this.Logger().Info("Info test");
        }
    }
}