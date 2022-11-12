# experience_merge
Tool to help merging "eman-like" EXP files

# most common scenario
A folder containing the Eman engine and several EXP files.<br>
We merge them all with this script :<p>
cd "E:\eman"<br>
experience_merge.exe<br>
ren *.exp eman.exp<br>
eman.exe exp-defrag eman.exp 4<br>
del eman.exp.bak<br>
