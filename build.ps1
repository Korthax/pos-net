[CmdletBinding()]
param(
    [Parameter()]
    [string]$Version = "1.0.0"
)

dotnet test
dotnet pack -o artifacts /p:Version=$Version
