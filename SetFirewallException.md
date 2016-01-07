# Introduction #

Windows Firewall APIを利用し、ポートの追加・削除を支援致します。

コマンドラインで、例外の追加・削除を支援致します。

# 動作環境 #
  * Windows XP Pro SP3 確認済み
  * Windows Server 2008 [R2](https://code.google.com/p/kumpro/source/detail?r=2) Foundation 確認済み

# 使い方と、例 #

コマンドライン例

```
SetFirewallException set    port tcp 80 "Web server" 
SetFirewallException remove port tcp 80 "" 
```

引数の説明

  * 引数1: "set" か "remove"
  * 引数2: "port"
  * 引数3: "tcp" か "udp"
  * 引数4: ポート番号
  * 引数5: アプリ名称。"remove"を使う際は都合上、""を指定します。

# UAC昇格につきまして #

requireAdministratorを設定していますので、対応OSでは実行する前に昇格の確認がなされます。