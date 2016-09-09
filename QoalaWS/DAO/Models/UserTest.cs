using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QoalaWS.DAO
{
    
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class UserTestClass
    {
        [TestMethod]
        public void MyTestMethod()
        {
            (new DAO.USER()).register();
        }
    }
}