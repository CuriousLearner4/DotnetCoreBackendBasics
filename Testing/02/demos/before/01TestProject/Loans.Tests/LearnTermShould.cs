using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    public class LearnTermShould
    {
        [Test]
        public void ReturnTermInMonth()
        {
            var sut = new LoanTerm(343);
            Assert.That(sut.ToMonths(), Is.EqualTo(12*343));
        }
    }
}
