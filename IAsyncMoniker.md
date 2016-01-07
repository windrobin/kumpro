# 結論 #

  * `IMoniker::BindToStorage`の最終的な結果は`OnStopBinding`で確認する。
  * ダウンロードの終了を捉える時は、`OnProgress`と`BINDSTATUS_ENDDOWNLOADDATA`で確認する。
  * ダウンロードの失敗は`OnStopBinding`で確認する。

# 資料 #

[C++ソースコード](http://code.google.com/p/kumpro/source/browse/trunk/TestIAsyncMoniker/T.cpp)

# 実験結果 #

一回目。ダウンロードが実行され、リアルタイムで進捗が報告されています。

```
 C:\Proj\gc-kumpro\trunk\TestIAsyncMoniker\Debug>TestIAsyncMoniker.exe  http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 asynchronous moniker
 BindToStorage  ->
 # GetBindInfo 00269328 ( 0 ) 002693D8
 # OnStartBinding 255 002692D8
 <- BindToStorage
 BindToStorage 0x000401e8
 # OnProgress 0 0 BINDSTATUS_PROXYDETECTING (null)
 # OnProgress 0 0 BINDSTATUS_COOKIE_SENT (null)
 # OnProgress 0 0 BINDSTATUS_FINDINGRESOURCE download.microsoft.com
 # OnProgress 0 0 BINDSTATUS_CONNECTING 202.229.2.138
 # OnProgress 0 0 BINDSTATUS_SENDINGREQUEST (null)
 # OnProgress 0 0 BINDSTATUS_CONTENTDISPOSITIONATTACH (null)
 # OnProgress 0 0 BINDSTATUS_MIMETYPEAVAILABLE application/pdf
 # OnProgress 5298 662280 BINDSTATUS_BEGINDOWNLOADDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnProgress 5298 662280 BINDSTATUS_CACHEFILENAMEAVAILABLE C:\Users\KU\AppData\Local\Microsoft\Windows\Temporary Internet Files\Content.IE5\IHUACTLP\windowscompoundbinaryfileformatspecification[1].pdf
 # OnDataAvailable BSCF_FIRSTDATANOTIFICATION 5298 0026DDE0 0029A840
 # OnProgress 9492 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 9492 0026DDE0 0029A840
 # OnProgress 13686 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 13686 0026DDE0 0029A840
 # OnProgress 16482 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 16482 0026DDE0 0029A840
 # OnProgress 23472 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 23472 0026DDE0 0029A840
 # OnProgress 47238 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 47238 0026DDE0 0029A840
 # OnProgress 107334 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 107334 0026DDE0 0029A840
 # OnProgress 185622 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 185622 0026DDE0 0029A840
 # OnProgress 261114 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 261114 0026DDE0 0029A840
 # OnProgress 324024 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 324024 0026DDE0 0029A840
 # OnProgress 339402 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 339402 0026DDE0 0029A840
 # OnProgress 347790 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 347790 0026DDE0 0029A840
 # OnProgress 368760 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 368760 0026DDE0 0029A840
 # OnProgress 417690 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 417690 0026DDE0 0029A840
 # OnProgress 493182 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 493182 0026DDE0 0029A840
 # OnProgress 560286 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 560286 0026DDE0 0029A840
 # OnProgress 623196 662280 BINDSTATUS_DOWNLOADINGDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 623196 0026DDE0 0029A840
 # OnProgress 662280 662280 BINDSTATUS_ENDDOWNLOADDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable BSCF_LASTDATANOTIFICATION 662280 0026DDE0 0029A840
 # OnStopBinding 0x0 (null)
```

二回目。ダウンロードは実行されず、キャッシュが渡ってきている様子を確認できます。

```
 C:\Proj\gc-kumpro\trunk\TestIAsyncMoniker\Debug>TestIAsyncMoniker.exe  http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 asynchronous moniker
 BindToStorage  ->
 # GetBindInfo 008B9328 ( 0 ) 008B93D8
 # OnStartBinding 255 008B92D8
 # OnProgress 0 0 BINDSTATUS_CONTENTDISPOSITIONATTACH (null)
 # OnProgress 0 0 BINDSTATUS_MIMETYPEAVAILABLE application/pdf
 # OnProgress 662280 662280 BINDSTATUS_BEGINDOWNLOADDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnProgress 662280 662280 BINDSTATUS_CACHEFILENAMEAVAILABLE C:\Users\KU\AppData\Local\Microsoft\Windows\Temporary Internet Files\Content.IE5\IHUACTLP\windowscompoundbinaryfileformatspecification[1].pdf
 # OnProgress 662280 662280 BINDSTATUS_ENDDOWNLOADDATA http://download.microsoft.com/download/0/b/e/0be8bdd7-e5e8-422a-abfd-4342ed7ad886/windowscompoundbinaryfileformatspecification.pdf
 # OnDataAvailable ? 662280 008BDDE0 008D0AD0
 # OnStopBinding 0x0 (null)
 <- BindToStorage
 BindToStorage 0x00000000
```

特別編。ダウンロード失敗をシミュレーションするために、途中でネットワークアダプタを無効にしました。OnStopBindingのHRESULT値が失敗を示しています。

```
 C:\Proj\gc-kumpro\trunk\TestIAsyncMoniker\Debug>TestIAsyncMoniker.exe http://xxx
 asynchronous moniker
 BindToStorage  ->
 # GetBindInfo 00328E78 ( 0 ) 00328F28
 # OnStartBinding 255 00328E28
 <- BindToStorage
 BindToStorage 0x000401e8
 # OnProgress 0 0 BINDSTATUS_PROXYDETECTING (null)
 # OnProgress 0 0 BINDSTATUS_FINDINGRESOURCE www.fujitec-neji.co.jp
 # OnProgress 0 0 BINDSTATUS_CONNECTING 210.248.135.44
 # OnProgress 0 0 BINDSTATUS_SENDINGREQUEST (null)
 # OnProgress 0 0 BINDSTATUS_MIMETYPEAVAILABLE application/pdf
 # OnProgress 6661 7944746 BINDSTATUS_BEGINDOWNLOADDATA http://xxx
 # OnProgress 6661 7944746 BINDSTATUS_CACHEFILENAMEAVAILABLE C:\Users\KU\AppData\Local\Microsoft\Window
 # OnDataAvailable BSCF_FIRSTDATANOTIFICATION 6661 0032D930 0034AC40
 # OnProgress 16384 7944746 BINDSTATUS_DOWNLOADINGDATA http://xxx
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 16384 0032D930 0034AC40
 # OnProgress 24576 7944746 BINDSTATUS_DOWNLOADINGDATA http://xxx
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 24576 0032D930 0034AC40
 # OnProgress 32768 7944746 BINDSTATUS_DOWNLOADINGDATA http://xxx
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 32768 0032D930 0034AC40
 # OnProgress 40960 7944746 BINDSTATUS_DOWNLOADINGDATA http://xxx
 ...
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 901120 0032D930 0034AC40
 # OnProgress 902518 7944746 BINDSTATUS_DOWNLOADINGDATA http://xxx
 # OnDataAvailable BSCF_INTERMEDIATEDATANOTIFICATION 902518 0032D930 0034AC40
 # OnStopBinding 0x800C0007 (null)
```