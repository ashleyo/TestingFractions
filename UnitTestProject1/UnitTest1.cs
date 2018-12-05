using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractionLib;
using static FractionLib.Fraction;


namespace UnitTestProject1
{
    //ADDED static class to hold test data
    public static class TestData
    {
        public static Tuple<Fraction, Fraction, Fraction>[] TestDataForMultiply = new Tuple<Fraction, Fraction, Fraction>[]
        {
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Create(1,2), Fraction.Create(1,2), Fraction.Create(1,4)),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Create(-1,2), Fraction.Create(1,2), Fraction.Create(-1,4)),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Create(3,7), Fraction.Create(9,8), Fraction.Create(27,56)),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Unity, Fraction.Create(1,2), Fraction.Create(1,2)),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Zero,Fraction.Create(1,2), Fraction.Zero),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Create(5,2),Fraction.Create(5,2), Fraction.Create(25,4))
        };
        public static Tuple<Fraction, Fraction, Fraction>[] TestDataForAdd = new Tuple<Fraction, Fraction, Fraction>[]
        {
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Create(1,2), Fraction.Create(1,2), Fraction.Create(1)),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Create(-1,2), Fraction.Create(1,2), Fraction.Zero),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Create(3,7), Fraction.Create(9,8), Fraction.Create(87,56)),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Unity, Fraction.Create(1,2), Fraction.Create(3,2)),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Zero,Fraction.Create(0,2), Fraction.Zero),
               Tuple.Create<Fraction,Fraction,Fraction>(Fraction.Create(-5,2),Fraction.Create(5,-2), Fraction.Create(-5))
        };
        public static Tuple<Fraction, Fraction, int>[] TestDataForComparison = new Tuple<Fraction, Fraction, int>[]
        {
            Tuple.Create<Fraction,Fraction,int>(Fraction.Create(5,4),Fraction.Create(5,4), 0),
            Tuple.Create<Fraction,Fraction,int>(Fraction.Create(5,5),Fraction.Create(5,4), -1),
            Tuple.Create<Fraction,Fraction,int>(Fraction.Create(5,5),Fraction.Create(5,6), 1)
        };
    }

    //ADDED more tests including example of how to use data-driven test
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructor()
        {
            Fraction F = Fraction.Create(3, 4);
            Assert.IsInstanceOfType(F, typeof(Fraction));
            Assert.AreEqual(F.denominator, 4);
            Assert.AreEqual(F.numerator, 3);
        }

        [TestMethod]
        public void TestEquality()
        {
            Fraction F = Fraction.Create(3, 4);
            Assert.AreEqual(F, F);
        }

        [TestMethod]
        public void TestUniqueZero()
        {
            Assert.AreEqual(Fraction.Create(0, 121), Fraction.Create(0));
        }


        [TestMethod]
        public void TestNegation()
        {
            Fraction F = Fraction.Create(3, 4);
            Fraction G = Fraction.Negate(Fraction.Negate(Fraction.Create(3, 4)));
            Assert.AreEqual(F, G);
        }

        [TestMethod]
        public void TestCreationFromInteger()
        {
            Fraction F = Fraction.Create(2);
            Assert.AreEqual(F, Fraction.Create(2, 1));
        }

        [TestMethod]
        public void TestAssignFromInteger()
        {
            Fraction F = 2;
            Assert.AreEqual(F, Fraction.Create(2, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(FractionException))]
        public void TestNoZeroDenominators() => Fraction.Create(1, 0);

        [TestMethod]
        public void TestReciprocal()
        {
            Fraction F = Fraction.Create(19, 37);
            Assert.AreEqual(F, Reciprocal(Reciprocal(F)));
        }
        

        [TestMethod]
        public void TestMultiply()
        {
            foreach ((Fraction m1, Fraction m2, Fraction re) in TestData.TestDataForMultiply)
            {
                Assert.AreEqual(m1 * m2, re);
                Assert.AreEqual(Multiply(m1, m2), re);
            }
        }

        [TestMethod]
        public void TestAdd()
        {
            foreach ((Fraction a1, Fraction a2, Fraction re) in TestData.TestDataForAdd)
            {
                Assert.AreEqual(Add(a1, a2), re);
                Assert.AreEqual(a1 + a2, re);
            }
        }

        [TestMethod]
        public void TestCompare()
        {
            foreach ((Fraction f1, Fraction f2, int re) in TestData.TestDataForComparison)
            {
                Assert.AreEqual(f1.CompareTo(f2), re);
            }
        }

        [TestMethod]
        public void TestCompareOverrides()
        {
            foreach ((Fraction f1, Fraction f2, int re) in TestData.TestDataForComparison)
            {
                if (re == 0) Assert.IsTrue(f1 == f2);
                if (re < 0) Assert.IsTrue(f1 < f2);
                if (re > 0) Assert.IsTrue(f1 > f2);
            }
        }

    }
}
