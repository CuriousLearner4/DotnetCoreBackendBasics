using System;
using System.Collections.Generic;
using System.Text;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanRepaymentClassShould
    {

        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        [TestCase(500_000, 10, 30, 4387.86)]
        public void CalculateCorrectMonthlyRepaymentWihtoutReturn(decimal principal, decimal interestRate, int termInYears, decimal expectedValue)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyRepayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
            Assert.That(monthlyRepayment, Is.EqualTo(expectedValue));
        }
        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData),"TestCases")]
        public void CalculateCorrectMonthlyRepaymentWihtoutReturn_centralized(decimal principal, decimal interestRate, int termInYears, decimal expectedValue)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyRepayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
            Assert.That(monthlyRepayment, Is.EqualTo(expectedValue));
        }
        
        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentDataCSV), "GetTestCases", new object[] {"data.csv"})]
        public void CalculateCorrectMonthlyRepaymentWihtoutReturn_csv(decimal principal, decimal interestRate, int termInYears, decimal expectedValue)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyRepayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
            Assert.That(monthlyRepayment, Is.EqualTo(expectedValue));
        }


        [Test]
        [TestCase(200_000,6.5,30,ExpectedResult = 1264.14)]
        [TestCase(200_000,10, 30,ExpectedResult = 1755.14)]
        [TestCase(500_000,10, 30,ExpectedResult = 4387.86)]
        public decimal CalculateCorrectMonthlyRepayment(decimal principal,decimal interestRate,int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataReturn),"TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_Centralized(decimal principal, decimal interestRate, int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test]
        public void CalculateCorrectMontlyRepayment_Combinatorial([Values(100_000,200_000,500_000)]decimal principal, [Values(6.5,10,20)]decimal interestRate, [Values(10,20,30)]int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyRepayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        //[TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        //[TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        //[TestCase(500_000, 10, 30, ExpectedResult = 4387.86)]
        [Test]
        [Sequential]
        public void CalculateCorrectMontlyRepayment_Sequential([Values(200_000,200_000,500_000)]decimal principal, [Values(6.5,10,10)]decimal interestRate, [Values(30,30,30)]int termInYears, [Values(1264.14,1755.14,4387.86)] decimal expectedRepayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyRepayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
            Assert.That(monthlyRepayment, Is.EqualTo(expectedRepayment));
        }

        [Test]
        public void CalculateCorrectMontlyRepayment_Range([Range(50_000, 1_000_000, 50_000)] decimal principal, [Range(0.5, 20.00, 0.5)] decimal interestRate, [Values(10, 20, 30)] int termInYears)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyRepayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }


    }
}
