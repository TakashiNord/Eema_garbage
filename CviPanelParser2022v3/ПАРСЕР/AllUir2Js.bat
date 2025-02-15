@echo off

rem for %%i In (NewElements.uir)  do CviPanelParser.exe "%%i" s
for %%i In (*.uir)  do CviPanelParser.exe "%%i"
rem PAUSE
