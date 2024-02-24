using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotePadClone.ViewModels;
using NUnit.Framework;

namespace NotePadCloneTests.ViewModels
{
    [TestFixture]
    public class MainWindowVmTests
    {
        [TestCase("", 0, 0, 0)]
        [TestCase("a", 1, 1, 0)]
        [TestCase("\r\n", 0, 2, 0)] // Put '\n' her if running tests on Unix system
        public void TextContent_UpdatesDocumentInfoCorrectly_IfValueChanges(string textStub, int expectedCharacters, int expectedLines, int expectedSize)
        {

        }
    }
}
