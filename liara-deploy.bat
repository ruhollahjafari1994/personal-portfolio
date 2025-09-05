@echo off
:: Liara Deploy Batch Script
:: ساخته‌شده برای ویندوز

echo ================================
echo  Liara CLI Deploy Script
echo ================================
echo.
echo *** نکته: قبل از اجرا، مطمئن شوید که NodeJS نصب شده است.
echo  برای دانلود NodeJS به: https://nodejs.org بروید.
echo.

pause

:: نصب Liara CLI
echo در حال نصب Liara CLI...
npm install -g @liara/cli
if %errorlevel% neq 0 (
    echo خطا در نصب Liara CLI. لطفا بررسی کنید که NodeJS و npm نصب شده باشند.
    pause
    exit /b
)

:: ورود به حساب Liara
echo ورود به سامانه Liara...
liara login
if %errorlevel% neq 0 (
    echo خطا در ورود به حساب. لطفا نام کاربری و رمز را بررسی کنید.
    pause
    exit /b
)

:: دیپلوی پروژه
echo در حال دیپلوی پروژه...
liara deploy

echo.
echo عملیات پایان یافت.
pause
