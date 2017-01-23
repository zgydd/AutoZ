#include-once
#include <defineCom.au3>

#include<add\define2003.au3>
#include <add\define2003r2.au3>
#include <add\defineXp.au3>

#include <add\defineVista.au3>
#include <add\define2008.au3>
#include <add\define2008r2.au3>
#include <add\definewin7.au3>
#include <add\defineWin8.au3>
#include <add\defineWin8.1.au3>
#include <add\defineWin2012.au3>
#include <add\defineWin2012r2.au3>

If $Os_Version = "xp"  Then
   _DefineXp()
ElseIf $Os_Version = "win2003r2"  Then
   _Define2003R2()
ElseIf $Os_Version = "vista"  Then
   _DefineVista()
ElseIf $Os_Version = "win2008"  Then
   _Define2008()
ElseIf $Os_Version = "win7"  Then
   _DefineWin7()
ElseIf $Os_Version = "win2008r2"  Then
   _Define2008R2()
ElseIf $Os_Version = "win8"  Then
   _DefineWin8()
ElseIf $Os_Version = "win8.1"  Then
   _DefineWin81()
ElseIf $Os_Version = "win2012"  Then
   _DefineWin2012()
ElseIf $Os_Version = "win2012r2"  Then
   _DefineWin2012R2()
EndIf