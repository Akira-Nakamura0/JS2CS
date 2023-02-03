using System;

namespace JS2CS.Net
{
  internal enum InputChunkState
  {
    None,
    Data,
    DataEnded,
    Trailer,
    End
  }
}
