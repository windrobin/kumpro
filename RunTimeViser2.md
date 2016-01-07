# ご紹介 #

Windowsの起動した・終了した日時の可視化を助けます

入手: http://code.google.com/p/kumpro/downloads/list?can=2&q=RunTimeViser2

![http://kumpro.googlecode.com/svn/trunk/RunTimeViser2/ss.jpg](http://kumpro.googlecode.com/svn/trunk/RunTimeViser2/ss.jpg)

# 詳細 #

使用例:
  * 夜中にマシンのファイルをバックアップをするように設定している場合。バックアップが出来ていなかった場合の原因として、マシンの電源が切れていなかったかどうかを検証する事例。

技術的な話:
  * 日時の取り方としまして、イベントログの情報(イベントビューアで参照可能)を参照しています。
  * システムログを見ています。
  * ソースはEventLogを見ています。
  * イベントID=6005 → 入
  * イベントID=6006 → 切 として判断しています。