using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using DealingWithApiChanges.MyApi;
using Xunit;

[assembly: UseReporter(typeof(XUnit2Reporter))]
namespace DealingWithApiChanges.Tests.ApiApprovals
{
    public class ApprovalTests
    {
        [Fact]
        public void TestApi()
        {
            var publicApiSnapshot = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Person).Assembly);
            Approvals.Verify(publicApiSnapshot);
        }
    }
}
