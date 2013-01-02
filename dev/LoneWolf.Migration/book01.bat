LoneWolf.Migration\bin\Debug\lwm.exe -files ..\..\..\LoneWolfDatabase\01fftd\input\en\ ..\..\..\LoneWolfDatabase\01fftd\output\en\
LoneWolf.Migration\bin\Debug\lwm.exe -code ..\..\..\LoneWolfDatabase\01fftd\output\en\
LoneWolf.Migration\bin\Debug\lwm.exe -pretzel ..\..\..\LoneWolfDatabase\01fftd\output\en\

xcopy .\LoneWolf.Migration.Pretzel\src\*.* ..\..\..\LoneWolfDatabase\01fftd\output\en\ /s /y

LoneWolf.Migration.Pretzel\Pretzel.exe bake ..\..\..\LoneWolfDatabase\01fftd\output\en\
