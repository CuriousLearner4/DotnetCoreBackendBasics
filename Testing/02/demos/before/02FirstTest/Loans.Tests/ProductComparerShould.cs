using System;
using System.Collections.Generic;
using System.Text;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    [ProductComparison]
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            products = new List<LoanProduct>
            {
                new LoanProduct(1,"a",1),
                new LoanProduct(2,"b",2),
                new LoanProduct(3,"c",3)

            };
        }

        [SetUp]
        public void Setup()
        {
            

            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }

        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
           
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            Assert.That(comparisons, Has.Exactly(3).Items);
        }
        [TearDown]
        public void Teardown()
        {
            //after every test
            //to dispose sut
        }
        
        [Test]
        public void NotReturnDuplicateComparisons()
        {
           
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            Assert.That(comparisons, Is.Unique);
        } 
        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);
            Assert.That(comparisons, Does.Contain(expectedProduct));
        }
        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        { 
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);
            Assert.That(comparisons, Has.Exactly(1).Property("ProductName").EqualTo("a").And.Property("InterestRate").EqualTo(1).And.Property("MonthlyRepayment").GreaterThan(0));
            Assert.That(comparisons, Has.Exactly(1).Matches<MonthlyRepaymentComparison>(item => item.ProductName == "a" && item.InterestRate == 1 && item.MonthlyRepayment > 0));
        }
        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues_Constraint()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);
            Assert.That(comparisons, Has.Exactly(1).Matches(new MonthlyRepaymentGreaterThanZeroConstraint("a",1)));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //runs after last test in this test class executes
            //e.g. disposing of shared expensive setup performed in OneTimeSetUp
            //products.Dispose(); e.g. if products implemented IDisposable
        }
    }
}
