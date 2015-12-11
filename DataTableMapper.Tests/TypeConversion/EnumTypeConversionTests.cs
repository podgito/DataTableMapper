using DataTableMapper.TypeConversion;
using NUnit.Framework;
using System;
using System.Collections;

namespace DataTableMapper.Tests.TypeConversion
{
    [TestFixture]
    public class EnumTypeConversionTests
    {
        private ITypeConverter _enumTypConverter;

        [SetUp]
        public void Setup()
        {
            _enumTypConverter = new EnumTypeConverter();
        }

        [Test, TestCaseSource(typeof(TestCaseMatchesDataSource))]
        public bool OnlyMatchOnEnumTypes(Type type)
        {
            return _enumTypConverter.IsMatch(typeof(object), type);
        }

        [Test, TestCaseSource(typeof(TestCaseValuesDataSource))]
        public Genres ConvertToGenreEnum(object value)
        {
            return (Genres)_enumTypConverter.Convert(value, typeof(Genres));
        }

        public enum Genres
        {
            Horror,
            Comedy,
            Thriller
        }

        private class TestCaseMatchesDataSource : IEnumerable
        {
            private static IEnumerable GetData()
            {
                yield return new TestCaseData(typeof(Genres)).Returns(true);
                yield return new TestCaseData(typeof(int)).Returns(false);
                yield return new TestCaseData(typeof(object)).Returns(false);
                yield return new TestCaseData(typeof(string)).Returns(false);
                yield return new TestCaseData(typeof(List)).Returns(false);
            }

            public IEnumerator GetEnumerator()
            {
                return GetData().GetEnumerator();
            }
        }

        private class TestCaseValuesDataSource : IEnumerable
        {
            private static IEnumerable GetData()
            {
                yield return new TestCaseData("Horror").Returns(Genres.Horror);
                yield return new TestCaseData(0).Returns(Genres.Horror);
                yield return new TestCaseData("0").Returns(Genres.Horror);
            }

            public IEnumerator GetEnumerator()
            {
                return GetData().GetEnumerator();
            }
        }
    }
}