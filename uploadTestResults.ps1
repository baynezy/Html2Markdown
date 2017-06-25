Write-Host "- About to upload test results"

$wc = New-Object "System.Net.WebClient"
$wc.UploadFile("https://ci.appveyor.com/api/testresults/mstest/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\test\Html2Markdown.Test\TestResults\results.trx))

Write-Host "- Results uploaded"