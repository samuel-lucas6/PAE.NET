/*
    PAE.NET: A .NET implementation of Pre-Authentication Encoding.
    Copyright (c) 2022 Samuel Lucas
    
    Permission is hereby granted, free of charge, to any person obtaining a copy of
    this software and associated documentation files (the "Software"), to deal in
    the Software without restriction, including without limitation the rights to
    use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
    the Software, and to permit persons to whom the Software is furnished to do so,
    subject to the following conditions:
    
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

namespace PAEDotNet;

public static class PAE
{
    private const int UInt64Length = 8;

    public static byte[] Encode(params byte[][] inputs)
    {
        var output = new byte[UInt64Length + inputs.Sum(input => input.Length) + UInt64Length * inputs.Length];
        Array.Copy(LE64((ulong)inputs.Length), output, UInt64Length);
        int offset = UInt64Length;
        foreach (var input in inputs) {
            Array.Copy(LE64((ulong)input.Length), sourceIndex: 0, output, destinationIndex: offset, UInt64Length);
            Array.Copy(input, sourceIndex: 0, output, destinationIndex: offset += UInt64Length, input.Length);
            offset += input.Length;
        }
        return output;
    }

    private static byte[] LE64(ulong value)
    {
        var bytes = BitConverter.GetBytes(value);
        if (!BitConverter.IsLittleEndian) {
            Array.Reverse(bytes);
        }
        return bytes;
    }
}