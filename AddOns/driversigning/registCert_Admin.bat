cd /D %~dp0
CertMgr.exe -add RICOHTestCert.cer -c -s -r localMachine root 
CertMgr.exe -add RICOHTestCert.cer -c -s -r localMachine TrustedPublisher
PAUSE