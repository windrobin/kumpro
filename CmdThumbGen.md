# Introduction #

IExtractImage2を実装し、コマンドラインベースのサムネイル生成ツールとの併用で以て、実装と成します。

# Details #

次の様な事例を想定し、開発を進めました：
  * DXF/DWGやPDF等のファイルについて、Windows Explorerで、サムネイルを表示したい。
  * 本来商用アプリに頼るべき分野でございますが、次の様なスキームで考えています：
    * 最低限の物につきましては、費用の負担を軽くご提供。
    * もっと高機能な物を!→他社様の商用アプリをご紹介。要は、そちらを買ってください。
  * ファイルからサムネイルを生成する技術を有するものとします。
  * 然しながら、DLLで開発しますと、開発もデバッグも困難を極めます。
  * DXF→BMP, DWG→BMP, PDF→BMP変換できるコマンドラインツールは比較的楽に作られるので、この方法を活用したい。
  * IExtractImage2の実装から、コマンドラインツールを呼び出す方式で行きましょうか。
  * CmdThumbGen (x86用)、誕生。

# CmdThumbGen.Gene COMオブジェクト様態 by oleview #

![http://kumpro.googlecode.com/svn-history/trunk/CmdThumbGen/Gene.jpg](http://kumpro.googlecode.com/svn-history/trunk/CmdThumbGen/Gene.jpg)

IExtractImage2, IPersistFileを実装しています。

IMarshalは、FTM(Free Threaded Marshaler)を集約しますので、見えています。主にIRunnableTaskの為に採用していますが、Windows Explorerでの非ブロック・非同期操作にも一役買っているようです。

IRunnableTaskも一応、実装します。Windows Explorer (XP SP3)からは利用されている形跡が有りません。