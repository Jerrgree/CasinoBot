param(
    [Parameter(Mandatory=$True, Position=0, ValueFromPipeline=$false)]
    [System.String]
    $Name
)

if ($Name -eq "") {
    Write-Host "Missing required parameter name"
}
else {
    dotnet ef migrations add $Name

    dotnet ef database update
}