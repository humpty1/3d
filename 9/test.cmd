rem  ��������� 5 �������


a -f _dev.devDir.elevation.txt   -zNm "elevation"  -yNm "direction" -xNm "deviation"  
exit


a.exe -f _nord.east.focusAng.txt  -zNm "㣮� 䮪��-�窠"  -yNm "�⪫������ �����-���⮪" -xNm "�⪫������ ��-�����"  -l 4


exit

a.exe -f _nord.east.focusRo.txt  -yNm "���.�� 䮪��"   -xNm "����ﭨ� �� �窨"  -l 4
exit

a.exe -f _dev.focusRo.artefacts.txt    -yNm "���. ॠ�쭮� ���न���� �� 䮪��"   -xNm "�⪫������ ����⠭��� �� ॠ�쭮�" 


a.exe -f _dev.focusRo.artefacts.txt    -zNm "distance between point and focus projection"   -yNm "point deviation" 

exit

a.exe -f _nord.east.focusRo.txt  -zNm "focus Ro"  -v -l 4
exit

rem  ��������� 5 �������
a.exe -f data3d.min.txt -ln -v -l 4
exit

 