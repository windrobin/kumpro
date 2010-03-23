SetFirewallException

Windows Firewall APIを利用し、ポートの追加・削除を支援致します。

使い方：

SetFirewallException set    port tcp 80 "Web server"
SetFirewallException remove port tcp 80 ""

引数1: "set" か "remove"
引数2: "port"
引数3: "tcp" か "udp"
引数4: ポート番号
引数5: アプリ名称。"remove"を使う際は都合上、""を指定します。

UAC昇格につきまして：

requireAdministratorを設定致しました。
