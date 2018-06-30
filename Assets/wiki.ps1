        git config --global credential.helper store
        Add-Content "$HOME\.git-credentials" "https://$($env:access_token):x-oauth-basic@github.com`n"
        git config --global user.email "leandro.ltavares@gmail.com"
        git config --global user.name "Leandro Luciani Tavares"
        md C:\projects\wiki2
        cd C:\projects\wiki2
        git clone --branch=master https://github.com/leandroltavares/GreenUtil.wiki.git 
        cd GreenUtil.wiki
        git status
        C:\projects\greenutil\Assets\MarkdownGenerator.exe "C:\projects\greenutil\GreenUtil\bin\Release\net471\GreenUtil.dll" "C:\projects\wiki2\GreenUtil.wiki"
        git status
        git add .
        git status
        git commit -m "Commiting wiki changes"
        git status
        git push
        git status