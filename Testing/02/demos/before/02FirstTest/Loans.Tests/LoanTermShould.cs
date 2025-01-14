﻿using System;
using System.Collections.Generic;
using Loans.Domain.Applications;
using NUnit.Framework;
namespace Loans.Tests
{
    [TestFixture]
    public class LoanTermShould
    {
        [Test]
        [Ignore("Feature in progress")]
        public void ReturnTermInMonths()
        {
            var sut = new LoanTerm(1);
            Assert.That(sut.ToMonths(), Is.EqualTo(12));
        }
        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);
            Assert.That(sut.Years, Is.EqualTo(1));
        }
        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);
            Assert.That(a, Is.EqualTo(b));
        }
        [Test]
        public void RespectValueInEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);
            Assert.That(a,Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);
            Assert.That(a, Is.SameAs(b));
            Assert.That(a, Is.Not.SameAs(c));

            var x = new List<string> { "A", "B" };
            var y = x;
            var z = new List<string> { "A", "B" };
            Assert.That(y, Is.SameAs(x));
            Assert.That(z, !Is.SameAs(x));
        }

        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;
            Assert.That(a, Is.EqualTo(0.33).Within(0.04));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void NotAllowZeroYears()
        {
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("years"));

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>().With.Matches<ArgumentOutOfRangeException>(ex => ex.ParamName == "years"));
        }
    }
}
