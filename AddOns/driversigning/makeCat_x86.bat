cd /D %~dp0

rem �p�r�ɍ��킹�ăT�C������OS��ύX����
inf2cat\Inf2cat.exe /driver:%~dp1 /os:8_x86

signtool_x86.exe sign /v /f  RICOHTestCert.pfx "%1"