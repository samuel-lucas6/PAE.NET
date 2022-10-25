using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PAEDotNet;

namespace PAEDotNetTests;

[TestClass]
public class PAETests
{
    [TestMethod]
    [DataRow("", null, "01000000000000000000000000000000")]
    [DataRow("test", null, "0100000000000000040000000000000074657374")]
    [DataRow("test", "ing", "02000000000000000400000000000000746573740300000000000000696e67")]
    public void TestVectors(string one, string? two, string expected)
    {
        byte[] input1 = Encoding.UTF8.GetBytes(one);
        byte[] input2 = two != null ? Encoding.UTF8.GetBytes(two) : Array.Empty<byte>();
        byte[] output = input2.Length == 0 ? PAE.Encode(input1) : PAE.Encode(input1, input2);
        Assert.AreEqual(expected, Convert.ToHexString(output).ToLower());
    }
}