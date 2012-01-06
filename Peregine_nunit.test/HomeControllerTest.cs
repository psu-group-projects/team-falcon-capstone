/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/06/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PeregrineUI_2.Controllers;

namespace Peregine_nunit.test
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Put_Msg_In_ViewBag()
        { 
            var Controller = new HomeController();
            var result = Controller.Index();
            Assert.IsNotNull(result.ViewBag);

        }
    }
}
