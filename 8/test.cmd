rem  пирамидка 5 строчек
a.exe -f data3d.min.txt -ln -v 
exit
rem  данные опыта 1000 строчек
a.exe -f data3d.txt  -ln -l 4
exit



a.exe -f data3d.axis.txt -ln -v -l 4
exit

rem   оси
rem  столбик 13 строчек
a.exe -f data3d.pillar.txt -ln -v -l 4
exit



  было два массива на 24кбайт
  програм менеджер показывает                        Process и GarbageCollector

память     пиковая память  виртуальная PeakWorkingSet64 PeakVirtualMemorySize64  GetTotalMemory
11,4 метра 11,4            12,4 метра  12,1             247,6                    0,612





rem данные в одной плоскости X Y
a.exe -f data3d.plane.txt  -ln -v -l 4
exit

  было два массива на 120 байт
  програм менеджер показывает                        Process и GarbageCollector

память     пиковая память  виртуальная PeakWorkingSet64 PeakVirtualMemorySize64  GetTotalMemory
11,4 метра 11,4            12,4 метра  12,1             247,6                    0,612


 