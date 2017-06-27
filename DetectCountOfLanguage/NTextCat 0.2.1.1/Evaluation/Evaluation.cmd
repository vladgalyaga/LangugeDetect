pushd .
cd..
echo THE FIRST RESULT IS THE BEST!!!
NTextCat.exe -noprompt -e=UTF-8 < Evaluation\english-utf8.txt
NTextCat.exe -noprompt -e=950 < Evaluation\chinese-big5.txt
NTextCat.exe -noprompt -e=UTF-8 < Evaluation\chinese-utf8.txt
NTextCat.exe -noprompt -e=Unicode < Evaluation\chinese2-1200.txt
NTextCat.exe -noprompt -e=Unicode < Evaluation\russian-1200.txt
NTextCat.exe -noprompt -e=1251 < Evaluation\russian-1251.txt
NTextCat.exe -noprompt -e=UTF-8 < Evaluation\nl-utf8.txt
popd