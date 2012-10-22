@echo off

SET DIR=%~d0%~p0%

SET server.database="vs01-inl-dev08"
SET database.name="squawkings-john"
SET database.user="sa"
SET database.password=""

SET sql.files.directory="%DIR%scripts"
SET version.file="%DIR%version.xml"
SET version.path="version"
SET environment=LOCAL

"rh.exe" /c="server=%server.database%;database=%database.name%;user id=%database.user%;password=%database.password%" /f=%sql.files.directory% /vf=%version.file% /vx=%version.path% /env=%environment% /repo="test" /t