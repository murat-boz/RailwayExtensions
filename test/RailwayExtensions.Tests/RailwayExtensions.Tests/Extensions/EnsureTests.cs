using FluentAssertions;
using RailwayExtensions.Extensions;

namespace RailwayExtensions.Tests.Extensions
{
    public class EnsureTests
    {
        [Fact]
        public void Ensure_Source_Result_Is_Failure_Predicate_Do_Not_Invoked_Expect_Is_Result_Failure()
        {
            //Arrange
            Result sut = Result.Failure("Error");

            //Act
            Result result = sut.Ensure(() => true, string.Empty);

            //Assert
            result.Should().Be(sut);
        }
    }
}
