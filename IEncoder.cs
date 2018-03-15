using System;
using System.Collections.Generic;
using System.Text;

namespace Encoder
{
    public interface IEncoder
    {

        byte[] Encode(byte[] inputData);
    }
}
