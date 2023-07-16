using System;
using System.Collections.Generic;
using Xunit;
using OpenCloseDays.Extensions;

namespace OpenCloseDays.Tests
{
    public class ExtensionsTests
    {
        [Theory]
        [MemberData(nameof(ParseDatesData))]
        public void ParseAsDatesList_ShouldParse(string input_datesString, DateTime[] expected_parsedDates)
        {
            Assert.Equal(expected_parsedDates, input_datesString.ParseAsDatesList());
        }

        private static DateTime[] ExpectedDatesList1 = new[] { new DateTime(2022, 11, 11), new DateTime(2022, 11, 12) };
        public static IEnumerable<object[]> ParseDatesData =>
            new List<object[]>
            {
                    new object[] { "2022-11-11,2022-11-12", ExpectedDatesList1 },
                    new object[] { "2022-11-11;2022-11-12", ExpectedDatesList1 },
                    new object[] { "2022-11-11 ; 2022-11-12", ExpectedDatesList1 },
                    new object[] { "2022-11-11 , 2022-11-12", ExpectedDatesList1 },
                    new object[] { "2022-11-11,xxx,2022-11-12", ExpectedDatesList1 },
                    new object[] { "2022-11-15", new[] { new DateTime(2022, 11, 15) } },
                    new object[] { "2022-15-11", Array.Empty<DateTime>() },
                    new object[] { "", Array.Empty<DateTime>() },
            };

        [Theory]
        [InlineData("", 0f)]
        [InlineData("0", 0f)]
        [InlineData("0.5", 0.5f)]
        [InlineData("0,5", 0.5f)]
        [InlineData("0,5,0", 0)]
        [InlineData("5.", 5f)]
        [InlineData(".15", 0.15f)]
        public void ParseAsFloat_ShouldParse(string input_string, float expectedFloat)
        {
            Assert.Equal(expectedFloat, input_string.ParseAsFloat());
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("0", true)]
        [InlineData("0.5", true)]
        [InlineData("0,5", true)]
        [InlineData("0,5,0", false)]
        [InlineData("5.", true)]
        [InlineData(".15", true)]
        public void CanParseAsFloat_ShouldParse(string input_string, bool expected_canParse)
        {
            Assert.Equal(expected_canParse, input_string.CanParseAsFloat());
        }


        [Theory]
        [InlineData(0, "0")]
        [InlineData(0.5, "0.5")]
        public void AsFloatInvariant_ShouldReturn(float input_float, string expected_formatted)
        {
            Assert.Equal(expected_formatted, input_float.AsFloatInvariant());
        }


        [Theory]
        [InlineData(0, "0h")]
        [InlineData(0.5, "0h30")]
        [InlineData(1.25, "1h15")]
        [InlineData(1.5, "1h30")]
        [InlineData(35, "35h")]
        public void ToHoursMinutes_ShouldReturn(float input_float, string expected_formatted)
        {
            Assert.Equal(expected_formatted, input_float.ToHoursMinutes());
        }
    }
}
