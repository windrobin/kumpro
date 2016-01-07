# Introduction #

移転しました： https://github.com/HiraokaHyperTools/AxTIF5

入手はこちらから： https://github.com/HiraokaHyperTools/AxTIF5/releases

![http://kumpro.googlecode.com/svn-history/trunk/AxTIF5/ss.jpg](http://kumpro.googlecode.com/svn-history/trunk/AxTIF5/ss.jpg)

将来的な対応につきまして:
  * Internet Explorer 11では、動作を確認しています。
  * Mozilla Firefox 33.1では、動作を確認しています。しかし、プラグイン技術であるNPAPIのサポート停止を受け、AxTIF5はいつか動作しなくなります。これに対して、代替手段は見つかっていません。[参考](http://www.forest.impress.co.jp/docs/news/20140304_637954.html)
  * Google Chrome 41.0では、動作を確認しています。しかし、プラグイン技術であるNPAPIのサポート停止を受け、AxTIF5は2015年9月以降、動作しなくなります。[参考](http://www.chromium.org/developers/npapi-deprecation)。これに対して、代替手段は見つかっていません。

# Details #

.TIF/.TIFFファイルの簡単な閲覧を支援致します。
  * Internet Explorer 32ビット版で、動作するように設計いたしました。
  * 修正BSDライセンスにて、ご提供いたします。
  * セットアッププログラム(.exe)の形でご提供いたします。コードサイニング証明書を取得する余力がなく、.CABでのインスタント配布につきましては、対応できません。ご容赦ください。
  * ~~アカウント単位・制限アカウント等でのご利用も想定しています。HKCUのみに登録操作し、HKCRやHKLMは書き換えしないようになっています。(書き換えていたら、ごめんなさい)~~。不具合多発のため、方針を変更しました。1.1.0から、HKUやHKCRの設定は削除し、HKLMに書き込むようにいたしました。