using ArgumentParser;

using NUnit.Framework;

namespace ArgumentParser_uTest
{
    public class CLIArgumentParser_uTest
    {
        [TestCase("testistan", "234.432")]
        public void Test1(string expectedName, string expectedVersion)
        {
            // Arrange
            var argumentParser = new CLIArgumentParser<TestingArgument>();
            string[] argumentsAray = new[]
            {
                "name=" + expectedName,
                "version=" + expectedVersion
            };

            // Act
            var exected = argumentParser.Parse(argumentsAray);

            // Assert
            Assert.AreEqual(expectedName, exected.name);
            Assert.AreEqual(expectedVersion, exected.version);
        }
    }

    public class TestingArgument
    {
        public string name;
        public string version;
    }
}