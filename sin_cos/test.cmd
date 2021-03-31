
echo app.exe -v -?  >.res.txt
app.exe -v -?  2>>.res.txt
echo app.exe -d -l 10 -p 0,5 -v -f dddd >>.res.txt
app.exe -d -l 10 -p 0,5 -v -f dddd >>.res.txt
