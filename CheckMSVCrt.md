# Introduction #

CheckMSVCrtは、MSVCRT(Microsoft Visual C++ ランタイムライブラリ)の有無をMsiQueryProductState APIにより、検出・検定いたします。

入手: http://code.google.com/p/kumpro/downloads/list?can=2&q=CheckMSVCrt

![http://kumpro.googlecode.com/svn/trunk/CheckMSVCrt/ss.jpg](http://kumpro.googlecode.com/svn/trunk/CheckMSVCrt/ss.jpg)

# Details #

制限事項:
  * Windows インストーラの機能を利用致します。
  * Windows インストーラでのProduct有無 ≠ 実際の有無 が有り得ます。

参考情報:
  * [How to detect VC++ 2008 redistributable?](http://stackoverflow.com/questions/203195/how-to-detect-vc-2008-redistributable)
  * [Mailbag: How to detect the presence of the Visual C++ 2010 redistributable package](http://blogs.msdn.com/b/astebner/archive/2010/05/05/10008146.aspx)
  * [Mailbag: How to detect the presence of the VC 8.0 runtime redistributable package](http://blogs.msdn.com/b/astebner/archive/2007/01/16/mailbag-how-to-detect-the-presence-of-the-vc-8-0-runtime-redistributable-package.aspx)