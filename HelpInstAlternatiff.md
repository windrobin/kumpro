# Introduction #

AlternaTIFFのセットアップを支援致します。

# Getting started #

入手は、こちらから：http://code.google.com/p/kumpro/downloads/list?can=2&q=HelpInstAlternatiff

# Screen shots #

画面写真(XP)：

![http://kumpro.googlecode.com/svn/trunk/HelpInstAlternatiff/ssxp.jpg](http://kumpro.googlecode.com/svn/trunk/HelpInstAlternatiff/ssxp.jpg)

画面写真(Windows Server 2008 [R2](https://code.google.com/p/kumpro/source/detail?r=2) Foundation 64bits)：

![http://kumpro.googlecode.com/svn/trunk/HelpInstAlternatiff/ssw8.jpg](http://kumpro.googlecode.com/svn/trunk/HelpInstAlternatiff/ssw8.jpg)

# Details #

工夫点:
  * 「Install into IE」ボタンのご用意。Alternatiffをセットアップするためのページを表示いたします。
  * 「Test Page」ボタンのご用意。Alternatiffをテストするためのページを表示いたします。
  * 「Reactivate」ボタンのご用意。Quick Time等によりMIMEが変更された際の、Quick TimeからMIME設定を取り除いた後に再度、有効化するページを表示いたします。
  * 「昇格など...」ボタンのご用意。Windows Vista, Windows 7, Windows Server 2008等の、UACが有効となっているOSに配慮しまして、管理者権限で以てAlternatiffを導入できるような手はずを整えます。

課題：
  * 「UAC状態」「整合性レベル」「IsAdmin」の検査結果から、「いまセットアップするべきかどうか？」を判定する機能。
  * 動作を32ビット(x86)に固定化する。