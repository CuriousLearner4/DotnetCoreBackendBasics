using Loans.Domain.Applications;
using NUnit.Framework;
using Moq;
using NUnit.Framework.Constraints;
using System;
namespace Loans.Tests
{
    public class LoanApplicationProcessorShould
    {
        [Test]
        public void DeclineLowSalary()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "User1", 22, "Near T-Hub, Hyderabad, Telangana.", 64_999);
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,mockCreditScorer.Object);
            sut.Process(application);
            Assert.That(application.GetIsAccepted(), Is.False);
        }

        [Test]
        public void Accept()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "User1", 22, "Near T-Hub, Hyderabad, Telangana.", 65_000);
            var mockIdentityVerifier = new Mock<IIdentityVerifier>(MockBehavior.Strict);
            mockIdentityVerifier.Setup(x => x.Initialize());
            mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.")).Returns(true);
            #region ForReference
            //var isValid = true;
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",ref It.Ref<IdentityVerificationStatus>.IsAny)).Callback(new ValidateCallback((string applicantName,int applicantAge,string applicantAddress,ref IdentityVerificationStatus status)=>status = new IdentityVerificationStatus(true)));
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",out isValid));
            // mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            #endregion
            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.SetupAllProperties();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            //mockCreditScorer.SetupProperty(x => x.Count);
            #region ForReference
            //var mockScoreValue = new Mock<ScoreValue>();
            //mockScoreValue.Setup(x => x.Score).Returns(300);
            //var mockScoreResult = new Mock<ScoreResult>();
            //mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);
            //mockCreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);
            //mockCreditScorer.Setup(x => x.Score).Returns(300);
            #endregion
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,mockCreditScorer.Object);
            sut.Process(application);
            Assert.That(application.GetIsAccepted(), Is.True);
            Assert.That(mockCreditScorer.Object.Count, Is.EqualTo(1));
        }
        [Test]
        public void DeclineWhenCreditScoreError()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "User1", 22, "Near T-Hub, Hyderabad, Telangana.", 65_000);
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.")).Returns(true);
            #region ForReference
            //var isValid = true;
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",ref It.Ref<IdentityVerificationStatus>.IsAny)).Callback(new ValidateCallback((string applicantName,int applicantAge,string applicantAddress,ref IdentityVerificationStatus status)=>status = new IdentityVerificationStatus(true)));
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",out isValid));
            // mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            #endregion
            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.SetupAllProperties();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            //mockCreditScorer.Setup(x => x.CalculateScore(It.IsAny<string>(), It.IsAny<string>())).Throws<InvalidOperationException>(); 
            mockCreditScorer.Setup(x => x.CalculateScore(It.IsAny<string>(), It.IsAny<string>())).Throws(new InvalidOperationException("Test Exception"));
            //mockCreditScorer.SetupProperty(x => x.Count);
            #region ForReference
            //var mockScoreValue = new Mock<ScoreValue>();
            //mockScoreValue.Setup(x => x.Score).Returns(300);
            //var mockScoreResult = new Mock<ScoreResult>();
            //mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);
            //mockCreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);
            //mockCreditScorer.Setup(x => x.Score).Returns(300);
            #endregion
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,mockCreditScorer.Object);
            sut.Process(application);
            Assert.That(application.GetIsAccepted(), Is.False);
        }
        [Test]
        public void VerifyGetSet()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "User1", 22, "Near T-Hub, Hyderabad, Telangana.", 65_000);
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            #region ForReference
            //var isValid = true;
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",ref It.Ref<IdentityVerificationStatus>.IsAny)).Callback(new ValidateCallback((string applicantName,int applicantAge,string applicantAddress,ref IdentityVerificationStatus status)=>status = new IdentityVerificationStatus(true)));
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",out isValid));
            // mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            #endregion
            mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.")).Returns(true);
            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.SetupAllProperties();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            //mockCreditScorer.SetupProperty(x => x.Count);
            #region ForReference
            //var mockScoreValue = new Mock<ScoreValue>();
            //mockScoreValue.Setup(x => x.Score).Returns(300);
            //var mockScoreResult = new Mock<ScoreResult>();
            //mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);
            //mockCreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);
            //mockCreditScorer.Setup(x => x.Score).Returns(300);
            #endregion
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,mockCreditScorer.Object);
            sut.Process(application);
            mockCreditScorer.VerifyGet(x => x.ScoreResult.ScoreValue.Score,Times.Once);
            mockCreditScorer.VerifySet(x => x.Count = It.IsAny<int>(),Times.Once);
            Assert.That(application.GetIsAccepted(), Is.True);
            Assert.That(mockCreditScorer.Object.Count, Is.EqualTo(1));
        }
        [Test]
        public void IntializeIdentityVerifier()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "User1", 22, "Near T-Hub, Hyderabad, Telangana.", 65_000);
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.")).Returns(true);
            #region ForReference
            //var isValid = true;
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",ref It.Ref<IdentityVerificationStatus>.IsAny)).Callback(new ValidateCallback((string applicantName,int applicantAge,string applicantAddress,ref IdentityVerificationStatus status)=>status = new IdentityVerificationStatus(true)));
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",out isValid));
            // mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            #endregion
            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.SetupAllProperties();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            #region ForReference
            //mockCreditScorer.SetupProperty(x => x.Count);
            //var mockScoreValue = new Mock<ScoreValue>();
            //mockScoreValue.Setup(x => x.Score).Returns(300);
            //var mockScoreResult = new Mock<ScoreResult>();
            //mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);
            //mockCreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);
            //mockCreditScorer.Setup(x => x.Score).Returns(300);
            #endregion
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,mockCreditScorer.Object);
            sut.Process(application);
            mockIdentityVerifier.Verify(x => x.Initialize());
            mockIdentityVerifier.Verify(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()));
            mockIdentityVerifier.VerifyNoOtherCalls();
        }
        [Test]
        public void CalculateScoreVerifier()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "User1", 22, "Near T-Hub, Hyderabad, Telangana.", 65_000);
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.")).Returns(true);
            #region ForReference
            //var isValid = true;
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",ref It.Ref<IdentityVerificationStatus>.IsAny)).Callback(new ValidateCallback((string applicantName,int applicantAge,string applicantAddress,ref IdentityVerificationStatus status)=>status = new IdentityVerificationStatus(true)));
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",out isValid));
            // mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            #endregion
            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.SetupAllProperties();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            //mockCreditScorer.SetupProperty(x => x.Count);
            #region ForReference
            //var mockScoreValue = new Mock<ScoreValue>();
            //mockScoreValue.Setup(x => x.Score).Returns(300);
            //var mockScoreResult = new Mock<ScoreResult>();
            //mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);
            //mockCreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);
            //mockCreditScorer.Setup(x => x.Score).Returns(300);
            #endregion
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,mockCreditScorer.Object);
            sut.Process(application);
            mockCreditScorer.Verify(x => x.CalculateScore("User1", "Near T-Hub, Hyderabad, Telangana."));
        }
        [Test]
        public void CalculateScoreCallsVerifier()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "User1", 22, "Near T-Hub, Hyderabad, Telangana.", 65_000);
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.")).Returns(true);
            #region ForReference
            //var isValid = true;
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",ref It.Ref<IdentityVerificationStatus>.IsAny)).Callback(new ValidateCallback((string applicantName,int applicantAge,string applicantAddress,ref IdentityVerificationStatus status)=>status = new IdentityVerificationStatus(true)));
            //mockIdentityVerifier.Setup(x => x.Validate("User1", 22, "Near T-Hub, Hyderabad, Telangana.",out isValid));
            // mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            #endregion
            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.SetupAllProperties();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            //mockCreditScorer.SetupProperty(x => x.Count);
            #region ForReference
            //var mockScoreValue = new Mock<ScoreValue>();
            //mockScoreValue.Setup(x => x.Score).Returns(300);
            //var mockScoreResult = new Mock<ScoreResult>();
            //mockScoreResult.Setup(x => x.ScoreValue).Returns(mockScoreValue.Object);
            //mockCreditScorer.Setup(x => x.ScoreResult).Returns(mockScoreResult.Object);
            //mockCreditScorer.Setup(x => x.Score).Returns(300);
            #endregion
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,mockCreditScorer.Object);
            sut.Process(application);
            mockCreditScorer.Verify(x => x.CalculateScore("User1", "Near T-Hub, Hyderabad, Telangana."),Times.Once);
        }

        [Test]
        public void NullReturnExample()
        {
            var mock = new Mock<INullExample>();
            mock.Setup(x => x.SomeMethod()).Returns<string>(null);
            var mockReturnValue = mock.Object.SomeMethod();
            Assert.That(mockReturnValue, Is.Null);
        }
        delegate void ValidateCallback(string applicantName, int applicantAge, string applicantAddress, ref IdentityVerificationStatus status);
    }

    public interface INullExample
    {
        string SomeMethod();
    }
}
