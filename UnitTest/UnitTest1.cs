using Lab_7_MagDiser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            double square = 1;
            double wallheight = 2;
            int windows = 3;
            int doors = 4;
            double coefficient1 = 5;
            double coefficient2 = 6;

            double axc = 35.4;

            Form2 c = new Form2();
            double act = c.powerCal(square, wallheight, windows, doors, coefficient1, coefficient2);

            Assert.AreEqual(axc, act);
        }

        [TestMethod]
        public void TestMethod2()
        {
            double x = 150;
            double y = 1.1;
            
            double axc = 151.1;

            Form1 c = new Form1();
            double act = c.totPower(x, y);

            Assert.AreEqual(axc, act);
        }

        [TestMethod]
        public void TestMethod3()
        {
            double x = 151.1;
            
            double axc = 3777.5;

            Form1 c = new Form1();
            double act = c.totPowerCor(x);

            Assert.AreEqual(axc, act);
        }
    }
}
