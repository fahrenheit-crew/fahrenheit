.NET debugging requires headers and libraries that are not shipped as part of the .NET SDK:
- `dbgshim.{h|lib|dll}`
- `cor.h`
- `corhdr.h`
- `corerror.h`
- `cordebug.h`
- `metahost.h`
- `mscoree.h`

They are obtained through the [.NET Diagnostics repo](https://github.com/dotnet/diagnostics/) instead.

While the [Microsoft.Diagnostics.DbgShim](https://www.nuget.org/packages/Microsoft.Diagnostics.DbgShim/) package exists,
it ships only the DLL, but not the associated header or import library. We therefore must vendor the remaining items
from a locally built .NET Diagnostics release. This must be updated for every major .NET release.

To build a new one when it's time to replace it, follow the 
[.NET Diagnostics build instructions](https://github.com/dotnet/diagnostics/blob/main/documentation/building/windows-instructions.md).

The current version of Diagnostics used is: `10.0.745401`.

The headers are cherry-picked from the following locations in diagnostics:
- `dbgshim.h`:  `src\dbgshim\dbgshim.h`
- `cor.h`:      `src\shared\inc\cor.h`
- `corhdr.h`:   `src\shared\inc\corhdr.h`
- `corerror.h`: `src\shared\pal\prebuilt\inc\corerror.h`
- `cordebug.h`: `src\shared\pal\prebuilt\inc\cordebug.h`
- `metahost.h`: `src\shared\pal\prebuilt\inc\metahost.h`
- `mscoree.h`:  `src\shared\pal\prebuilt\inc\mscoree.h`
