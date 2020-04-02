using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SonarJsConfig;

namespace SonarJSPocTests
{
    [TestClass]
    public class EslintRulesProviderTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetJavascriptRules()
        {
            var actual = EslintRulesProvider.GetJavaScriptRuleKeys();

            actual.Should().NotBeEmpty();

            DumpKeys(actual);
        }

        [TestMethod]
        public void GetTypeScriptRules()
        {
            var actual = EslintRulesProvider.GetTypeScriptRuleKeys();

            actual.Should().NotBeEmpty();

            DumpKeys(actual);
        }

        private void DumpKeys(IEnumerable<string> ruleKeys)
        {
            foreach (var item in ruleKeys)
            {
                TestContext.WriteLine(item);
            }
        }
    }
}
