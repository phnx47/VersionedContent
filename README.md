# VersionedContent

[![NuGet](https://img.shields.io/nuget/v/VersionedContent.svg)](https://www.nuget.org/packages/VersionedContent) [![License MIT](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT) 

Use the current timestamp as the version for static files. Cached for one day

```
<!DOCTYPE html>
<html>
<head>
    <title>Test Page</title>
</head>
<body>
<div>
    Content
</div>
    <script src="@Url.VersionedContent("~/Scripts/main.js")"></script>
</body>
</html>
```

Result:

```
<!DOCTYPE html>
<html>
<head>
    <title>Test Page</title>
</head>
<body>
<div>
    Content
</div>
    <script src="/Scripts/main.js?v=1492371642"></script>
</body>
</html>
```
