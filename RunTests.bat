CD .\artifacts\bin\King.Azure.Test.Unit\Release\app

dnvm use 1.0.0-rc1-update1 -r clr -a x64
dnu restore -f ../../../King.Azure/Release
./King.Azure.Test.Unit.cmd

CD ../../../../../