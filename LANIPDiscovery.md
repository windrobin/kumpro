# Introduction #

IPアドレスのリストを与えて、MACアドレス・NetBIOS名の情報を取得し一覧表示します。

**New!** リモート接続技術を活用できるようになりました。

入手: http://code.google.com/p/kumpro/downloads/list?can=2&q=LANIPDiscovery

![http://kumpro.googlecode.com/svn/trunk/LANIPDiscovery/ss.jpg](http://kumpro.googlecode.com/svn/trunk/LANIPDiscovery/ss.jpg)


# Details #

効能：
  * どこでもLAN(GMOインターネット株式会社様の商品)を使っている環境で、いまどこのPCがLANとつながっているか判らない場合に、IPアドレスからMACアドレス・NetBIOS名を得ることで、接続先の判別を助ける効能。

技術面：
  * 「状態」欄につきましては、ARP結果を示すようになっています。SendARPを使っています。
  * 「NetBIOS」欄につきましては、ソケット通信をして確かめています。相手のUDP 137にパケットを投げて、その反応・中身を見ています。nbtstatとWiresharkを使って解析し、実装しました。