using FluentAssertions;
using System;
using Xunit;

namespace Cnab.Test.Unit
{
    public class StringExtension
    {

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("133")]
        [InlineData("1444")]
        [InlineData("16555")]
        [InlineData("166666")]
        [InlineData("1777777")]
        [InlineData("2147483647")]
        public void ParseIntShouldReturnIntWhenValueIsValid(string value)
        {
            // Act
            var result = Domain.Shared.ParseExtension.ParseInt(value);

            // Assert
            result.Should().Match(x => x.Equals(result));
        }

        [Theory]
        [InlineData("3147483647")]
        public void ParseIntShouldThrowExceptionWhenValueExceedTheLimitOfInteger(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseInt(value);

            // Assert
            act.Should().Throw<OverflowException>();
        }

        [Theory]
        [InlineData("2,00")]
        [InlineData("9.12")]
        [InlineData("12$")]
        public void ParseIntShouldThrowFormatExceptionWhenValueIsInvalid(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseInt(value);

            // Assert
            act.Should().Throw<FormatException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ParseIntShouldReturnExceptionWhenValueIsInvalid(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseInt(value);

            // Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void ParseHourShouldReturnTimeSpanWhenValueIsValid()
        {
            // Arrange
            var value = "210836";

            // Act
            var result = Domain.Shared.ParseExtension.ParseHour(value);

            // Assert
            result.GetType().Should().Be(typeof(TimeSpan));
        }

        [Theory]
        [InlineData("2,31288")]
        [InlineData("131$211")]
        [InlineData("#31$211")]
        [InlineData("#310(211 ")]
        public void ParseHourShouldThrowFormatExceptionWhenValueIsInvalid(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseHour(value);

            // Assert
            act.Should().Throw<FormatException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]

        public void ParseHourShouldThrowExceptionWhenValueIsInvalid(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseHour(value);

            // Assert
            act.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("20220804")]
        public void ParseDateShouldReturnDateWhenValueIsValid(string value)
        {
            // Act
            var result = Domain.Shared.ParseExtension.ParseDate(value);

            // Assert
            result.GetType().Should().Be(typeof(string));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ParseDateShouldThrowExceptionWhenValueIsNullOrEmpty(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseDate(value);

            // Assert
            act.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("AABBESQER")]
        public void ParseDateShouldThrowExceptionWhenValueIsInvalid(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseDate(value);

            // Assert
            act.Should().Throw<FormatException>();
        }

        [Theory]
        [InlineData("10")]
        [InlineData("212")]
        [InlineData("1800")]
        [InlineData("55896")]
        public void ParseToDecimalShouldReturnDecimalValueWhenValueIsValid(string value)
        {
            // Act
            var result = Domain.Shared.ParseExtension.ParseToDecimal(value);

            // Assert
            result.GetType().Should().Be(typeof(decimal));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]

        public void ParseToDecimalShouldThrowExceptionWhenValueIsNullOrEmpty(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseToDecimal(value);

            // Assert
            act.Should().Throw<Exception>();
        }


        [Theory]
        [InlineData("R$8.000,00")]
        [InlineData("10$")]
        [InlineData("11   ,")]
        public void ParseToDecimalShouldThrowException(string value)
        {
            // Act
            Action act = () => Domain.Shared.ParseExtension.ParseToDecimal(value);

            // Assert
            act.Should().ThrowExactly<FormatException>();
        }

        [Theory]
        [InlineData(11, 24, 36)]
        [InlineData(00, 12, 48)]
        [InlineData(13, 21, 18)]
        public void ConvertTimeFromUtcShouldReturnExactDateTime(int hour, int minute, int second)
        {
            // Arrange
            var dateTime = new DateTime(2022, 04, 22, hour, minute, second);
            // Act
            var result = Domain.Shared.ParseExtension.ConvertTimeFromUtc(dateTime);

            // Assert
            result.Should().Be(dateTime.AddHours(-3));

        }
    }
}
