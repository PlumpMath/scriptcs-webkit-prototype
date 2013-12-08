scriptcs-webkit-prototype
=========================

A prototype to try and do something akin to [node-webkit](https://github.com/rogerwang/node-webkit) using ScriptCS, OWIN and CefSharp.

It requires my fork of ScriptCs which addresses [#216](https://github.com/scriptcs/scriptcs/issues/216) by creating a scriptcs.x86.exe that can load native 32-bit libs.

After you install the packages, you will then need to manually copy packages\CefSharp.Wpf\cef\*.* into the packages\CefSharp.Wpf.1.25.5\lib\net40 folder. My plan is to create a Script Pack that will make sure the native and managed components of CefSharp can find each other without needing to do anything


