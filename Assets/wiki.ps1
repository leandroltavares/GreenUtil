Param([string]$token)

$mdFolder = C:\projects\wiki2

Write-Host "Start publishing documentation to wiki"
git config --global credential.helper store
Add-Content "$HOME\.git-credentials" "https://$($token):x-oauth-basic@github.com`n"
git config --global user.email "leandro.ltavares@gmail.com"
git config --global user.name "Leandro Luciani Tavares"
Remove-Item $mdFolder -Force -Recurse -ErrorAction SilentlyContinue
md $mdFolder
cd $mdFolder
git clone --branch=master https://github.com/leandroltavares/GreenUtil.wiki.git 
cd GreenUtil.wiki
git status
C:\projects\greenutil\Assets\MarkdownGenerator.exe "C:\projects\greenutil\GreenUtil\bin\Release\net471\GreenUtil.dll" "$($mdFolder)\GreenUtil.wiki"
git status
git add .
git status
git commit -m "Commiting wiki changes"
git status
git push
git status
Write-Host "Start publishing documentation to wiki"