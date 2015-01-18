LoneWolfMigration
=================

LoneWolfMigration converts the Project Aon Internet Editions to the format used by the [LoneWolf Android app](https://github.com/hlaueriksson/LoneWolf/).

Input: HTML (the standard, full-featured, multi-page edition)

Output: HTML, Java

This repo only contains the code that does the conversion.
The content of the books are published and hosted by [Project Aon](http://www.projectaon.org/)

Requirements
------------

Visual Studio 2012

- http://www.visualstudio.com/

NuGet

- https://www.nuget.org/

Content
-------

This project assumes that input and output folders are located at:

- ..\LoneWolfDatabase\ `book` \input\en\
- ..\LoneWolfDatabase\ `book` \output\en\

...where `book` is the given book identifier, e.g. **01fftd**

Download the standard zip archives (full-featured, multi-page edition) from http://www.projectaon.org/ and unpack them in input folders.

Run
---

Book 01 - Flight from the Dark

- \dev\LoneWolf.Migration\book01.bat
