# Substantially copied from TerraFX.Interop.Windows.
# Original copyright © Tanner Gooding and Contributors - see THIRD-PARTY-NOTICES in repository root!

# ---
# THIS SCRIPT REQUIRES POWERSHELL 7 TO EXECUTE
# ---

function Generate() {
    $generateRspFiles = Get-ChildItem -Path $PSScriptRoot -Recurse -Filter "sgen_f*.rsp"
    
    $generateRspFiles | ForEach-Object {
        Push-Location -Path $_.DirectoryName
        & ClangSharpPInvokeGenerator "@$($_.Name)"
        Pop-Location
    }
}

try {
    $Root        = Join-Path -Path $PSScriptRoot -ChildPath ".."
    $RootFFX     = Join-Path -Path $Root         -ChildPath "src" "core" "ffx"
    $RootFFX2    = Join-Path -Path $Root         -ChildPath "src" "core" "ffx2"
    $RootFFXIds  = Join-Path -Path $Root         -ChildPath "src" "core" "ffx" "ids"
    $RootFFX2Ids = Join-Path -Path $Root         -ChildPath "src" "core" "ffx2" "ids"
      
    Generate
    
    # ClangSharpPInvokeGenerator always emits IDs in the same place as the struct.
    # In Fahrenheit we keep them separate. We just move them after the fact.
    $FFXIds      = Join-Path -Path $RootFFX  -ChildPath "*Id.cs"
    $FFX2Ids     = Join-Path -Path $RootFFX2 -ChildPath "*Id.cs"
    
    Get-ChildItem -Path $FFXIds  -Recurse | Move-Item -Force -Destination $RootFFXIds
    Get-ChildItem -Path $FFX2Ids -Recurse | Move-Item -Force -Destination $RootFFX2Ids
}
catch {
    Write-Host -Object $_
    Write-Host -Object $_.Exception
    Write-Host -Object $_.ScriptStackTrace
    exit 1
}