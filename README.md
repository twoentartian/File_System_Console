#File_System_Console
</span></div><div>A little program to simulate the file system. Console UI.</div><div>Commands set:</div><div>&nbsp; 1) create file [name,content]</div><div>&nbsp; 2) create folder [name]</div><div>&nbsp; 3) open file [name]</div><div>&nbsp; 4) open folder [name]/[..]</div><div>&nbsp; 5) del file [name]</div><div>&nbsp; 6) del folder [name]</div><div>&nbsp; 7) open memo [name]</div><div>&nbsp; 8) create memo [name]</div><div>&nbsp; 9) list folder self</div><div>BASE folder is the root and every file/folder will be the child of BASE.</div><div>Memo is a file (.vDOS) on windows to store the simulated file tree. Opening memo will load all the files and folders in .vDOS file.</div><div><br /></div><div><span style="font-size: 32px;">
#An example (the part followed by >>> is user input)
<div><span style="line-height: 1.7;">&nbsp; &nbsp; -----------------------</span></div><div>&nbsp; &nbsp; || Virtual DOS. V1.0 ||</div><div>&nbsp; &nbsp; -----------------------</div><div>&nbsp; &nbsp; Open Target:BASE</div><div>&nbsp; &nbsp; Empty Folder</div><div>&gt;&gt;&gt; create file file1 content foe file1</div><div>&gt;&gt;&gt; create folder folder1</div><div>&gt;&gt;&gt; list folder self</div><div>&nbsp; &nbsp; BASE includes: &nbsp; &nbsp; file1(File), folder1(Folder)</div><div>&gt;&gt;&gt; open file file1</div><div>&nbsp; &nbsp; Open Target:file1</div><div>&nbsp; &nbsp; Content:content foe file1</div><div>&gt;&gt;&gt; open folder folder1</div><div>&nbsp; &nbsp; Open Target:folder1</div><div>&nbsp; &nbsp; Empty Folder</div><div>&gt;&gt;&gt; open folder ..</div><div>&nbsp; &nbsp; Open Target:BASE</div><div>&nbsp; &nbsp; BASE includes: &nbsp; &nbsp; file1(File), folder1(Folder)</div><div>&gt;&gt;&gt; del file file1</div><div>&gt;&gt;&gt; list folder self</div><div>&nbsp; &nbsp; BASE includes: &nbsp; &nbsp; folder1(Folder)</div><div>&gt;&gt;&gt; del folder folder1</div><div>&gt;&gt;&gt; list folder self</div><div>&nbsp; &nbsp; Empty Folder</div>
