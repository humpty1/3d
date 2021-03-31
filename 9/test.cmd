rem  яшЁрьшфър 5 ёЄЁюўхъ


a -f _dev.devDir.elevation.txt   -zNm "elevation"  -yNm "direction" -xNm "deviation"  
exit


a.exe -f _nord.east.focusAng.txt  -zNm "угол фокус-точка"  -yNm "отклонение Запад-Восток" -xNm "отклонение Юг-Север"  -l 4


exit

a.exe -f _nord.east.focusRo.txt  -yNm "расс.от фокуса"   -xNm "расстояние от точки"  -l 4
exit

a.exe -f _dev.focusRo.artefacts.txt    -yNm "расс. реальной координаты от фокуса"   -xNm "отклонение высчитанной от реальной" 


a.exe -f _dev.focusRo.artefacts.txt    -zNm "distance between point and focus projection"   -yNm "point deviation" 

exit

a.exe -f _nord.east.focusRo.txt  -zNm "focus Ro"  -v -l 4
exit

rem  яшЁрьшфър 5 ёЄЁюўхъ
a.exe -f data3d.min.txt -ln -v -l 4
exit

 