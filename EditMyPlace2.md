# Introduction/Download #

Windowsのプレースバーや、Microsoft Officeのマイプレースにつきまして、カスタマイズを支援いたします。

こちらのページから入手できます: http://code.google.com/p/kumpro/downloads/list?can=2&q=EditMyPlace2

![http://kumpro.googlecode.com/svn/trunk/EditMyPlace2/ss.jpg](http://kumpro.googlecode.com/svn/trunk/EditMyPlace2/ss.jpg)

# Details #

効能1:
  * Windows 2000/XPをご利用の環境で、
  * ファイルを開く・保存する等、ファイルを選択する画面にて、
  * よく活用するフォルダを、ファイルを開く・保存するダイアログの左側に添付することで、
  * 参照に掛かる時間を短縮するような効能。

技術的な話:
  * Windows 2000/XP設定につきましては、レジストリ _`HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Comdlg32\PlacesBar`_ を見ています。
  * Office 2000設定につきましては、レジストリ _`HKCU\Software\Microsoft\Office\9.0\Common\Open Find\Places`_ 下を、
  * Office 2003設定につきましては、レジストリ _`HKCU\Software\Microsoft\Office\11.0\Common\Open Find\Places`_ 下を、各々見ています。
  * Office 2003のプレビュー表示につきましては、 _`"C:\Program Files\Microsoft Office\OFFICE11\OSA.EXE" -f`_ のようなものを使っています。

参考に致しました情報等:
  * [iris](http://www.vector.co.jp/soft/win95/util/se270852.html)
  * http://q.hatena.ne.jp/1116386193
    * Windowsの「名前を付けて保存」ダイアログで、左側に「最近使ったファイル」「デスクトップ」「マイドキュメント」「マイコンピュータ」などが表示されますが、ここに任意のディレクトリなどを追加できるユーティリティがあったと記憶しています。ご存じの方、教えてください。