# git-helper.ps1
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

Write-Host "Repository Summary"
Write-Host "------------------"
$branch = git rev-parse --abbrev-ref HEAD
$commit = git rev-parse HEAD
$root = git rev-parse --show-toplevel
Write-Host "Branch: $branch"
Write-Host "Commit: $commit"
Write-Host "Root:   $root"
Write-Host ""

Write-Host "Project Reference Map"
Write-Host "---------------------"

# Find all .csproj files
$projects = Get-ChildItem -Path $root -Recurse -Filter *.csproj

foreach ($proj in $projects) {
    Write-Host "`nProject:" ($proj.FullName -replace [regex]::Escape($root + "\"), "")
    $refs = @(Select-String -Path $proj.FullName -Pattern "<ProjectReference Include=" | ForEach-Object {
        ($_ -split '"')[1]
    })
    if ($refs.Count -gt 0) {
        foreach ($ref in $refs) {
            Write-Host "  → $ref"
        }
    }
    else {
        Write-Host "  (No project references)"
    }
}

Write-Host "`nChanged Files (vs. HEAD)"
Write-Host "--------------------------"
git diff --name-only

Write-Host "`nTracked Files (first 20)"
Write-Host "--------------------------"
$files = git ls-files
$files | Select-Object -First 20 | ForEach-Object { Write-Host $_ }
Write-Host "... ($($files.Count) total tracked files)"
Write-Host ""
Write-Host "Done."
