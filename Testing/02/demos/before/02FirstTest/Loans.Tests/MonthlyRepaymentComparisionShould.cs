﻿using System;
using System.Collections.Generic;
using System.Text;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    public class MonthlyRepaymentComparisionShould
    {
        [Test]
        public void RespectValueEquality()
        {
            var a = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            Assert.That(a, Is.EqualTo(b));
        }
        [Test]
        public void RespectValueInEquality()
        {
            var a = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 42.42m, 23.22m);
            Assert.That(a, Is.Not.EqualTo(b));
        }
    }
}
