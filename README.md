# GreenUtil

Library made from coders to coders with some utilities used in GreenConcept projects.

[![Build status](https://ci.appveyor.com/api/projects/status/qqdsnxdp9oq47w8n/branch/master?svg=true)](https://ci.appveyor.com/project/leandroltavares/greenutil/branch/master) [![codecov](https://codecov.io/gh/leandroltavares/GreenUtil/branch/master/graph/badge.svg)](https://codecov.io/gh/leandroltavares/GreenUtil)

## Overview

Highly tested .NET library with some important and useful utilities function widely used in GreenConcept projects. The library contains several extension methods in order to simplify the usage of common programming routines.
The utilities are divided into namespaces:

- Collections: Logics related to ```IEnumerable<T>``` and other collections like ```Dictionary```. Some main operations are ```DistinctBy```, ```Shuffle```, ```Contains``` and ```ToDictionaryList```;
- Crypto: Logic related to Hashing (MD5 and SHA1), Symmetric and Asymmetric cryptography;
- Data: Logic related to data (XML, Json, Cache, Mail, etc...);
- Enumeration: Logic related to ```Enums```;
- Imaging: Logic related to ```images``` and ```bitmaps```, including high speed bits manipulation, ```Scale```, ```Crop```, ```FixOrientation``` (EXIF), get images ```MimeTypes``` and ```base64``` conversions  
- IO: Logic related to files and directories, including Regex search, ```GetEncodingFromBOM```, detect file availability and others;
- Linq: Logic related to ```Linq``` and ```Expression```, inclduing canonical ```True``` or ```False``` expressions and logical operators ```And``` and ```Or``` to compose complex operations;
- Performance: Simple stopwatch to measure ```Actions``` execution time;
- String: Logic related to ```String``` processing including logic for removing numbers, diacritics, alphanumeric, conversion from and to ```base64``` and ```hex```;
It also include logics for brazilian commom identifiers ([**CNPJ (portuguese)**](https://pt.wikipedia.org/wiki/Cadastro_Nacional_da_Pessoa_Jur%C3%ADdica), [**CPF (portuguese)**](https://pt.wikipedia.org/wiki/Cadastro_de_pessoas_f%C3%ADsicas), 
[**RG (portuguese)**](https://pt.wikipedia.org/wiki/C%C3%A9dula_de_identidade) and [**Boleto (portuguese)**](https://pt.wikipedia.org/wiki/Boleto_banc%C3%A1rio));
- Web: Logic related to Web. Methods for calling WebAPIs (GET and POST) and parsing the results;
- Workflow: Logic for automata transition validations;

## Installation

GreenUtil is available as a NuGet package. You can install it using the NuGet Package Console window:

```
PM> Install-Package GreenUtil
```

## Dependencies
The library is designed to depend as little as possible to other libraries. With that in mind, it depends only on [**Newtonsoft.Json**](https://www.nuget.org/packages/Newtonsoft.Json/) and [**Sytem.Drawing.Commom**](https://www.nuget.org/packages/System.Drawing.Common/)
