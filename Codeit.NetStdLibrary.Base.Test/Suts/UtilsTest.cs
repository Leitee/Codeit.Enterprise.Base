using FluentAssertions;
using Codeit.NetStdLibrary.Base.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codeit.NetStdLibrary.Base.Test.DataAccessSuts
{
    public class UtilsTest
    {
        [Fact(DisplayName = "Should returns my today's age equal 35 or greater")]
        public void GetEdadByBirthday()
        {
            //Arrange
            var birthDate = new DateTime(1985, 11, 23);

            //Act
            var age = CodeitUtils.GetAgeFromBirthDate(birthDate);

            //Assert
            age.Should().BeGreaterOrEqualTo(35);
        }

        [Fact(DisplayName = "Should return the last element and removes it")]
        public void GetElementByIndex()
        {

            //Arrange
            var list = new List<int> { 5, 8, 15 };
            var index = list.Count - 1;

            //Act
            var element = CodeitUtils.PopAt(list, index);

            //Assert
            element.Should().Be(15);
        }
    }
}
